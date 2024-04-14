using System.Collections.Generic;
using UnityEngine;

public enum TurnType { PLAYER, ENEMY}

public class TurnManager : MonoBehaviour
{
    public Transform characters;         // transform that holds Character objects
    private List<Character> turnOrder;
    private int turnIndex;               // increment at end of turns, modulo char count. Determines who goes next

    public VoidEvent playerTurnStart;
    public VoidEvent enemyTurnStart;
    //public VoidEvent turnEnd;
    //public VoidEvent characterSummoned;  // do we want to support adding more characters during a match?

    public TurnType currentTurnOwner;

    private void Awake()
    {
        currentTurnOwner = TurnType.PLAYER;
    }

    // characters may not have been initialized in Awake(), vars that depend on chars should be initialized here
    private void Start()
    {
        Transform enemies = characters.GetChild(0);
        Transform pcs = characters.GetChild(1);
        List<Character> allChars = new List<Character>();
        foreach (GameObject enemy in enemies)
        {
            allChars.Add(enemy.GetComponent<Character>());
        }
        foreach (GameObject pc in pcs)
        {
            allChars.Add(pc.GetComponent<Character>());
        }

        turnOrder = new List<Character>();
        while (allChars.Count > 0) 
        {
            int r = Random.Range(0, allChars.Count);
            turnOrder.Add(allChars[r]);
            allChars.RemoveAt(r);
        }

        turnOrder[0].StartTurn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentTurnOwner == TurnType.PLAYER)
                playerTurnStart.Raise();
            else if (currentTurnOwner == TurnType.ENEMY)
                enemyTurnStart.Raise();
        }
    }

    public void AdvanceTurnOrder()
    {
        if (currentTurnOwner == TurnType.PLAYER)
            currentTurnOwner = TurnType.ENEMY;
        else
            currentTurnOwner = TurnType.PLAYER;
    }
}
