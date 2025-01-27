using UnityEngine;

public class MissTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object is a bubble
        if (other.CompareTag("Bubble"))
        {
            // Get reference to GameManager and penalize the player
            GameManager gameManager = FindFirstObjectByType<GameManager>();
            gameManager.LoseLife();

            // Destroy the missed bubble
            Destroy(other.gameObject);
        }
    }
}
