using UnityEngine;

public class IntoxicationVisualController : MonoBehaviour
{
    public IntoxicationManager intoxicationManager;
    public GameObject tunnelingVignette;

    void Update()
    {
        if (intoxicationManager == null || tunnelingVignette == null)
            return;

        float level = intoxicationManager.intoxicationLevel;

        // ON/OFF test først
        if (level > 0)
        {
            tunnelingVignette.SetActive(true);
        }
        else
        {
            tunnelingVignette.SetActive(false);
        }
    }
}