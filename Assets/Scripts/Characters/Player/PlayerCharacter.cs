using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public PlayerCharacter()
    {
        attackSpeed = 2.5f;
        moveSpeed = 5f;
        attackDamage = 10f;
        name = "Player";
    }
}
