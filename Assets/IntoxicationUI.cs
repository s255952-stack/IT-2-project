using UnityEngine;
using TMPro;

public class IntoxicationUI : MonoBehaviour
{
    public MotionInertia motionInertia;
    public TextMeshProUGUI intoxicationText;

    void Update()
    {
        if (motionInertia == null || intoxicationText == null) return;

        int level = motionInertia.GetBeersDrunk();
        intoxicationText.text = "Intoxication: " + level;

        if (level < 3)
            intoxicationText.color = Color.green;
        else if (level < 6)
            intoxicationText.color = Color.yellow;
        else
            intoxicationText.color = Color.red;
    }
}
