using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected enum Goal 
    { 
        WAIT,  // do nothing
        //THINK, // rerun decision making logic. Must be done to start the turn because enemies could have moved, my memory is no longer accurate.
        MOVE,  // perform smooth animation until target is reached or can't move further
        USE_SKILL, // target has been reached, use a skill
        //END_TURN // I've acted and my animations have finished, end my turn
    }

    // how fast should we move?
    public const float VELOCITY = 25.0f; // 5 is 1 tile per second.
    // it's possible for characters to become uncentered depending on frame rate and velocity. We'll need to snap them in place occassionally.
    private const float SNAP_TIME = GameGrid.TILE_SIZE / VELOCITY; 

    protected string characterName;
    protected float attackDamage;
    protected float healAmount;
    protected float stunnedTime;

    protected bool isMyTurn;

    public VoidEvent turnEnd;
    protected Tile location;
    protected Transform party;

    // Goal specific vars
    protected Goal goal; // should I act, move, or end my turn? Pattern: Finite Automaton.
    protected Character target; // the intended target of my skill
    protected Skill skill; // the skill we want to perform
    protected List<Tile> path; // the path I will follow while moving
    protected Tile nextTile; // the next tile I will animate toward

    private float timer;
    private UnityEngine.Vector3 direction;
    

    protected virtual void Start()
    {
        // characters should be stored under an object that represents their party
        party = transform.parent;

        // we should have some way to organize characters before the match. For now, random placement.
        location = GameGrid.Instance.GetRandomTile();
        Debug.Log(name + " starts at " + location.gridLocation.x + " " + location.gridLocation.y);
        transform.position = new UnityEngine.Vector3(location.worldSpaceLocation.x, transform.position.y, location.worldSpaceLocation.z);
    }

    protected virtual void Update()
    {
        switch (goal) 
        {
            case Goal.MOVE:
                // animation logic. Once animation ends, determine if location is close enough to use skill.
                timer += Time.deltaTime;
                if (timer >= SNAP_TIME)
                {
                    transform.position = new UnityEngine.Vector3(nextTile.worldSpaceLocation.x, transform.position.y, nextTile.worldSpaceLocation.z);
                    location = nextTile;
                    timer = 0f;
                    ResetDirection();
                }
                else
                {
                    transform.position += direction * VELOCITY * Time.deltaTime;
                }
                break;
            case Goal.USE_SKILL: 
                // animation logic. Once animation ends, end turn.
                break;
            default: break;
        }
    }

    // pop the path stack to get next tile. Set direction to an identity vector (vector of 1 in any direction) so we can control speed better.
    protected void ResetDirection()
    {
        if (path.Count > 0)
        {
            nextTile = path.First();
            path.RemoveAt(0);
            float xDiff = nextTile.worldSpaceLocation.x - transform.position.x;
            float zDiff = nextTile.worldSpaceLocation.z - transform.position.z;
            float xDirection = xDiff > 0 ? 1 : xDiff < 0 ? -1 : 0;
            float zDirection = zDiff > 0 ? 1 : zDiff < 0 ? -1 : 0;
            direction = new UnityEngine.Vector3(xDirection, 0, zDirection);
        }
        else
        {
            goal = Goal.USE_SKILL;
        }
    }

    // let subclasses implement the "determine action and target" logic
    public virtual void StartTurn()
    {
        //complete any action to be done on turn start.
        isMyTurn = true;
        Debug.Log(characterName + " has started their turn!");
    }

    public virtual void Attack()
    {
        Debug.Log(characterName + " has attacked, dealing: " + attackDamage + " damage!");
    }

    public virtual void Heal()
    {
        Debug.Log(characterName + " has healed for: " + healAmount + "!");
    }

    public virtual void TakeDamage(int damageAmount)
    {
        Debug.Log(characterName + " has been hit for: " + damageAmount + "!");
    }

    public virtual void EndTurn()
    {
        //complete any action to be done on the end of the turn.
        isMyTurn = false;
        turnEnd.Raise();
        Debug.Log(characterName + " has ended their turn!");
    }

    public virtual IEnumerator Stunned()
    {
        yield return null;
    }

    public Tile GetLocation()
    {
        return location;
    }

    /**********************************************************************
     * Decision Making Logic
     *********************************************************************/

    protected Character FindClosestEnemy()
    {
        Transform otherParty = GetOtherParty();
        Character closestEnemy = null;
        float closestEnemyDistance = 99999f;
        foreach (Transform enemyT in otherParty)
        {
            Character enemy = enemyT.GetComponent<Character>();
            float distance = DistanceFromMe(enemy.GetLocation());
            if (distance < closestEnemyDistance)
            {
                closestEnemyDistance = distance;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }

    protected Tile FindClosestTileToUseSkill(Character enemy, Skill skill)
    {
        Tile enemyLocation = enemy.GetLocation();
        HashSet<Tile> moveCandidates = GameGrid.Instance.GetTilesInRange(enemyLocation, skill.Range());
        Tile closestMC = null;
        float closestMCDistance = 99999f;
        foreach (Tile tile in moveCandidates)
        {
            float distance = DistanceFromMe(tile);
            if (distance < closestMCDistance)
            {
                closestMCDistance = distance;
                closestMC = tile;
            }
        }
        return closestMC;
    }

    protected Transform GetOtherParty()
    {
        if (party == Parties.enemies)
        {
            return Parties.pcs;
        }
        else
        {
            return Parties.enemies;
        }
    }

    protected float DistanceFromMe(Tile destination)
    {
        //sqrt(x2 - x1)^2 + (y2 - y1)^2
        float xTerm = Mathf.Pow(destination.gridLocation.x - location.gridLocation.x, 2);
        float yTerm = Mathf.Pow(destination.gridLocation.y - location.gridLocation.y, 2);
        return Mathf.Sqrt(xTerm + yTerm);
    }
}
