using UnityEngine;

// Simulates imbalance/head wobble based on the intoxication level
public class DrunkHeadWobble : MonoBehaviour
{
    public MotionInertia motionInertia;

    [Header("Position Wobble")]
    public float firstBeerPositionX = 0.02f;
    public float maxPositionX = 0.08f;

    [Header("Rotation Wobble")]
    public float firstBeerRotationZ = 2f;
    public float maxRotationZ = 8f;

    [Header("Speed")]
    public float wobbleSpeed = 2.0f;

    private Vector3 startLocalPosition;
    private Quaternion startLocalRotation;

    void Start()
    {
        // Store the original position and rotation, so the object can return to normal
        startLocalPosition = transform.localPosition;
        startLocalRotation = transform.localRotation;
    }

    void Update()
    {
        // Stop if the intoxication system is not assigned
        if (motionInertia == null) return;

        int beers = motionInertia.GetBeersDrunk();
        int maxBeers = motionInertia.GetMaxBeers();

        // If no beers have been collected, smoothly return to the original state
        if (beers <= 0)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, startLocalPosition, 4f * Time.deltaTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, startLocalRotation, 4f * Time.deltaTime);
            return;
        }

        // Calculate intoxication strength between 0 and 1
        float t = Mathf.Clamp01((float)(beers - 1) / Mathf.Max(1, maxBeers - 1));

        // Non-linear scaling makes the wobble increase more strongly at higher intoxication
        t = t * t;

        // Increase wobble amount based on intoxication level
        float currentPosX = Mathf.Lerp(firstBeerPositionX, maxPositionX, t);
        float currentRotZ = Mathf.Lerp(firstBeerRotationZ, maxRotationZ, t);

        // Use sine waves to create smooth side movement and rotation
        float posX = Mathf.Sin(Time.time * wobbleSpeed) * currentPosX;
        float rotZ = Mathf.Sin(Time.time * wobbleSpeed * 1.3f) * currentRotZ;

        // Create target position and rotation for the wobble effect
        Vector3 targetPos = startLocalPosition + transform.right * posX;
        Quaternion targetRot = startLocalRotation * Quaternion.Euler(0f, 0f, rotZ);

        // Smoothly move and rotate toward the wobble target
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 2f * Time.deltaTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, 2f * Time.deltaTime);
    }
}


