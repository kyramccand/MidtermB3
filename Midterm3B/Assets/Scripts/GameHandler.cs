using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameHandler : MonoBehaviour {

    public static int timer = 0;
    public static bool timerLive = true;

    private float theTimer = 0f;
    public TMP_Text timerText;

    public GameObject changeTimeBG;


    void Start(){
        if(timerLive == true){
            timer = 0;
        }

        UpdateTimer();
    }

    void FixedUpdate(){
        if(timerLive == true){
            theTimer += 0.02f;
            if(theTimer >= 0.1f){
                timer += 1;
                theTimer = 0;
                UpdateTimer();
            }
        }
    }

    public void UpdateTimer(){
        int noDecimal = timer / 10;
        int yesDecimal = timer % 10;
        timerText.text = "TIME: " + noDecimal + "." + yesDecimal;
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
        StartCoroutine(endScene());
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

    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

        timer = 0;
        timerLive = true;
    }

    IEnumerator endScene(){
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("EndWin");
    }
}