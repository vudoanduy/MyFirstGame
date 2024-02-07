using TMPro;
using UnityEngine;

public class ManageMusic : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource soundSource;
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
        SetSource(musicSource,backGround);
        musicSource.loop = true;
        musicSource.volume = soundSource.volume = 0.5f;
        stateM = stateS = 1;
        stateMusic.text = stateSound.text = "ON"; 
    }

    public void BtnMusic(){
        stateM = (stateM + 1) % 2;
        if(stateM == 0){
            SetVolumn(musicSource, 0, stateMusic, "OFF");
        } else {
            SetVolumn(musicSource, 0.5f, stateMusic, "ON");
        }
    }
    public void BtnSound(){
        stateS = (stateS + 1) % 2;
        if(stateM == 0){
            SetVolumn(soundSource, 0, stateMusic, "OFF");
        } else {
            SetVolumn(soundSource, 0.5f, stateMusic, "ON");
        }
    }
    public void GameOver(){
        SetSource(soundSource, gameOver);
        SetSource(musicSource, 0);
    }
    public void Victory(){
        SetSource(soundSource, victory);
        SetSource(musicSource, 0);
    }
    public void CollectCoin(){
        SetSource(soundSource, collectCoin);
    }

    // ----------//
    public void SetSource(AudioSource audioSource, AudioClip audioClip){
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    public void SetSource(AudioSource audioSource,int isPause){
        if(isPause == 0){
            audioSource.Pause();
        } else {
            audioSource.UnPause();
        }
    }
    public void SetVolumn(AudioSource audioSource, float value, TextMeshProUGUI textMeshProUGUI, string state){
        audioSource.volume = value;
        textMeshProUGUI.text = state;
    }
}
