using UnityEngine;

public class SpeedPanel : MonoBehaviour
{
    public float boostMultiplier = 1.6f;
    public float boostDuration = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("BOOST PANEL HIT by: " + other.name);

        CarController car = other.GetComponent<CarController>();
        if (car != null)
        {
            Debug.Log("CarController found! Starting boost.");
            car.StartBoost(boostDuration, boostMultiplier);
        }
        else
        {
            Debug.Log("NO CarController on the object that entered the trigger.");
        }
    }
}
