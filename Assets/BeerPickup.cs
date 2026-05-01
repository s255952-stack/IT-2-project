using UnityEngine;

// Handles beer pickup based on distance to the player
public class BeerPickup : MonoBehaviour
{
    public Transform player;
    public float pickupDistance = 1.5f;

    void Update()
    {
        // Safety check to avoid null reference errors
        if (player == null) return;

        // Calculate distance between beer and player
        float distance = Vector3.Distance(transform.position, player.position);

        // If player is close enough, trigger pickup
        if (distance < pickupDistance)
        {
            // Find intoxication effect system 
            MotionInertia drunk = FindObjectOfType<MotionInertia>();

            if (drunk != null)
            {
                // Increase intoxication effects
                drunk.DrinkBeer();
            }

            Debug.Log("Beer picked up!");

            // Remove beer from scene after pickup
            Destroy(gameObject);
        }
    }
}
