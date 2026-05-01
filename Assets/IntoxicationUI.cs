using UnityEngine;
using TMPro;

// Displays current intoxication level for testing/debugging
public class IntoxicationUI : MonoBehaviour
{
    public MotionInertia motionInertia;
    public TextMeshProUGUI intoxicationText;

    void Update()
    {
        // Skip if references are missing
        if (motionInertia == null || intoxicationText == null) return;

        // Get current intoxication level
        int level = motionInertia.GetBeersDrunk();
        intoxicationText.text = "Intoxication: " + level;

        // Change color based on intoxication level
        if (level < 3)
            intoxicationText.color = Color.green;
        else if (level < 6)
            intoxicationText.color = Color.yellow;
        else
            intoxicationText.color = Color.red;
    }
}