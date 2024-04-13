using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    private void Start()
    {
        EnemyCharacter skelly = new EnemyCharacter();
        Debug.Log("Creating " + skelly.name + "...");
        
        Debug.Log(skelly.name + "created!");

        skelly.Attack();
    }
}
