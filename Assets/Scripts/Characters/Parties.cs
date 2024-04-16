using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// parties should be represented by a blank game object whose children will be considered "on the same side".
// If we add another faction for 3+ way fights, it should be listed here
public class Parties : MonoBehaviour
{
    public static Transform pcs;
    public static Transform enemies;

    public void Awake()
    {
        enemies = transform.GetChild(0);
        pcs = transform.GetChild(1);
    }
}
