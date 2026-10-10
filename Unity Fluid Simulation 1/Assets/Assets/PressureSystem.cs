using TMPro.EditorUtilities;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PressureSystem : MonoBehaviour
{
    public float smoothingRadius = 0.8f;
    public float pressureStrength = 5f;
    private Spawner spawner;

    void Start()
    {
        spawner = GetComponent<Spawner>();
    }
    
    void Update()
    {
        float dt = Time.deltaTime;
        int n = spawner.transforms.Count;
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                Vector2 posA = spawner.transforms[i].position;
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
