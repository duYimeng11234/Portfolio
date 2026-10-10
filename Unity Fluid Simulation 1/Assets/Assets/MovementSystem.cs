using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    public float damping = 0.99f;
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
            spawner.velocities[i] *= damping;
            spawner.transforms[i].position += (Vector3)(spawner.velocities[i] * dt);
        }
    }
}
