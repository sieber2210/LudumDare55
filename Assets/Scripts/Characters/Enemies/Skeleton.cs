using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Character
{
    protected override void Start()
    {
        base.Start();
        characterName = "Skeleton";
        attackDamage = 5f;
        healAmount = 1f;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void StartTurn()
    {
        base.StartTurn();
        //do extra skeleton stuff on start of turn
        //skeletons are aggressive melee fighters. Just move and attack.

        // find the closest enemy in the opposite party
        Character closestEnemy = FindClosestEnemy();

        // find the closest tile that is skill.range units away from the target
        skill = BasicMelee.Instance;
        Tile basicMeleeDestination = FindClosestTileToUseSkill(closestEnemy, skill);

        // find the shortest path to the target tile
        path = GameGrid.Instance.FindPath(location, basicMeleeDestination);

        // start moving toward that tile
        goal = Goal.MOVE;
        ResetDirection();

        // IMPORTANT: when animations need to be considered, animation start and ending frames should trigger next steps.
    }

    public override void Attack()
    {
        base.Attack();
        //do extra skeleton attack stuff
    }

    public override void Heal()
    {
        base.Heal();
        //do extra skeleton heal stuff
    }

    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
    }

    public override void EndTurn()
    {
        base.EndTurn();
        //do extra skeleton stuff on end of turn
        //enemyTurnEnd.Raise();
    }
}
