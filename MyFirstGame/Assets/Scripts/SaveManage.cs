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
    public int selectedSkin = 0;
    public int maxHP = 5;
    public float speed = 8f;
    public float force = 10f;
    public Vector3 scale = new Vector3(1,1,1);
    private int totalLevel = 0;

    public List<int> checkSkin = new List<int>(1);
    public List<int> myNumItem = new List<int>(1);

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
        SaveGame();
    }

    public void SetMaxLevel(int maxLevelCurrent){
        if(maxLevelCurrent > totalLevel) return;
        this.maxLevelCurrent = maxLevelCurrent;
        SaveGame();
    }
    public void SetSelectedSkin(int selectedSkin){
        this.selectedSkin = selectedSkin;
        SaveGame();
    }
    public void SetTotalLevel(int totalLevel){
        this.totalLevel = totalLevel;
        SaveGame();
    }
    public void SetMaxHP(int maxHP){
        this.maxHP = maxHP;
    }
    public void SetSpeed(float speed){
        this.speed = speed;
    }
    public void SetForce(float force){
        this.force = force;
    }
    public void SetScale(Vector3 scale){
        this.scale = scale;
    }
    public void SetCheckSkin(List<int> checkSkin){
        this.checkSkin = checkSkin;
        SaveGame();
    }
    public void SetMyNumItem(List<int> myNumItem){
        this.myNumItem = myNumItem;
        SaveGame();
    }

    public int GetCoinTotal(){
        return this.coinTotal;
    }

    public int GetMaxLevel(){
        return this.maxLevelCurrent;
    }
    public int GetSelectedSkin(){
        return this.selectedSkin;
    }
    public int GetTotalLevel(){
        return this.totalLevel;
    }
    public int GetMaxHP(){
        return this.maxHP;
    }
    public float GetSpeed(){
        return this.speed;
    }
    public float GetForce(){
        return this.force;
    }
    public Vector3 GetScale(){
        return this.scale;
    }
    public List<int> GetCheckSkin(){
        return this.checkSkin;
    }
    public List<int> GetMyNumItem(){
        return this.myNumItem;
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
            this.selectedSkin = obj.selectedSkin;
            this.checkSkin = obj.checkSkin;
        }
    }
    void OnApplicationQuit(){
        SaveGame();
    }
}
