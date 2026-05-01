using UnityEngine;
using UnityEngine.UI;

// Controls a visual intoxication overlay (mainly for simulation)
public class DrunkOverlayEffect : MonoBehaviour
{
    public Image overlayImage;
    public MotionInertia motionInertia;

    public float maxDarkness = 0.5f;

    void Start()
    {
        // Check that required references are assigned
        if (overlayImage == null)
            Debug.LogError("Overlay Image mangler!");

        if (motionInertia == null)
            Debug.LogError("Motion Inertia mangler!");
    }

    void Update()
    {
        // Skip if references are missing
        if (overlayImage == null || motionInertia == null)
            return;

        // Get intoxication level (0–1)
        float drunk = motionInertia.GetDrunkPercent();

        // Non-linear scaling to make effect stronger at higher levels
        drunk = Mathf.Pow(drunk, 2f);

        Color color = overlayImage.color;

        // Adjust transparency to create darkening effect
        color.a = Mathf.Lerp(0f, maxDarkness, drunk);

        // Adjust colors to create warm/yellow intoxication tint
        color.r = 1f;
        color.g = Mathf.Lerp(1f, 0.65f, drunk);
        color.b = Mathf.Lerp(1f, 0.35f, drunk);

        // Apply updated color to overlay
        overlayImage.color = color;
    }
}