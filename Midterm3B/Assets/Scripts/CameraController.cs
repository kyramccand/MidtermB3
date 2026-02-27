using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform store;
    public float smoothTime = 0.25f;
    public float panDuration = 2f;
    public float waitDuration = 1f;

    private Vector3 velocity = Vector3.zero;
    private float waitTime = 0f;
    private float elapsedTime = 0f;
    private bool isPanning = false;
    private bool isWaiting = true;

    void Start() {
        transform.position = new Vector3(store.position.x, store.position.y, -1);
    }

    void LateUpdate()
    {
        if (player == null) {
            return;
        }

        // Wait at the store for the desired amount of time
        if (isWaiting) {
            waitTime += Time.deltaTime;
            if (waitTime >= waitDuration) {
                isWaiting = false;
                isPanning = true;
            }
            return;
        }

        // Then pan smoothly to the truck in the desired amount of time
        if (isPanning) {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime/panDuration;
            float smoothT = Mathf.SmoothStep(0f, 1f, t); // ease in/out
            // https://docs.unity3d.com/ScriptReference/Mathf.SmoothStep.html

            Vector3 target = Vector3.Lerp(store.position, player.position, smoothT);
            // https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
            transform.position = new Vector3(target.x, target.y, -1);

            if (t >= 1f) isPanning = false;
        }   
        
        // After that, follow the player for the rest of the game!
        else {
            transform.position = new Vector3(player.position.x, player.position.y, -1);
        }
    }
}