using UnityEngine;

public class MotionInertia : MonoBehaviour
{
    [Header("Alcohol Level")]
    public int beersDrunk = 0;
    public int maxBeers = 10;

    public void DrinkBeer()
    {
        beersDrunk++;

        if (beersDrunk > maxBeers)
            beersDrunk = maxBeers;

        Debug.Log("Beers drunk: " + beersDrunk);
    }

    public float GetDrunkPercent()
    {
        return Mathf.Clamp01((float)beersDrunk / maxBeers);
    }
}
