using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManageMusic : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundSource;
    [Header("Audio Clip")]
    public AudioClip backGround;
    public AudioClip gameOver;
    public AudioClip collectCoin;
    public AudioClip victory;
    [Header("State")]
    [SerializeField] TextMeshProUGUI stateMusic;
    [SerializeField] TextMeshProUGUI stateSound;
    int stateM, stateS;

    void Start(){
        musicSource.clip = backGround;
        musicSource.Play();
        musicSource.loop = true;
        musicSource.volume = soundSource.volume = 0.5f;
        stateM = stateS = 1;
        stateMusic.text = stateSound.text = "ON"; 
    }

    public void BtnMusic(){
        stateM = (stateM + 1) % 2;
        if(stateM == 0){
            musicSource.volume = 0f;
            stateMusic.text = "OFF";
        } else {
            musicSource.volume = 0.5f;
            stateMusic.text = "ON";
        }
    }
    public void BtnSound(){
        stateS = (stateS + 1) % 2;
        if(stateM == 0){
            soundSource.volume = 0f;
            stateSound.text = "OFF";
        } else {
            soundSource.volume = 0.5f;
            stateSound.text = "ON";
        }
    }
    public void GameOver(){
        musicSource.clip = gameOver;
        musicSource.Play();
        musicSource.loop = false;
    }
}
