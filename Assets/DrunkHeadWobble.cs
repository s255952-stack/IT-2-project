using UnityEngine;

public class DrunkHeadWobble : MonoBehaviour
{
    public MotionInertia motionInertia;

    [Header("Position Wobble")]
    public float firstBeerPositionX = 0.002f;
    public float maxPositionX = 0.01f;

    [Header("Rotation Wobble")]
    public float firstBeerRotationZ = 0.3f;
    public float maxRotationZ = 1.5f;

    [Header("Speed")]
    public float wobbleSpeed = 1.0f;

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

        if (beers <= 0)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, startLocalPosition, 4f * Time.deltaTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, startLocalRotation, 4f * Time.deltaTime);
            return;
        }

        float t = Mathf.Clamp01((float)(beers - 1) / Mathf.Max(1, motionInertia.maxBeers - 1));

        // Blødere stigning i starten
        t = t * t;

        float currentPosX = Mathf.Lerp(firstBeerPositionX, maxPositionX, t);
        float currentRotZ = Mathf.Lerp(firstBeerRotationZ, maxRotationZ, t);

        float posX = Mathf.Sin(Time.time * wobbleSpeed) * currentPosX;
        float rotZ = Mathf.Sin(Time.time * wobbleSpeed * 1.3f) * currentRotZ;

        Vector3 targetPos = startLocalPosition + new Vector3(posX, 0f, 0f);
        Quaternion targetRot = startLocalRotation * Quaternion.Euler(0f, 0f, rotZ);

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, 2f * Time.deltaTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, 2f * Time.deltaTime);
    }
}


