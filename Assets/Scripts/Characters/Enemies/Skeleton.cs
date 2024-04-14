using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Character
{
    private void Start()
    {
        characterName = "Skeleton";
        attackDamage = 5f;
        healAmount = 1f;
    }

    public override void StartTurn()
    {
        base.StartTurn();
        //do extra skeleton stuff on start of turn
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

    public override void EndTurn()
    {
        base.EndTurn();
        //do extra skeleton stuff on end of turn
    }
}
