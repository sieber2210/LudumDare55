using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMB : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Creating player...");
        PlayerCharacter thisPC = new PlayerCharacter();
        Debug.Log("Player created!");

        thisPC.Attack();
    }
}
