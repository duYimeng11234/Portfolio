using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoxSpawner : MonoBehaviour
{
    public Vector2 spawnPosition = new Vector2(0f, 3.5f);
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private FluidBoxInteraction interaction;
    private bool hasSpawned = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        interaction = GetComponent<FluidBoxInteraction>();
        sr.enabled = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.position = spawnPosition;
        if(interaction != null)
        {
            interaction.enabled = false;
        }
    }

    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SpawnBox();
        }
        if(Keyboard.current.rKey.wasPressedThisFrame)
        {
            ResetBox();
        }
    }

    public void SpawnBox()
    {
        if(hasSpawned)
        {
            return;
        }
        hasSpawned = true;
        sr.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        if(interaction != null)
        {
            interaction.enabled = true;
        }
    }

    public void ResetBox()
    {
        hasSpawned = false;
        sr.enabled = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.position = spawnPosition;
        if(interaction != null)
        {
            interaction.enabled = false;
        }
    }
}
