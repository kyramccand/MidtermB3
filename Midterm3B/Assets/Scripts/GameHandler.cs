using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameHandler : MonoBehaviour {

    public static int timer = 0;
    public static bool timerLive = false;
    public static bool newGame = true;
    public static bool gameGo = false;

    private float theTimer = 0f;
    public TMP_Text timerText;
    public TMP_Text introText;

    public GameObject changeTimeBG;
    public GameObject countdown;

    public AudioManager audioManager;

    public static int buildingsHit = 0;
    public static int peopleHit = 0;
    public static int carsHit = 0;
    public static int bonusConesGot = 0;

    public AudioSource lowSFX;
    public AudioSource highSFX;


    void Start() {
        if(newGame == true){
            timer = 0;
            buildingsHit = 0;
            peopleHit = 0;
            carsHit = 0;
            bonusConesGot = 0;
            theTimer = 0f;
        }
        introText.text = "Destroy the Ice Cream Store!";
        StartCoroutine(startCountdown());
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
    }

    public void WinGame(){
        StopTimer();
        newGame = false;
        StartCoroutine(endScene());
    }

    public void StartGame() {
        gameGo = true;
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

    public void HowToPlay() {
        SceneManager.LoadScene("HowToPlay");
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        timer = 0;
        newGame = true;
        gameGo = false;
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator startCountdown() {
        if(newGame == true && gameGo == true){
            countdown.SetActive(true);
            countdown.GetComponent<ChangeCountdown>().Number("3");
            lowSFX.Play();
            yield return new WaitForSeconds(1f);
            countdown.GetComponent<ChangeCountdown>().Number("2");
            lowSFX.Play();
            yield return new WaitForSeconds(1f);
            countdown.GetComponent<ChangeCountdown>().Number("1");
            lowSFX.Play();
            yield return new WaitForSeconds(1f);
            
            // Hide the message after the countdown ends
            introText.enabled = false;
            
            countdown.GetComponent<ChangeCountdown>().Go();
            highSFX.Play();
            yield return new WaitForSeconds(0.5f);
        
        
            audioManager.PlayMusicAtBegin();
            timerLive = true;
            gameGo = false;
        }
    }

    IEnumerator endScene(){
        gameGo = false;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("EndWin");
    }
}