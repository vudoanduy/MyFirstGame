using System;
using TMPro;
using UnityEngine;

[Serializable]
public class CoinInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinInfo;

    public int coinTotal = 0;
    public static string SAVEDATA = "SAVEDATA_COIN";

    void Start(){
        LoadGame();
        UpdateCoin();
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.M)){
            SaveGame();
        }
    }

    public void AddCoin(int value){
        coinTotal += value;
        UpdateCoin();
    }
    public void UpdateCoin(){
        coinInfo.text = Convert.ToString(coinTotal);
    }
    public void SaveGame(){
        string jsonString = JsonUtility.ToJson(this);
        SaveSystem.SetString(SAVEDATA,jsonString);
        Debug.Log(jsonString);
    }
    public void LoadGame(){
        string jsonString = SaveSystem.GetString(SAVEDATA);
        if(jsonString != string.Empty)
        {
            SaveDataGame obj = JsonUtility.FromJson<SaveDataGame>(jsonString);
            this.coinTotal = obj.coinTotal;
        }
    }
    void OnApplicationQuit(){
        SaveGame();
    }
}
