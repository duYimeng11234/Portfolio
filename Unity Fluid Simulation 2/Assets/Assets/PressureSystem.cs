using TMPro.EditorUtilities;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PressureSystem : MonoBehaviour
{
    public float smoothingRadius = 0.8f;
    public float pressureStrength = 5f;
    private Spawner spawner;
    private SpatialGrid grid;

    void Start()
    {
        spawner = GetComponent<Spawner>();
        grid = GetComponent<SpatialGrid>();
    }
    
    void Update()
    {
        float dt = Time.deltaTime;
        grid.Build(spawner.transforms, smoothingRadius);
        for (int i = 0; i < spawner.transforms.Count; i++)
        {
            Vector2 posA = spawner.transforms[i].position;
            List<int> neighbors = grid.GetNeighbors(posA);
            foreach(int j in neighbors)
            {
                if(j <= i)
                {
                    continue;
                }
                Vector2 posB = spawner.transforms[j].position;
                Vector2 diff = posA - posB;
                float dist = diff.magnitude;
                if (dist < smoothingRadius && dist > 0.001f)
                {
                    float strength = 1f - (dist / smoothingRadius);
                    Vector2 direction = diff / dist;
                    Vector2 force = direction * strength * pressureStrength * dt;
                    spawner.velocities[i] += force;
                    spawner.velocities[j] -= force;
                }
            }
        }
    }
}
