using System.Collections.Generic;
using System.Collections;
using UnityEngine;

//This script goes onto the Player object
public class CarController_TopDown : MonoBehaviour {

    public GameHandler gameHandlerObj;

    [Header("Car settings")]
    public float turnFactor = 0.1f;
    public float maxSpeed = 5f;
    public float maxReverse = -3f;
    public float speed = 1f;
    public float maxSteering = 10f;
    public float minSteering = -10f;
    public float slowDown = 0.05f;
    public float speedBoost = 2f;

     // Local Variables
    Vector2 inputVector;
    float steeringInput = 0f;
    float rotationAngle = 0f;

    // Components
    Rigidbody2D carRb2D;

    void Awake(){
        carRb2D = GetComponent<Rigidbody2D>();
        if(GameObject.FindWithTag("GameHandler") != null){
          gameHandlerObj = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        }
    }

    void Update() {
        inputVector = Vector2.zero;
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        
        // Update driving direction
        UpdateSteering(inputVector);
        UpdateSpeed();
     }

    void FixedUpdate(){
        ApplySteering();
     }

    
    void UpdateSpeed() {
      if (speed > maxSpeed) {
        speed -= slowDown;
      }
      else if (speed < maxReverse) {
        speed += slowDown;
      }
    }

    void UpdateSteering(Vector2 direction) {
      if (direction.y > 0) {
        if (speed < maxSpeed) {
          speed += 0.01f;
        }
      }
      if (direction.x < 0) { // Detect a change to turning left
        if (steeringInput > minSteering) {
          steeringInput -= 0.01f;
        }
      }
      else if (direction.x > 0) { // Detect a change to turning right
        if (steeringInput < maxSteering) {
          steeringInput += 0.01f;
        }
      }
      if (direction.y < 0) {
        steeringInput = 0;
          if (speed > 0) {
            speed -= 0.01f;
          }
          else if (speed > maxReverse) {
            speed -= 0.005f;
          }
      }
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

     void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Building"){
          gameHandlerObj.AddTime(50);
          speed = 0;
        }else if(other.gameObject.tag == "IceCreamStore"){
          speed = 0;
          gameHandlerObj.StopTimer();
        }else if(other.gameObject.tag == "BonusCone"){
          gameHandlerObj.SubtractTime(10);
          Destroy(other.gameObject);
        }
     }

     void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "SpeedPanel")
        {
          if (speed > 0) {
            speed += speedBoost;
          }
          else if (speed < 0) {
            speed -= speedBoost;
          }
        }
    }
}