using UnityEngine;

public class GridManager : MonoBehaviour
{
    public float smoothingRadius = 0.8f;
    private Spawner spawner;
    private SpatialGrid grid;

    void Start()
    {
        spawner = GetComponent<Spawner>();
        grid = GetComponent<SpatialGrid>();
    }

    void Update()
    {
        grid.Build(spawner.transforms, smoothingRadius);
    }
}
