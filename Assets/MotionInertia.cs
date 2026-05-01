using UnityEngine;

// Stores and provides intoxication data for other scripts
public class MotionInertia : MonoBehaviour
{
    [Header("Alcohol Level")]
    [SerializeField] private int beersDrunk = 0;
    [SerializeField] private int maxBeers = 7;

    public void DrinkBeer()
    {
        // Increase intoxication, but clamp to max value
        beersDrunk++;
        if (beersDrunk > maxBeers)
            beersDrunk = maxBeers;

        Debug.Log("Beers drunk: " + beersDrunk);
    }

    public int GetBeersDrunk()
    {
        return beersDrunk;
    }

    public int GetMaxBeers()
    {
        return maxBeers;
    }

    public float GetDrunkPercent()
    {
        // Returns normalized value (0–1) used for effects
        return Mathf.Clamp01((float)beersDrunk / maxBeers);
    }
}