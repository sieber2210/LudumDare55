using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnType { PLAYER, ENEMY}

public class TurnManager : MonoBehaviour
{
    public VoidEvent playerTurnStart;
    public VoidEvent enemyTurnStart;

    public TurnType currentTurnOwner;

    private void Awake()
    {
        currentTurnOwner = TurnType.PLAYER;
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
