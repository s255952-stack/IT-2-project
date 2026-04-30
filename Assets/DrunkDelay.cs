using UnityEngine;
using System.Collections.Generic;

public class DrunkDelay : MonoBehaviour
{
    public Transform cameraTransform;

    [Range(0f, 1f)]
    public float intoxicationLevel = 0f;

    public int maxBufferSize = 50;

    private Queue<Vector3> positionBuffer = new Queue<Vector3>();
    private Queue<Quaternion> rotationBuffer = new Queue<Quaternion>();

    void Start()
    {
        // Safety check (undgår errors)
        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform is NOT assigned in DrunkDelay!");
        }
    }

    void Update()
    {
        if (cameraTransform == null) return;

        // Gem nuværende position & rotation
        positionBuffer.Enqueue(cameraTransform.localPosition);
        rotationBuffer.Enqueue(cameraTransform.localRotation);

        // Hold buffer størrelse stabil
        if (positionBuffer.Count > maxBufferSize)
        {
            positionBuffer.Dequeue();
            rotationBuffer.Dequeue();
        }

        // Beregn delay baseret på intoxication
        int delayAmount = Mathf.RoundToInt(intoxicationLevel * maxBufferSize);

        if (positionBuffer.Count > delayAmount)
        {
            Vector3[] posArray = positionBuffer.ToArray();
            Quaternion[] rotArray = rotationBuffer.ToArray();

            // 🔥 Smooth overgang (bedre følelse)
            cameraTransform.localPosition = Vector3.Lerp(
                cameraTransform.localPosition,
                posArray[delayAmount],
                Time.deltaTime * 10f
            );

            cameraTransform.localRotation = Quaternion.Slerp(
                cameraTransform.localRotation,
                rotArray[delayAmount],
                Time.deltaTime * 10f
            );
        }
    }

    // 👉 Bruges af BeerPickup
    public void AddIntoxication(float amount)
    {
        intoxicationLevel += amount;
        intoxicationLevel = Mathf.Clamp01(intoxicationLevel);

        Debug.Log("Intoxication level: " + intoxicationLevel);
    }
}