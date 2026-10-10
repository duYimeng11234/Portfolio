using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Data;

public class FluidBoxInteraction : MonoBehaviour
{
    public float pushStrength = 3.0f;
    public float restitution = 0.8f;
    public float fluidForceMult = 3.0f;
    public float buoyancyStrength = 15f;
    public float fluidDrag = 1.5f;
    public float torqueStrength = 3f;
    private Spawner spawner;
    private SpatialGrid grid;
    private Rigidbody2D rb;
    private HashSet<int> candidates = new HashSet<int>();

    void Start()
    {
        spawner = FindAnyObjectByType<Spawner>();
        grid = FindAnyObjectByType<SpatialGrid>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        Vector2 boxPos = rb.position;
        float angle = rb.rotation * Mathf.Deg2Rad;
        Vector2 halfSize = transform.localScale * 0.5f;
        Vector2 right = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        Vector2 up = new Vector2(-Mathf.Sin(angle), Mathf.Cos(angle));
        candidates.Clear();
        Vector2[] samplePoints = new Vector2[]
        {
            boxPos,
            boxPos + right * halfSize.x + up * halfSize.y,
            boxPos - right * halfSize.x + up * halfSize.y,
            boxPos + right * halfSize.x - up * halfSize.y,
            boxPos - right * halfSize.x - up * halfSize.y
        };
        foreach(Vector2 point in samplePoints)
        {
            foreach(int idx in grid.GetNeighbors(point))
            {
                candidates.Add(idx);
            }
        }
        Vector2 totalForceOnBox = Vector2.zero;
        int particlesInside = 0;
        foreach(int i in candidates)
        {
            Vector2 worldPos = spawner.transforms[i].position;
            Vector2 local = worldPos - boxPos;
            float localX = Vector2.Dot(local, right);
            float localY = Vector2.Dot(local, up);
            if(Mathf.Abs(localX) < halfSize.x && Mathf.Abs(localY) < halfSize.y)
            {
                float overlapX = halfSize.x - Mathf.Abs(localX);
                float overlapY = halfSize.y - Mathf.Abs(localY);
                Vector2 pushDir;
                float overlap;
                if(overlapX < overlapY)
                {
                    pushDir = right * Mathf.Sign(localX);
                    overlap = overlapX;
                }
                else
                {
                    pushDir = up * Mathf.Sign(localY);
                    overlap = overlapY;
                }
                spawner.transforms[i].position = (Vector2)spawner.transforms[i].position + pushDir * (overlap * pushStrength + 0.01f);
                Vector2 vel = spawner.velocities[i];
                Vector2 reflected = vel - 2f * Vector2.Dot(vel, pushDir) * pushDir;
                spawner.velocities[i] = reflected * restitution + rb.linearVelocity * (1f - restitution);
                totalForceOnBox -= pushDir * overlap * fluidForceMult;
                float torque = -localX * overlap * torqueStrength;
                rb.AddTorque(torque, ForceMode2D.Force);
                particlesInside++;
            }
        }
        float submersionRatio = Mathf.Clamp(particlesInside / 10f, 0f, 1f);
        if (particlesInside > 0)
        {
            totalForceOnBox += Vector2.up * submersionRatio * buoyancyStrength;
        }
        if(particlesInside > 0)
        {
            totalForceOnBox -= (Vector2)rb.linearVelocity * fluidDrag * submersionRatio;
        }
        rb.AddForce(totalForceOnBox, ForceMode2D.Force);
    }
}
