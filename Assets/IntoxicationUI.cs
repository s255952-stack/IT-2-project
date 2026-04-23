using UnityEngine;
using TMPro;

public class IntoxicationUI : MonoBehaviour
{
    public IntoxicationManager intoxicationManager;
    public TextMeshProUGUI intoxicationText;

    void Update()
    {
        int level = intoxicationManager.intoxicationLevel;

        intoxicationText.text = "Intoxication: " + level;

        // ændrer farve
        if (level < 3)
            intoxicationText.color = Color.green;
        else if (level < 6)
            intoxicationText.color = Color.yellow;
        else
            intoxicationText.color = Color.red;
    }
}


