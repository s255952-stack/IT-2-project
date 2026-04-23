using UnityEngine;

public class GroundSnap : MonoBehaviour
{
    public CharacterController controller;
    public LayerMask groundLayer;
    public float rayStartHeight = 1.0f;
    public float rayLength = 3.0f;
    public float snapSpeed = 10f;
    public float maxSnapDistance = 0.5f;

    void Update()
    {
        if (controller == null) return;

        Vector3 rayOrigin = transform.position + Vector3.up * rayStartHeight;

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, rayLength, groundLayer))
        {
            float groundY = hit.point.y;
            float currentY = transform.position.y;
            float distanceToGround = currentY - groundY;

            // Snap kun hvis vi er lidt over jorden
            if (distanceToGround > 0.01f && distanceToGround < maxSnapDistance)
            {
                Vector3 targetPosition = new Vector3(
                    transform.position.x,
                    groundY,
                    transform.position.z
                );

                transform.position = Vector3.Lerp(
                    transform.position,
                    targetPosition,
                    snapSpeed * Time.deltaTime
                );
            }
        }
    }
}
