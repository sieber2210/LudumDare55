using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public const float TILE_SIZE = 5f; // in unity units

    // just give everyone access to the grid, it's simpler...
    public static GameGrid Instance;

    public GameObject tilePrefab; // for now, just a point. Longer term, would be cool to have a blue neon outline of tiles, or neon points. Make these togglable.

    public int width = 10;
    public int height = 10;

    public int tileOffsetX = 5;
    public int tileOffsetZ = 5;

    public Tile[,] grid;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        if (grid != null) return;

        grid = new Tile[width, height];
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
                Tile tile = point.GetComponent<Tile>();
                tile.gridLocation = new Vector2Int( i, j );
                grid[i, j] = tile;
            }
        }
    }

    public Vector3 GetTile(int x, int y) 
    {
        return grid[x, y].transform.position;
    }

    public bool IsOutOfBounds(Vector2Int tile)
    { 
        return IsOutOfBounds(tile.x, tile.y);
    }

    public bool IsOutOfBounds(int x, int y)
    {
        return (x >= width || y >= height) || (x < 0 || y < 0);
    }

    // This can get confusing because we need to keep in mind the x,y coordinates that the grid is made of.
    // But the grid contains GameObjects, not simple vectors, because we also need to know how an x,y tile converts to world space Vector3 coordinates
    public HashSet<Tile> GetTilesInRange(Tile center, int range)
    { 
        // range 0 can only mean the tile cast from
        if (range == 0) return new HashSet<Tile> { grid[center.gridLocation.x, center.gridLocation.y] };

        // using a HashSet means we can blindly add tiles and duplicates will not exist
        HashSet<Tile> tilesInRange = new HashSet<Tile>();
        GetTilesInRangeDFS(center.gridLocation.x, center.gridLocation.y, 0, range, tilesInRange, new HashSet<Tile>());
        return tilesInRange;
    }

    // perform recursive depth first search to find all tiles in range
    // x, y: location of the tile being considered by this frame
    // stepNumber: how many steps have we taken to get here? If equal to range, add myself to the hash set and return. Else, find neighbors and recurse
    // range: how many steps to take? Caps number of recursions.
    // tiles: will store our response, since the response needs to be built across multiple stack frames
    // touched: remembers which tiles we already touched. Touching them again is suboptimal, they can't be part of the solution more than once, if at all.
    private void GetTilesInRangeDFS(int x, int y, int stepNumber, int range, HashSet<Tile> tiles, HashSet<Tile> touched)
    {
        touched.Add(grid[x, y]);

        if (stepNumber == range)
        {
            tiles.Add(grid[x, y]);
            return;
        }

        if (IsValidForRecursion(x + 1, y, touched)) { GetTilesInRangeDFS(x + 1, y, stepNumber + 1, range, tiles, touched); }
        if (IsValidForRecursion(x - 1, y, touched)) { GetTilesInRangeDFS(x - 1, y, stepNumber + 1, range, tiles, touched); }
        if (IsValidForRecursion(x, y + 1, touched)) { GetTilesInRangeDFS(x, y + 1, stepNumber + 1, range, tiles, touched); }
        if (IsValidForRecursion(x, y - 1, touched)) { GetTilesInRangeDFS(x, y - 1, stepNumber + 1, range, tiles, touched); }

        tiles.Add(grid[x, y]);
    }

    // perform a modified breadth first search that enqueues path followed instead of just the last step touched.
    // BFS ensures we get the shortest possible path, compared to depth first search which basically gives any random path.
    public List<Tile> FindPath(Tile from, Tile to)
    {
        HashSet<Tile> touched = new HashSet<Tile>();
        Queue<List<Tile>> q = new Queue<List<Tile>>();
        q.Enqueue(new List<Tile> { from });
        touched.Add(from);
        while (q.Count > 0)
        {
            List<Tile> path = q.Dequeue();
            Tile cur = path.Last();
            if (cur == to)
            {
                return path;
            }

            Vector2Int loc = cur.gridLocation;
            BFSAddStep(q, path, touched, loc.x + 1, loc.y);
            BFSAddStep(q, path, touched, loc.x - 1, loc.y);
            BFSAddStep(q, path, touched, loc.x, loc.y + 1);
            BFSAddStep(q, path, touched, loc.x, loc.y - 1);
        }
        return null;
    }

    private void BFSAddStep(Queue<List<Tile>> q, List<Tile> path, HashSet<Tile> touched, int x, int y)
    {
        if (IsValidForRecursion(x, y, touched))
        {
            List<Tile> newPath = new List<Tile>(path) { grid[x, y] };
            touched.Add(grid[x, y]);
            q.Enqueue(newPath);
        }
    }

    private bool IsValidForRecursion(int x, int y, HashSet<Tile> touched)
    {
        return !IsOutOfBounds(x, y) && !touched.Contains(grid[x, y]);
    }

    public Tile GetRandomTile()
    {
        if (grid == null) Start();
        return grid[Random.Range(0, width), Random.Range(0, height)];
    }
}
