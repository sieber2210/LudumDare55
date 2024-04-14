using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public VoidEvent playerTurnEnd;
    public Animator anim;

    private void Start()
    {
        characterName = "Player";
        attackDamage = 10f;
        healAmount = 5f;
        stunnedTime = 3f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Attack();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Heal();
        if (Input.GetKeyDown(KeyCode.Alpha3))
            EndTurn();
        if (Input.GetKeyDown(KeyCode.Alpha5))
            TakeDamage(5);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            StartCoroutine(Stunned());
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
        anim.SetTrigger("isAttacking");
    }

    public override void Heal()
    {
        base.Heal();
        //do extra player heal stuff
    }

    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
        anim.SetTrigger("onHit");
    }

    public override void EndTurn()
    {
        base.EndTurn();
        //do extra player stuff on end of turn
        playerTurnEnd.Raise();
    }

    public override IEnumerator Stunned()
    {
        anim.SetTrigger("onStunned");
        anim.SetBool("isStunned", true);
        yield return new WaitForSeconds(stunnedTime);
        anim.SetBool("isStunned", false);        
    }
}