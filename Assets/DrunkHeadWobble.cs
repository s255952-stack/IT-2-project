using UnityEngine;

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
        startLocalPosition = transform.localPosition;
        startLocalRotation = transform.localRotation;
    }

    void Update()
    {
        if (motionInertia == null) return;

        int beers = motionInertia.beersDrunk;

        // Hvis ingen øl → gå tilbage til normal
        if (beers <= 0)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, startLocalPosition, 4f * Time.deltaTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, startLocalRotation, 4f * Time.deltaTime);
            return;
        }

        float t = Mathf.Clamp01((float)(beers - 1) / Mathf.Max(1, motionInertia.maxBeers - 1));
        t = t * t; // blødere start

        float currentPosX = Mathf.Lerp(firstBeerPositionX, maxPositionX, t);
        float currentRotZ = Mathf.Lerp(firstBeerRotationZ, maxRotationZ, t);

        float posX = Mathf.Sin(Time.time * wobbleSpeed) * currentPosX;
        float rotZ = Mathf.Sin(Time.time * wobbleSpeed * 1.3f) * currentRotZ;

        // 🔥 VIGTIGT: bevæg parent (ikke kamera direkte)
        Vector3 targetPos = startLocalPosition + transform.right * posX;
        Quaternion targetRot = startLocalRotation * Quaternion.Euler(0f, 0f, rotZ);

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 2f * Time.deltaTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, 2f * Time.deltaTime);
    }
}


