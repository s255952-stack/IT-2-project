using UnityEngine;
using UnityEngine.XR;

public class DelayedBeerLocomotion : MonoBehaviour
{
    public Transform head;
    public MotionInertia motionInertia;

    [Header("Movement Speed")]
    public float normalMoveSpeed = 5f;
    public float minimumMoveSpeed = 0.2f;

    [Header("Gravity")]
    public float gravity = -9.81f;

    private CharacterController controller;
    private InputDevice leftHand;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (controller == null)
            Debug.LogError("CharacterController mangler på samme objekt som DelayedBeerLocomotion!");

        if (head == null)
            Debug.LogError("Head er ikke sat i Inspector!");

        if (motionInertia == null)
            Debug.LogError("MotionInertia er ikke sat i Inspector!");
    }

    void Update()
    {
        if (controller == null || head == null || motionInertia == null)
            return;

        if (!leftHand.isValid)
            leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        Vector2 input = Vector2.zero;
        leftHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out input);

        Vector3 forward = head.forward;
        Vector3 right = head.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * input.y + right * input.x;

        float drunkPercent = motionInertia.GetDrunkPercent();
        float currentSpeed = Mathf.Lerp(normalMoveSpeed, minimumMoveSpeed, drunkPercent);

        if (controller.isGrounded && verticalVelocity < 0f)
            verticalVelocity = -1f;

        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;

        controller.Move(move * currentSpeed * Time.deltaTime);

        Debug.Log("Drunk %: " + drunkPercent + " | Current speed: " + currentSpeed);
    }
}


