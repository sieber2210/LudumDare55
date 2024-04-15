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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Attack();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Heal();
        if (Input.GetKeyDown(KeyCode.Alpha4))
            EndTurn();
    }

    public override void StartTurn()
    {
        base.StartTurn();
        //do extra skeleton stuff on start of turn
        //skeletons are aggressive melee fighters. Just move and attack.
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
