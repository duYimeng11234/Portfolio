using UnityEngine;
using System.Collections.Generic;

public class SpatialGrid : MonoBehaviour
{
    private Dictionary<Vector2Int, List<int>> grid = new Dictionary<Vector2Int, List<int>>();
    public float cellSize;

    public void Build(List<Transform> transforms, float radius)
    {
        cellSize = radius;
        grid.Clear();
        for(int i = 0; i < transforms.Count; i++)
        {
            Vector2Int cell = GetCell(transforms[i].position);
            if(!grid.ContainsKey(cell))
            {
                grid[cell] = new List<int>();
            }
            grid[cell].Add(i);
        }
    }

    private Vector2Int GetCell(Vector2 pos)
    {
        return new Vector2Int(Mathf.FloorToInt(pos.x / cellSize), Mathf.FloorToInt(pos.y / cellSize));
    }

    public List<int> GetNeighbors(Vector2 worldPos)
    {
        List<int> result = new List<int>();
        Vector2Int center = GetCell(worldPos);
        for(int dx = -1; dx <= 1; dx++)
        {
            for(int dy = -1; dy <= 1; dy++)
            {
                Vector2Int neighbor = new Vector2Int(center.x + dx, center.y + dy);
                if(grid.ContainsKey(neighbor))
                {
                    result.AddRange(grid[neighbor]);
                }
            }
        }
        return result;
    }
}
