using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake : MonoBehaviour
{
    public float power, duration, slowDownAmount;
    public Transform camera_1;
    public bool shouldShake;

    Vector3 startPosition;
    float initialDuration;

    private void Start()
    {
        camera_1 = this.transform;
        startPosition = camera_1.localPosition;
        initialDuration = duration;
        shouldShake = true;
    }

    private void Update()
    {
        if (shouldShake)
        {
            if (duration > 0)
            {
                camera_1.localPosition = startPosition + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                camera_1.localPosition = startPosition;
            }
        }
    }
}
