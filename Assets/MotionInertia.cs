using UnityEngine;

public class MotionInertia : MonoBehaviour
{
    [Header("Alcohol Level")]
    private int beersDrunk = 0;

    [SerializeField]
    private int maxBeers = 7;

    // Metode til at tilføje en øl
    public void DrinkBeer()
    {
        beersDrunk++;

        if (beersDrunk > maxBeers)
            beersDrunk = maxBeers;

        Debug.Log("Beers drunk: " + beersDrunk);
    }

    // Getter (kontrolleret adgang)
    public int GetBeersDrunk()
    {
        return beersDrunk;
    }

    // Procent (bruges til effekter)
    public float GetDrunkPercent()
    {
        return Mathf.Clamp01((float)beersDrunk / maxBeers);
    }
}