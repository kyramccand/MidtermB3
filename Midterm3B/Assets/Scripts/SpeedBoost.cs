using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered has the tag "Player"
        if (other.CompareTag("PlayerCar"))
        {
            Debug.Log("Player has crossed the object!");
            // Add your logic here (e.g., load scene, play sound, open door)
        }
    }
}
