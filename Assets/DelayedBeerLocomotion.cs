using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class DelayedBeerLocomotion : MonoBehaviour
{
    public Transform head;
    public MotionInertia motionInertia;

    public float moveSpeed = 2f;
    public float delayPerBeer = 2f;
    public float maxDelay = 6f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private InputDevice leftHand;
    private Queue<InputFrame> inputBuffer = new Queue<InputFrame>();

    private float verticalVelocity;

    private struct InputFrame
    {
        public Vector2 input;
        public float time;

        public InputFrame(Vector2 input, float time)
        {
            this.input = input;
            this.time = time;
        }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller == null || head == null || motionInertia == null) return;

        if (!leftHand.isValid)
        {
            leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        }

        Vector2 currentInput = Vector2.zero;
        leftHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out currentInput);

        inputBuffer.Enqueue(new InputFrame(currentInput, Time.time));

        float delay = motionInertia.beersDrunk * delayPerBeer;
        delay = Mathf.Clamp(delay, 0f, maxDelay);

        Vector2 delayedInput = Vector2.zero;

        while (inputBuffer.Count > 0 && Time.time - inputBuffer.Peek().time >= delay)
        {
            delayedInput = inputBuffer.Dequeue().input;
        }

        Vector3 forward = head.forward;
        Vector3 right = head.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * delayedInput.y + right * delayedInput.x;

        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -1f;

        verticalVelocity += gravity * Time.deltaTime;
        move.y = verticalVelocity;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}
