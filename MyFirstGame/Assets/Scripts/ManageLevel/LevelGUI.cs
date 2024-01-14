using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelGUI : MonoBehaviour
{
    [SerializeField] GameObject listPage;
    [SerializeField] GameObject panel;
    [SerializeField] LeanTweenType tweenType;

    GameObject[] buttonPages;

    int currentPage;
    int maxPage;
    int btnPage;

    Vector3 posCurrent;

    void Start(){
        currentPage = 1;
        btnPage = 1;
        maxPage = listPage.transform.childCount;
        posCurrent = listPage.transform.localPosition;

        buttonPages = new GameObject[maxPage];

        SetPage(btnPage);
    }

    private void SetUpStart(){
        for(int i = 0; i < maxPage; i++){
            buttonPages[i] = panel.transform.GetChild(i).gameObject;
        }
    }

    public void Prev(){
        if(currentPage <= 1) return;
        currentPage--;
        posCurrent.x += 1844;
        listPage.LeanMoveLocal(posCurrent, 0.4316337f).setEase(tweenType);
        if(currentPage == maxPage - 1){
            SetActiveBtn("Next", true);
        }
        if(currentPage == 1){
            SetActiveBtn("Prev", false);
        }
    }
    public void Next(){
        if(currentPage >= maxPage) return; 
        currentPage++;
        posCurrent.x -= 1844;
        listPage.LeanMoveLocal(posCurrent, 0.4316337f).setEase(tweenType);
        if(currentPage == 2){
            SetActiveBtn("Prev", true);
        }
        if(currentPage == maxPage){
            SetActiveBtn("Next", false);
        }
    }
    public void SetActiveBtn(string gameObject, bool active){
        Button gameObject1 = GameObject.Find(gameObject).gameObject.GetComponent<Button>();
        gameObject1.interactable = active;
    }
    public void btnInPanel(){
        string s = EventSystem.current.currentSelectedGameObject.name;
        btnPage = Convert.ToInt32(s);
        SetPage(btnPage);
    }
    public void SetPage(int btnPage){
        posCurrent.x += (currentPage - btnPage) * 1844;
        listPage.LeanMoveLocal(posCurrent, 0.4316337f).setEase(tweenType);
        currentPage = btnPage;
        if(currentPage == 1){
            SetActiveBtn("Prev", false);
        } else if(currentPage == maxPage){
            SetActiveBtn("Next", false);
        } else {
            SetActiveBtn("Prev", true);
            SetActiveBtn("Next", true);
        }
    }
    
}
