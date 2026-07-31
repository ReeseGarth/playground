using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
    [SerializeField] private float amplitude = 2f;
    [SerializeField] private float frequency = 2f;

    private Vector3 startingPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        float verticalOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        transform.position = startingPosition + Vector3.up * verticalOffset;
    }
}
