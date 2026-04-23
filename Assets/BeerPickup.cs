using UnityEngine;

public class BeerPickup : MonoBehaviour
{
    public Transform player;
    public float pickupDistance = 1.5f;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < pickupDistance)
        {
            MotionInertia drunk = FindObjectOfType<MotionInertia>();

            if (drunk != null)
            {
                drunk.DrinkBeer();
            }

            Destroy(gameObject);
        }
    }
}

