using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    public GameObject breakEffect; // Assign a particle effect for breaking bricks

    private void OnCollisionEnter(Collision collision)
    {
        // Check if Mario is hitting the block from below
        if (IsHittingFromBelow(collision))
        {
            // Break brick if Mario hits it from below
            if (collision.gameObject.CompareTag("Brick"))
            {
                Destroy(collision.gameObject);
                Instantiate(breakEffect, collision.transform.position, Quaternion.identity);
                GameManager.Instance.AddPoints(100); // Add points for breaking a brick
            }

            // Hit question block if Mario hits it from below
            if (collision.gameObject.CompareTag("QuestionBlock"))
            {
                QuestionBlockAnimator questionBlock = collision.gameObject.GetComponent<QuestionBlockAnimator>();
                if (questionBlock != null)
                {
                    questionBlock.OnBlockHit();
                }
            }
        }
    }

    private bool IsHittingFromBelow(Collision collision)
    {
        // Check if Mario is hitting the block from below
        Vector3 hitDirection = (collision.transform.position - transform.position).normalized;
        return hitDirection.y > 0.5f; // Adjust the threshold as needed
    }
}