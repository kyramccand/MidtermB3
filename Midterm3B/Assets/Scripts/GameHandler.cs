using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameHandler : MonoBehaviour {

    public int timer = 0;
    private float theTimer = 0f;
    public TMP_Text timerText;

    public bool timerLive = true;

    public GameObject changeTimeBG;


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
        if(timerLive == true){
            int noDecimal = timer / 10;
            int yesDecimal = timer % 10;
            timerText.text = "TIME: " + noDecimal + "." + yesDecimal;
        }
    }

    public void AddTime(int points){
        if(timerLive == true){
            timer += points;
            UpdateTimer();

            changeTimeBG.SetActive(true);
            changeTimeBG.GetComponent<ChangeTimer>().Add(points);
        }
    }

    public void SubtractTime(int points){
        if(timerLive == true){
            timer -= points;
            UpdateTimer();

            changeTimeBG.SetActive(true);
            changeTimeBG.GetComponent<ChangeTimer>().Subtract(points);
        }
    }

    public void StopTimer(){
        timerLive = false;
    }

    public void StartGame() {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame() {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
      }

    public void Credits() {
        SceneManager.LoadScene("Credits");
    }

}