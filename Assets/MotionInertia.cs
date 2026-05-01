using UnityEngine;

public class MotionInertia : MonoBehaviour
{
    [Header("Alcohol Level")]
    [SerializeField] private int beersDrunk = 0;
    [SerializeField] private int maxBeers = 7;

    public void DrinkBeer()
    {
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
        return Mathf.Clamp01((float)beersDrunk / maxBeers);
    }
}
