using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ManageLevel : MonoBehaviour
{
    [SerializeField] GameObject listPage;

    GameObject[] miniPage;

    int countPage;
    int totalLevel = 0;

    List<GameObject> levels;
    List<TextMeshProUGUI> texts;

    void Start(){
        countPage = listPage.transform.childCount;
        miniPage = new GameObject[countPage];
        levels = new List<GameObject>();
        texts = new List<TextMeshProUGUI>();
        SetUpStart();
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
        }
    }

    public void Home(){
        SceneManager.LoadScene("Main");
    }

    public void SetScene(){
       string s = "Level" + EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
       SceneManager.LoadScene(s); 
    }
}
