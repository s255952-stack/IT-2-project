using UnityEngine;
using UnityEngine.XR;

// Handles simple forward movement in VR using controller input
public class SimpleVRLocomotion : MonoBehaviour
{
    public Transform head;
    public float speed = 2f;

    private CharacterController controller;
    private InputDevice rightHand;

    void Start()
    {
        // Get CharacterController used for movement
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Ensure we have a valid right-hand controller
        if (!rightHand.isValid)
        {
            rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }

        // Move forward when trigger is pressed
        if (rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed) && triggerPressed)
        {
            // Use head direction, but ignore vertical movement
            Vector3 direction = head.forward;
            direction.y = 0;
            direction.Normalize();

            controller.Move(direction * speed * Time.deltaTime);
        }
    }
}
