using System;
using TMPro;
using UnityEngine;

[Serializable]
public class PlayerInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpInfo;

    ManageMusic manageMusic;
    ManageGUILevel manageGUILevel;
    CoinInfo coinInfo;

    protected int maxHP;
    protected int currentHP; // Money the player actually owns
    float timeNoDamage = 2f;

    void Start(){
        manageMusic = GameObject.Find("ManageAudio").GetComponent<ManageMusic>();
        manageGUILevel = GameObject.Find("ManageGUILevel").GetComponent<ManageGUILevel>();
        coinInfo = GameObject.Find("ManageCoin").GetComponent<CoinInfo>();
        currentHP = maxHP = 2;
        hpInfo.text = currentHP + " / " + maxHP;
    }

    void Update(){
        timeNoDamage += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){
            if(timeNoDamage < 2f) return;
            timeNoDamage = 0;
            currentHP--;
            hpInfo.text = currentHP + " / " + maxHP;
            if(currentHP == 0){
                gameObject.SetActive(false);
                Debug.Log("Game Over");
                manageMusic.GameOver();
                manageGUILevel.PlayerDie();
            }
        }
        if(other.gameObject.tag == "Coin"){
            manageMusic.SetSource(manageMusic.soundSource, manageMusic.collectCoin);
            Destroy(other.gameObject);
            coinInfo.AddCoin(1);
        }
    }
    public void MaxHP(){
        currentHP = maxHP;
        hpInfo.text = currentHP + " / " + maxHP;
        manageMusic.SetSource(manageMusic.musicSource, 1);
    }
}
