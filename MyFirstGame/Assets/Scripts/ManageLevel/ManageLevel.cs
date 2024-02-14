using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

public class ManageLevel : MonoBehaviour
{
    public static ManageLevel Instance;
    [SerializeField] GameObject listPage;

    GameObject[] miniPage;
    List<GameObject> levels;
    List<TextMeshProUGUI> texts;

    int countPage;
    int totalLevel;
    public int maxLevelCurrent;

    void Start(){

        countPage = listPage.transform.childCount;
        maxLevelCurrent = SaveManage.Instance.GetMaxLevel();

        miniPage = new GameObject[countPage];
        levels = new List<GameObject>();
        texts = new List<TextMeshProUGUI>();
        SetUpStart();
        SaveManage.Instance.SetTotalLevel(totalLevel);
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.M)){
            if(maxLevelCurrent == totalLevel) return;
            maxLevelCurrent++;
            UnlockLevel(maxLevelCurrent);
            SaveManage.Instance.SetMaxLevel(maxLevelCurrent);
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
        }
        UnlockLevel(maxLevelCurrent);
    }

    public void Home(){
        SceneManager.LoadScene("Main");
    }

    public void SetScene(){
       string s =  EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
       SceneManager.LoadScene(s); 
    }

    public void UnlockLevel(int maxLevelCurrent){
        this.maxLevelCurrent = maxLevelCurrent;
        for(int i = 0; i < maxLevelCurrent; i++){
            levels[i].gameObject.GetComponent<Button>().interactable = true;
        }
    }
    public int GetTotalLevel(){
        return this.totalLevel;
    }
}
