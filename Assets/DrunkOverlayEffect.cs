using UnityEngine;
using UnityEngine.UI;

public class DrunkOverlayEffect : MonoBehaviour
{
    public Image overlayImage;
    public MotionInertia motionInertia;

    public float maxDarkness = 0.5f;

    void Start()
    {
        if (overlayImage == null)
            Debug.LogError("Overlay Image mangler!");

        if (motionInertia == null)
            Debug.LogError("Motion Inertia mangler!");
    }

    void Update()
    {
        if (overlayImage == null || motionInertia == null)
            return;

        float drunk = motionInertia.GetDrunkPercent();
        drunk = Mathf.Pow(drunk, 2f);

        Color color = overlayImage.color;
        color.a = Mathf.Lerp(0f, maxDarkness, drunk);
        color.r = 1f;
        color.g = Mathf.Lerp(1f, 0.65f, drunk);
        color.b = Mathf.Lerp(1f, 0.35f, drunk);

        overlayImage.color = color;
    }
}
