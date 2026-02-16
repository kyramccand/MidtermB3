using System.Collections.Generic;
using System.Collections;
using UnityEngine;

//This script goes onto the Player object
public class CarController_TopDown : MonoBehaviour {

    public GameHandler gameHandlerObj;

    [Header("Car settings")]
    public float turnSensitivity = 5f;
    public float maxSpeed = 5f;
    public float maxReverse = -3f;
    public float speed = 1f;
    public float maxSteering = 10f;
    public float minSteering = -10f;
    public float slowDown = 0.05f;
    public float speedBoost = 2f;

     // Local Variables
    Vector2 inputVector;
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
        
        // Update driving speed
        UpdateSpeed(inputVector);
     }

    void FixedUpdate(){
        ApplySteering();
     }

    void UpdateSpeed(Vector2 direction) {
        if (speed > maxSpeed) {
        speed -= slowDown;
        }
        else if (speed < maxReverse) {
          speed += slowDown;
        }
        if (direction.y > 0) {
            if (speed < maxSpeed) {
                speed += 0.01f;
            }
        }
        else if (direction.y < 0) {
            if (speed > 0) {
                speed -= 0.01f;
            }
            else if (speed > maxReverse) {
                speed -= 0.005f;
            }
        }
    }

     void ApplySteering(){
          rotationAngle -= inputVector.x * turnSensitivity;
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
        }else if(other.gameObject.tag == "Pedestrian"){
          gameHandlerObj.AddTime(100);
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