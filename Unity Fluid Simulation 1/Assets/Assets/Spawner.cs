using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public GameObject particlePrefab;
    public int particleCount = 60;
    public float spawnRadius = 1.5f;

    [HideInInspector] public List<Transform> transforms = new List<Transform>();
    [HideInInspector] public List<Vector2> velocities = new List<Vector2>();

    void Start()
    {
        for(int i = 0; i < particleCount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = new Vector3(randomOffset.x, randomOffset.y + 1f, 0f);

            GameObject p = Instantiate(particlePrefab, spawnPos, Quaternion.identity);

            transforms.Add(p.transform);
            velocities.Add(Vector2.zero);
        }
        
    }

    void Update()
    {
        
    }
}
