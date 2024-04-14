using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected string characterName;
    protected float attackDamage;
    protected float healAmount;

    public virtual void StartTurn()
    {
        //complete any action to be done on turn start.
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
        Debug.Log(characterName + " has ended their turn!");
    }
}
