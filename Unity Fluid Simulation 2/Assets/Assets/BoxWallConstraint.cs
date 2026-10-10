using UnityEngine;

public class BoxWallConstraint : MonoBehaviour
{
    public float boundsWidth = 8f;
    public float boundsHeight = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        Vector2 halfBox = transform.localScale / 2;
        float halfW = (boundsWidth / 2) - halfBox.x;
        float halfH = (boundsHeight / 2) - halfBox.y;
        Vector2 pos = rb.position;
        Vector2 vel = rb.linearVelocity;
        if(pos.x < -halfW)
        {
            pos.x = -halfW;
            vel.x = Mathf.Abs(vel.x) * 0.4f;
        }
        if(pos.x > halfW)
        {
            pos.x = halfW;
            vel.x = -Mathf.Abs(vel.x) * 0.4f;
        }
        if(pos.y < -halfH)
        {
            pos.y = -halfH;
            vel.y = Mathf.Abs(vel.y) * 0.4f;
        }
        if(pos.y > halfH)
        {
            pos.y = halfH;
            vel.y = Mathf.Abs(vel.y) * 0.4f;
        }
        rb.position = pos;
        rb.linearVelocity = vel;
    }
}
