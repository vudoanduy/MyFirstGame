using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveManage : MonoBehaviour
{
    private static SaveManage instance;
    public static SaveManage Instance{
        get{return instance;}
        set{}
    }
    public int coinTotal = 0;
    public int maxLevelCurrent = 1;
    private int totalLevel = 0;
    public static string SAVEDATA = "SAVEDATA";
    

    void Awake(){
        if(instance == null){
            instance = this;
        } else{
            Destroy(gameObject);
        }
    }

    void Start(){
        LoadGame();
    }

    public void SetCoinTotal(int coinTotal){
        this.coinTotal = coinTotal;
        Debug.Log(this.coinTotal);
        Debug.Log(Instance.coinTotal);
    }

    public void SetMaxLevel(int maxLevelCurrent){
        if(maxLevelCurrent > totalLevel) return;
        this.maxLevelCurrent = maxLevelCurrent;
    }
    public void SetTotalLevel(int totalLevel){
        this.totalLevel = totalLevel;
    }

    public int GetCoinTotal(){
        return this.coinTotal;
    }

    public int GetMaxLevel(){
        return this.maxLevelCurrent;
    }
    public int GetTotalLevel(){
        return this.totalLevel;
    }

    public void SaveGame(){
        string jsonString = JsonUtility.ToJson(SaveManage.Instance);
        Debug.Log(jsonString);
        SaveSystem.SetString(SAVEDATA,jsonString);
    }
    public void LoadGame(){
        string jsonString = SaveSystem.GetString(SAVEDATA);
        Debug.Log(jsonString);
        if(jsonString != string.Empty)
        {
            SaveManageData obj = JsonUtility.FromJson<SaveManageData>(jsonString);
            this.coinTotal = obj.coinTotal;
            this.maxLevelCurrent = obj.maxLevelCurrent;
        }
    }
    void OnApplicationQuit(){
        SaveGame();
    }
}
