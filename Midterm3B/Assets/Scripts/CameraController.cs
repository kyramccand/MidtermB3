using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;

    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x, player.position.y, -1);
        }
    }
}
