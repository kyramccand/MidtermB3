using System.Collections;
using UnityEngine;

public class DestroyIceCreamStore : MonoBehaviour
{
    public GameObject breakVFX;          // drag Break prefab here
    public float vfxLifetime = 0.5f;
    public string carTag = "Car";

    public float shakeTime = 0.12f;
    public float shakeAmount = 0.06f;

    bool destroyed = false;
    Vector3 startPos;

    void Awake()
    {
        startPos = transform.position;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (destroyed) return;
        if (!other.gameObject.CompareTag(carTag)) return;

        destroyed = true;
        StartCoroutine(BreakSequence(other));
    }

    IEnumerator BreakSequence(Collision2D other)
    {
        // shake
        float t = 0f;
        while (t < shakeTime)
        {
            t += Time.deltaTime;
            transform.position = startPos + (Vector3)(Random.insideUnitCircle * shakeAmount);
            yield return null;
        }
        transform.position = startPos;

        // spawn break at contact point
        Vector2 hitPoint = other.GetContact(0).point;

        if (breakVFX != null)
        {
            GameObject fx = Instantiate(breakVFX, hitPoint, Quaternion.identity);
            Destroy(fx, vfxLifetime);
        }

        // remove store
        Destroy(gameObject);
    }
}