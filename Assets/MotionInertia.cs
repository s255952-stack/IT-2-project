using UnityEngine;

public class MotionInertia : MonoBehaviour
{
    [Header("Drunk Level")]
    public int beersDrunk = 0;

    [Header("Settings")]
    public float maxSwayAmount = 3f;
    public float maxSwaySpeed = 2f;

    private float currentSwayAmount;
    private float currentSwaySpeed;

    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.localRotation;
    }

    void Update()
    {
        // Skaler effekt ud fra antal øl
        float t = Mathf.Clamp01(beersDrunk / 10f);

        currentSwayAmount = Mathf.Lerp(0f, maxSwayAmount, t);
        currentSwaySpeed = Mathf.Lerp(0.5f, maxSwaySpeed, t);

        float swayZ = Mathf.Sin(Time.time * currentSwaySpeed) * currentSwayAmount;

        transform.localRotation = startRotation * Quaternion.Euler(0f, 0f, swayZ);
    }

    public void DrinkBeer()
    {
        beersDrunk++;
        Debug.Log("Beers: " + beersDrunk);
    }
}


