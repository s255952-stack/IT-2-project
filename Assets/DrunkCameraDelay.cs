using UnityEngine;

public class DrunkCameraDelay : MonoBehaviour
{
    public Transform targetCamera; // din XR Main Camera

    [Range(0f, 1f)]
    public float delayStrength = 0.1f; // hvor “fuld” man er

    private Quaternion currentRotation;

    void Start()
    {
        currentRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (targetCamera == null) return;

        // Smooth follow (delay effekt)
        currentRotation = Quaternion.Slerp(
            currentRotation,
            targetCamera.rotation,
            1f - delayStrength
        );

        transform.rotation = currentRotation;
    }
}