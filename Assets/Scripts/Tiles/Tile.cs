using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int gridLocation;
    public Vector3 worldSpaceLocation;
    public Character occupant;

    private void Awake()
    {
        worldSpaceLocation = transform.position;
    }

    public bool IsOccupied()
    { 
        return occupant != null; 
    }
}
