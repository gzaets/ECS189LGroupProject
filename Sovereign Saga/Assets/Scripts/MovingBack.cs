using UnityEngine;

public class MovingBack : MonoBehaviour
{
    public float scrollSpeed = 1.0f;
    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        float offsetX = Time.time * scrollSpeed;
        Vector2 offset = new Vector2(offsetX, 0);
        renderer.material.mainTextureOffset = offset;
    }
}