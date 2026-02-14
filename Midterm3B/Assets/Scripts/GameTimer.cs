using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;

    public float timeRemaining = 60f;
    public bool isRunning = true;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!isRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            isRunning = false;
            Debug.Log("TIME UP!");
        }
    }

    public void DeductTime(float amount)
    {
        timeRemaining = Mathf.Max(0f, timeRemaining - amount);
    }
}
