using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

        public AudioSource MenuMusic;
        public AudioSource GameMusic1;
        //public AudioSource GameMusic2;

        private AudioSource theMusic;
        private static float musicTimeStamp = 22.8f;
        public float currentTimeStamp;

        void Awake(){
                //set the music based on the scene
                if ((SceneManager.GetActiveScene().name == "MainMenu")
                || (SceneManager.GetActiveScene().name == "Credits")
                || (SceneManager.GetActiveScene().name == "HowToPlay")
                ){
                        theMusic = MenuMusic;
                } else if (SceneManager.GetActiveScene().name == "Level1"){
                        theMusic = GameMusic1;
                        musicTimeStamp = 0f;
                } else if (SceneManager.GetActiveScene().name == "EndWin"){
                        theMusic = GameMusic1;
                        musicTimeStamp = 22.8f;
                }

                //set the time and play:
                
                if((SceneManager.GetActiveScene().name != "EndWin")){
                        if(SceneManager.GetActiveScene().name != "Level1"){
                                theMusic.time = musicTimeStamp;
                                theMusic.Play();
                        } 
                }
        }

        void Update(){
               //keep track of timestamp, to auto-call it in the next scene:
               musicTimeStamp = theMusic.time;
               currentTimeStamp = theMusic.time;
        }

//change timestamp (can be called by door code):
        public void SetTimeStamp(){
               musicTimeStamp = theMusic.time;
        }

        public void PlayMusicAtBegin(){
                theMusic.time = 0.0f;
                theMusic.Play();
        }
}