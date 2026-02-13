using System.Collections.Generic;
using System.Collections;
using UnityEngine;

//This script goes onto the Player object
public class CarController_TopDown : MonoBehaviour {

    [Header("Car settings")]
    public float turnFactor = 5f;
    public float speed = 1f;

     // Local Variables
    Vector2 inputVector;
    float steeringInput = 0f;
    float rotationAngle = 0f;
    string direction = "Right";

    // Components
    Rigidbody2D carRb2D;

    void Awake(){
        carRb2D = GetComponent<Rigidbody2D>();
    }

    void Update() {
        inputVector = Vector2.zero;
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        SetInputVector(inputVector);
     }

    void FixedUpdate(){
          ApplySteering();
     }

     void ApplySteering(){
          rotationAngle -= steeringInput * turnFactor;
          float angleInRadians = (rotationAngle + 90) * Mathf.Deg2Rad;
          // Apply rotation to car
          
          float xVelocity = Mathf.Cos(angleInRadians) * speed;
          float yVelocity = Mathf.Sin(angleInRadians) * speed;
          Vector2 move = new Vector2(xVelocity * Time.fixedDeltaTime, yVelocity * Time.fixedDeltaTime);
          carRb2D.MoveRotation(rotationAngle);
          carRb2D.MovePosition(carRb2D.position + move);         
     }

     void SetInputVector(Vector2 inputVector){
          steeringInput = inputVector.x;
     }
}