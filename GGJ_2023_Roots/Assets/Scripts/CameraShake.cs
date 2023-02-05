using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float swayMagnitude = 0.1f;
    public float swaySpeed = 1.0f;
    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        transform.localPosition = initialPosition + new Vector3(Mathf.PerlinNoise(Time.time * swaySpeed, 0f) - 0.5f, Mathf.PerlinNoise(0f, Time.time * swaySpeed) - 0.5f, 0f) * swayMagnitude;
    }
}