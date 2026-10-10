using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse2D : MonoBehaviour
{
    public float diameter = 3;

    void Start()
    {
        transform.localScale = new Vector3(diameter, diameter, 1f);
    }

    void Update()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(Camera.main.transform.position.z)));
        mousePos.z = 0f;
        transform.position = mousePos;
    }
}