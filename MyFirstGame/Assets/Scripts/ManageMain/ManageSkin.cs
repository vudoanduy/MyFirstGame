using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class ManageSkin : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] GameObject listSkin, notifiError, notifi;

    [Header("TextMeshProGUI")]
    [SerializeField] TextMeshProUGUI iBuy, coinTotalText, notifiText;

    GameObject[] skins;

    int selectedSkin, selectingSkin, coinTotal;
    public int countSkin;
    int[] defaultCheck = {1,0,0};
    int[] costSkin = {0,2000,10000,50000};

    bool isBuySuccess;

    List<int> checkSkin;
    
    Vector3 posStart;

    //
    void Start(){
        SaveManage.Instance.LoadGame();
        SetUpStart();
        UpdateCoin();
    }

    //
    private void SetUpStart(){
        SetParameter();
        InitializatingObject();
    }

    public void SetParameter(){
        countSkin = listSkin.transform.childCount;
        coinTotal = SaveManage.Instance.GetCoinTotal();
        selectedSkin = selectingSkin = SaveManage.Instance.GetSelectedSkin();       
        iBuy.text = "Selected";
        notifiError.transform.localScale = Vector3.zero;
    }

    public void InitializatingObject(){
        skins = new GameObject[countSkin];

        //
        for(int i = 0; i < countSkin; i++){
            skins[i] = listSkin.transform.GetChild(i).gameObject;
        }
        SetMask(selectedSkin);
        posStart = listSkin.transform.localPosition = new Vector3(-260 - 620 * selectedSkin, listSkin.transform.localPosition.y, listSkin.transform.localPosition.z);
        // Load checkSkin
        if(SaveManage.Instance.GetCheckSkin().Count != 0){
            checkSkin = SaveManage.Instance.GetCheckSkin();
        } else {
            checkSkin = new List<int>(defaultCheck);
            SaveManage.Instance.SetCheckSkin(checkSkin);
        }
        notifi.SetActive(false);
    }

    // Set In/Outside mask
    private void SetMask(int selectingSkin){
        for(int i = 0; i < countSkin; i++){
            if(i == selectingSkin){
                skins[i].transform.GetChild(0).GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            }else{
                skins[i].transform.GetChild(0).GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }
        }
    }

    // Button 
    public void Prev(){
        int ID = selectingSkin - 1;
        if(ID < 0){
            ID = countSkin - 1;
        }
        posStart.x += 620 * (selectingSkin - ID);
        selectingSkin = ID;
        SetMask(selectingSkin);
        listSkin.LeanMoveLocalX(posStart.x, 0.1f).setEase(LeanTweenType.linear);
        SetText();
    }
    public void Next(){
        int ID = selectingSkin + 1;
        if(ID == countSkin){
            ID = 0;
        }
        posStart.x += 620 * (selectingSkin - ID);
        selectingSkin = ID;
        SetMask(selectingSkin);
        listSkin.LeanMoveLocalX(posStart.x, 0.1f).setEase(LeanTweenType.linear);
        SetText();
    }
    public void Buy(){
        if(iBuy.text == "Buy"){
            notifi.SetActive(true);
            notifiText.text = "Are you sure you want to buy this skin for " + costSkin[selectingSkin] + " coins?";
        } else if(iBuy.text == "Select"){
            selectedSkin = selectingSkin;
            SaveManage.Instance.SetSelectedSkin(selectedSkin);
            SaveManage.Instance.SetCheckSkin(checkSkin);
            iBuy.text = "Selected";
        }
    }
    public void SetText(){
        if(checkSkin[selectingSkin] == 0){
            iBuy.text = "Buy";
        } else {
            if(selectingSkin == selectedSkin) {
                iBuy.text = "Selected";
            } else{
                iBuy.text = "Select";
            }
        }
    }
    // Button in notifi
    public void No(){
        notifi.SetActive(false);
    }
    public void Yes(){
        isBuySuccess = false;
        CheckCoin();
        if(isBuySuccess){
            iBuy.text = "Select";
            checkSkin[selectingSkin] = 1;
            SaveManage.Instance.SetCheckSkin(checkSkin);
        }
        notifi.SetActive(false);
    }
    //
    public void CheckCoin(){
        if(costSkin[selectingSkin] <= coinTotal){
            coinTotal -= costSkin[selectingSkin];
            SaveManage.Instance.SetCoinTotal(coinTotal);
            UpdateCoin();
            isBuySuccess = true;
        } else {
            NotifiError();
            isBuySuccess = false;
        }
    }
    public void UpdateCoin(){
        if(coinTotal < 1000000){
            coinTotalText.text = coinTotal.ToString();
        } else {
            if(coinTotal >= 2000000000){
                coinTotal = 2000000000;
                coinTotalText.text = coinTotal.ToString();
            } else if((coinTotal/1000000000) != 0){
                double coin = (double)coinTotal/1000000000;
                coinTotalText.text = Math.Round(coin,3).ToString() + "B";
            } else {
                coinTotalText.text = (coinTotal/1000000).ToString() + "M";
            }
        }
    }
    //
    public void NotifiError(){
        notifiError.transform.localScale = new Vector3(1,1,1);
        LeanTween.scale(notifiError, Vector3.zero, 0.2f).setDelay(1f).setEase(LeanTweenType.linear);
    }
}
