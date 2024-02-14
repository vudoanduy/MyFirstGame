using System;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpInfo;

    ManageMusic manageMusic;
    ManageGUILevel manageGUILevel;
    CoinInfo coinInfo;
    Victory victory;

    protected int maxHP;
    protected int currentHP; // Money the player actually owns
    float timeNoDamage = 2f;

    void Start(){
        manageMusic = GameObject.Find("ManageAudio").GetComponent<ManageMusic>();
        manageGUILevel = GameObject.Find("ManageGUILevel").GetComponent<ManageGUILevel>();
        coinInfo = GameObject.Find("ManageCoin").GetComponent<CoinInfo>();
        victory = GameObject.Find("Victory").GetComponent<Victory>();
        currentHP = maxHP = 2;
        hpInfo.text = currentHP + " / " + maxHP;
    }

    void Update(){
        timeNoDamage += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other){
        switch(other.gameObject.tag){
            case "Enemy":
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
                break;
            case "Coin":
                manageMusic.SetSource(manageMusic.soundSource, manageMusic.collectCoin);
                Destroy(other.gameObject);
                coinInfo.AddCoin(1);
                break;
            case "Door":
                SetUnlockLevel();
                victory.VictoryLevel();
                break;
            default:
                break;
        }
    }
    public void MaxHP(){
        currentHP = maxHP;
        hpInfo.text = currentHP + " / " + maxHP;
        manageMusic.SetSource(manageMusic.musicSource, 1);
    }
    public void SetUnlockLevel(){
        int levelCurrent = manageGUILevel.GetLevelCurrent();
        int maxLevelCurrent = SaveManage.Instance.GetMaxLevel();
        if(levelCurrent < maxLevelCurrent || maxLevelCurrent == SaveManage.Instance.GetTotalLevel()){
            return;
        }
        SaveManage.Instance.SetMaxLevel(levelCurrent + 1);
    }
}
