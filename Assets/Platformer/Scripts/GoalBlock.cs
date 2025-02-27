using UnityEngine;

public class GoalBlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Level Complete! Mario reached the goal.");
            // Add logic to load the next level or show a victory screen
        }
    }
}