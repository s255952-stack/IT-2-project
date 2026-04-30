using UnityEngine;
using UnityEngine.XR;

public class SimpleVRLocomotion : MonoBehaviour
{
    public Transform head;
    public float speed = 2f;

    private CharacterController controller;
    private InputDevice rightHand;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!rightHand.isValid)
        {
            rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }

        if (rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed) && triggerPressed)
        {
            Vector3 direction = head.forward;
            direction.y = 0;
            direction.Normalize();

            controller.Move(direction * speed * Time.deltaTime);
        }
    }
}
