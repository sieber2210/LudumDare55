using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    private void Start()
    {
        characterName = "Player";
        attackDamage = 10f;
        healAmount = 5f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartTurn();
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Attack();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Heal();
        if (Input.GetKeyDown(KeyCode.Alpha3))
            EndTurn();
    }

    public override void StartTurn()
    {
        base.StartTurn();
        //do extra player stuff on start of turn
    }

    public override void Attack()
    {
        base.Attack();
        //do extra player attack stuff
    }

    public override void Heal()
    {
        base.Heal();
        //do extra player heal stuff
    }

    public override void EndTurn()
    {
        base.EndTurn();
        //do extra player stuff on end of turn
    }
}