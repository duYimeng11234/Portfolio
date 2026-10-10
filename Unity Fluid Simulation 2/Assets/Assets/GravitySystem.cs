using UnityEngine;

public class GravitySystem : MonoBehaviour
{
    public float gravity = -9.8f;
    private Spawner spawner;
    void Start()
    {
        spawner = GetComponent<Spawner>();
    }

    void Update()
    {
        float dt = Time.deltaTime;
        for(int i = 0; i < spawner.transforms.Count; i++)
        {
            spawner.velocities[i] += new Vector2(0f, gravity * dt);
        }
    }
}
