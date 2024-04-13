using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    public EnemyCharacter()
    {
        attackSpeed = 2.5f;
        moveSpeed = 5f;
        attackDamage = 5f;
        name = "Enemy";
    }
}
