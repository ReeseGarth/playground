using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class RandomColorEverySecond : MonoBehaviour
{
    [SerializeField, Min(0.01f)] private float interval = 1f;

    private Renderer objectRenderer;
    private float timer;

    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            ChangeColor();
            timer -= interval;
        }
    }

    private void ChangeColor()
    {
        objectRenderer.material.color = Random.ColorHSV();
    }
}