using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class MouseForceGPU : MonoBehaviour
{
    public float radius = 1.5f;
    public float strength = 10f;
    private Spawner spawner;
    private SpatialGrid grid;
    private Vector2 mouseWorld;

    void Start()
    {
        spawner = GetComponent<Spawner>();
        grid = GetComponent<SpatialGrid>();
    }
    
    void Update()
    {
        bool pushing = Mouse.current.leftButton.isPressed;
        bool pulling = Mouse.current.rightButton.isPressed;
        if(!pushing && !pulling)
        {
            return;
        }
        Vector3 mouseScreen = Mouse.current.position.ReadValue();
        mouseScreen.z = -Camera.main.transform.position.z;
        mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        List<int> nearby = grid.GetNeighbors(mouseWorld);
        float dt = Time.deltaTime;
        foreach(int i in nearby)
        {
            Vector2 pos = spawner.transforms[i].position;
            Vector2 diff = pos - mouseWorld;
            float dist = diff.magnitude;
            if(dist < radius && dist > 0.001f)
            {
                float falloff = 1f - (dist / radius);
                Vector2 direction = diff / dist;
                if(pushing)
                {
                    spawner.velocities[i] += direction * falloff * strength * dt;
                }
                else
                {
                    spawner.velocities[i] -= direction * falloff * strength * dt;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
        Gizmos.DrawWireSphere(mouseWorld, radius);
    }
}
