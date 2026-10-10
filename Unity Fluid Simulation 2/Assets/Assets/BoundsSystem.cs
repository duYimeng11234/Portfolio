using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class BoundsSystem : MonoBehaviour
{
    public float boundsWidth = 8f;
    public float boundsHeight = 5f;
    public float bounceEnergy = 0.5f;
    private Spawner spawner;
    private LineRenderer lr;

    void Start()
    {
        spawner = GetComponent<Spawner>();
        lr = gameObject.AddComponent<LineRenderer>();
        lr.positionCount = 4;
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.loop = true;
        lr.useWorldSpace = true;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.orange;
        lr.endColor = Color.orange;
        DrawBorder();
    }

    void Update()
    {
        float halfW = boundsWidth / 2;
        float halfH = boundsHeight / 2;
        for(int i = 0; i < spawner.transforms.Count; i++)
        {
            Vector3 pos = spawner.transforms[i].position;
            Vector2 vel = spawner.velocities[i];
            if(pos.x < -halfW)
            {
                pos.x = -halfW;
                vel.x = Mathf.Abs(vel.x) * bounceEnergy;
            }
            if(pos.x > halfW)
            {
                pos.x = halfW;
                vel.x = -Mathf.Abs(vel.x) * bounceEnergy;
            }
            if(pos.y < -halfH)
            {
                pos.y = -halfH;
                vel.y = Mathf.Abs(vel.y) * bounceEnergy;
            }
            if(pos.y > halfH)
            {
                pos.y = halfH;
                vel.y = -Mathf.Abs(vel.y) * bounceEnergy;
            }
            spawner.transforms[i].position = pos;
            spawner.velocities[i] = vel;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(boundsWidth, boundsHeight, 0f));
    }

    void DrawBorder()
    {
        float halfW = boundsWidth / 2;
        float halfH = boundsHeight / 2;
        lr.SetPosition(0, new Vector3(-halfW, -halfH, 0));
        lr.SetPosition(1, new Vector3(-halfW, halfH, 0));
        lr.SetPosition(2, new Vector3(halfW, halfH, 0));
        lr.SetPosition(3, new Vector3(halfW, -halfH, 0));
    }
}

