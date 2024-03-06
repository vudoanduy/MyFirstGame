using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGUILevel : MonoBehaviour
{
    [Header("Button when player died")]
    [SerializeField] GameObject revival, die;

    GameObject player;
    PlayerInfo playerInfo;

    int levelCurrent, maxHP;
    string nameLevel;

    //
    void Start(){
        SetUpStart();
        SetBool(false);
    }

    //
    public void SetUpStart(){
        SetParameter();
        InitializatingObject();
    }

    public void SetParameter(){
        nameLevel = SceneManager.GetActiveScene().name;

        bool hasNonDigit = nameLevel.Any(c => !char.IsDigit(c));

        if(hasNonDigit){
            nameLevel = nameLevel.Substring(0,nameLevel.Count()-1);
        }

        levelCurrent = int.Parse(nameLevel);

        maxHP = SaveManage.Instance.GetMaxHP();
    }

    public void InitializatingObject(){
        player = GameObject.Find("Player").gameObject;
        playerInfo = player.GetComponent<PlayerInfo>();
    }

    // Button
    private void SetBool(bool active){
        revival.SetActive(active);
        die.SetActive(active);
    }
    public void PlayerDie(){
        SetBool(true);
    }
    public void Revival(){
        player.SetActive(true);       
        SetBool(false);
        playerInfo.SetCurrentHP(maxHP);
    }

    
    public int GetLevelCurrent(){
        return this.levelCurrent;
    }
    public string GetNameLevel(){
        return nameLevel;
    }
}
