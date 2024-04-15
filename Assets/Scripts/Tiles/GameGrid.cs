using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public GameObject tilePrefab; // for now, just a point. Longer term, would be cool to have a blue neon outline of tiles, or neon points. Make these togglable.

    public int width = 10;
    public int height = 10;

    public int tileOffsetX = 5;
    public int tileOffsetZ = 5;

    public void Awake()
    {
        for (int i = 0; i < width; i++) 
        {
            for (int j = 0; j < height; j++)
            {
                // instantiate a point
                GameObject point = Instantiate(
                    tilePrefab,
                    transform.position + new Vector3(tileOffsetX * i, 0f, tileOffsetZ * j),
                    Quaternion.identity,
                    transform
                );
            }
        }
    }
}
