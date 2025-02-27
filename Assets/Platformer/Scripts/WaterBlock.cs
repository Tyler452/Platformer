using UnityEngine;

public class WaterBlock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Mario died! He hit the water.");
        }
    }
}