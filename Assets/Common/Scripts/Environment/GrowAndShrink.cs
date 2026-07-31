using UnityEngine;

public class GrowAndShrink : MonoBehaviour
{
    [SerializeField, Min(0.01f)] private float minScale = 0.5f;
    [SerializeField, Min(0.01f)] private float maxScale = 2f;
    [SerializeField] private float frequency = 2f;

    private Vector3 startingScale;

    private void OnValidate()
    {
        maxScale = Mathf.Max(minScale, maxScale);
    }

    private void Start()
    {
        startingScale = transform.localScale;
    }

    private void Update()
    {
        float t = (Mathf.Sin(Time.time * frequency) + 1f) / 2f;

        float currentScale = Mathf.Lerp(minScale, maxScale, t);

        transform.localScale = startingScale * currentScale;
    }
}