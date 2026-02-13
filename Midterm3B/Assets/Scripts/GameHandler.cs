using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHandler : MonoBehaviour {

    public int timer = 0;
    private float theTimer = 0f;
    public TMP_Text timerText;

    void Start(){
        UpdateTimer();
    }

    void FixedUpdate(){
        theTimer += 0.02f;
        if(theTimer >= 0.1f){
            timer += 1;
            theTimer = 0;
            UpdateTimer();
        }
    }

    public void UpdateTimer(){
        int noDecimal = timer / 10;
        int yesDecimal = timer % 10;
        timerText.text = "TIME: " + noDecimal + "." + yesDecimal;
    }

    public void AddTime(int points){
        timer += points;
        UpdateTimer();
    }

    public void SubtractTime(int points){
        timer -= points;
        UpdateTimer();
    }
}