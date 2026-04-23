using UnityEngine;

public class IntoxicationManager : MonoBehaviour
{
    public int intoxicationLevel = 0;

    public void AddIntoxication(int amount)
    {
        intoxicationLevel += amount;
        Debug.Log("Intoxication level: " + intoxicationLevel);
    }
}


