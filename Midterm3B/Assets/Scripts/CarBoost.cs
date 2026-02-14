using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Movement")]
    public float baseSpeed = 8f;
    public float turnSpeed = 250f;

    [Header("Boost")]
    public float boostTimeCostPerSecond = 2f; // extra time drain per second while boosting

    float currentMultiplier = 1f;
    Coroutine boostRoutine;

    void Update()
    {
        float steer = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        transform.Rotate(0, 0, -steer * turnSpeed * Time.deltaTime);

        float speed = baseSpeed * currentMultiplier;
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void StartBoost(float duration, float multiplier)
    {
        if (boostRoutine != null) StopCoroutine(boostRoutine);
        boostRoutine = StartCoroutine(BoostRoutine(duration, multiplier));
    }

        IEnumerator BoostRoutine(float duration, float multiplier)
        {
            currentMultiplier = multiplier;
    
            float t = 0f;
            while (t < duration)
            {
                // Deduct extra time WHILE boosted
                if (GameTimer.Instance != null && GameTimer.Instance.timeRemaining > 0)
                {
                    GameTimer.Instance.timeRemaining -= boostTimeCostPerSecond * Time.deltaTime;
                }
                t += Time.deltaTime;
                yield return null;
            }
            currentMultiplier = 1f;
        }
    }
