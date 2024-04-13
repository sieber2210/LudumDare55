using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public float attackSpeed;
    public float moveSpeed;
    public float attackDamage;
    public string name;


    public void Attack()
    {
        Debug.Log(name + " has attacked, dealing: " + attackDamage + " damage!");
    }
}
