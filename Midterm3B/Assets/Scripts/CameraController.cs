using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform target;

    void Update () {
        transform.position = new Vector3 (
            transform.position.x,
                // Mathf.Clamp(targetToFollow.positon.y, 0f, 10f),
                transform.position.y,
                transform.position.z);
    }
}
