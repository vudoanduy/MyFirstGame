using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

[Serializable]
public class ManageLevel : MonoBehaviour
{
    [SerializeField] GameObject listPage;

    GameObject[] miniPage;

    int countPage;
    int totalLevel = 0;
    public int maxLevelCurrent = 1;
    public static string SAVEDATA = "SAVEDATA_LEVEL";

    List<GameObject> levels;
    List<TextMeshProUGUI> texts;

    void Awake(){
        LoadGame();
    }
    void Start(){
        countPage = listPage.transform.childCount;
        miniPage = new GameObject[countPage];
        levels = new List<GameObject>();
        texts = new List<TextMeshProUGUI>();
        SetUpStart();
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.M)){
            if(maxLevelCurrent == totalLevel) return;
            maxLevelCurrent++;
            UnlockLevel(maxLevelCurrent);
        }
    }

    public void SetUpStart(){
        for(int i = 0; i < countPage; i++){
            miniPage[i] = listPage.transform.GetChild(i).gameObject;
            int countChild = miniPage[i].transform.childCount;
            totalLevel += countChild;
            for(int j = 0; j < countChild; j++){
                GameObject obj = miniPage[i].transform.GetChild(j).gameObject;
                levels.Add(obj);
                texts.Add(obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
            }
        }
        for(int i = 0; i < totalLevel; i++){
            texts[i].text = Convert.ToString(i + 1);
            levels[i].gameObject.GetComponent<Button>().interactable = false;
            UnlockLevel(maxLevelCurrent);
        }
    }

    public void Home(){
        SceneManager.LoadScene("Main");
    }

    public void SetScene(){
       string s = "Level" + EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
       SceneManager.LoadScene(s); 
    }

    public void UnlockLevel(int maxLevelCurrent){
        for(int i = 0; i < maxLevelCurrent; i++){
            levels[i].gameObject.GetComponent<Button>().interactable = true;
        }
    }
    public void SaveGame(){
        string jsonString = JsonUtility.ToJson(this);
        SaveSystem.SetString(SAVEDATA,jsonString);
        Debug.Log(jsonString);
    }
    public void LoadGame(){
        string jsonString = SaveSystem.GetString(SAVEDATA);
        Debug.Log(jsonString);
        if(jsonString != String.Empty){
            MaxLevelCurrent obj = JsonUtility.FromJson<MaxLevelCurrent>(jsonString);
            this.maxLevelCurrent = obj.maxLevelCurrent;
        }
    }
    void OnApplicationQuit(){
        SaveGame();
    }
}
