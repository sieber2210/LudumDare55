using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected string characterName;
    protected float attackDamage;
    protected float healAmount;

    protected bool isMyTurn;

    public VoidEvent turnEnd;
    //protected Goal goal; // should I attack, move, heal, or end my turn? Pattern: Finite Automaton.
    //protected Character target;

    // delegates are params that represent a function, any function that has the specified param list.
    // This way, classes that extend character can pass their own skill function here and the base class can still understand what to do.
    protected delegate void action(); 

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

    public virtual void EndTurn()
    {
        //complete any action to be done on the end of the turn.
        isMyTurn = false;
        turnEnd.Raise();
        Debug.Log(characterName + " has ended their turn!");
    }

    public void Update()
    {
        
    }
}
