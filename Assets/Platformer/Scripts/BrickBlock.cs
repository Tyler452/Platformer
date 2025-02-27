using UnityEngine;

public class BrickBlock : MonoBehaviour
{
    public GameObject brokenBrickPrefab; // Assign the broken brick prefab in the Inspector

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player hit the brick!");
            if (IsHittingFromBelow(collision))
            {
                Debug.Log("Player hit the brick from below!");
                BreakBrick();
            }
        }
    }

    private void BreakBrick()
    {
        Debug.Log("Breaking the brick!");

        // Replace the brick with the broken version
        if (brokenBrickPrefab != null)
        {
            Instantiate(brokenBrickPrefab, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogError("Broken brick prefab is not assigned!");
        }

        // Add points via GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddPoints(100);
        }
        else
        {
            Debug.LogError("GameManager instance is missing!");
        }

        // Destroy the original brick
        Destroy(gameObject);
    }

    private bool IsHittingFromBelow(Collision collision)
    {
        // Get the contact point between Mario and the brick
        ContactPoint contact = collision.contacts[0];
        Vector3 hitDirection = (contact.point - transform.position).normalized;

        // Debug the hit direction
        Debug.Log("Hit Direction: " + hitDirection);

        // Check if the hit direction is mostly upward
        return hitDirection.y > 0.5f; // Adjust the threshold if needed
    }
}