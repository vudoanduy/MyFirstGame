using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManageCoin : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] GameObject notifi, notifiError, listItemCurrent;

    [Header("TextMeshProGUI")]
    [SerializeField] TextMeshProUGUI coinTotalText, iBuy;

    GameObject[] items;

    int coinTotal, selectingItem;

    int[] listCostCoin = {50,100,200,500,2000,10000,50000}, listCostItem = {0,500,1500,1000,1000,1000,1000};

    List<int> myNumItem;

    //
    void Start(){
        SetUpStart();
        UpdateCoin();
    }

    //
    public void SetUpStart(){
        SetParameter();
        InitializatingObject();
        notifi.SetActive(false);
    }

    public void SetParameter(){
        coinTotal = SaveManage.Instance.GetCoinTotal();
        if(SaveManage.Instance.GetMyNumItem().Count == 0){
            myNumItem = new List<int>(){0,0,0,0,0,0,0};
            SaveManage.Instance.SetMyNumItem(myNumItem);
        } else {
            myNumItem = SaveManage.Instance.GetMyNumItem();
        }
        items = new GameObject[listItemCurrent.transform.childCount];
    }

    public void InitializatingObject(){
        int count = listItemCurrent.transform.childCount;
        for(int i = 0; i < count; i++){
            items[i] = listItemCurrent.transform.GetChild(i).gameObject;
            items[i].transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = myNumItem[i].ToString();
        }
    }

    // Button
    public void GetFreeCoin(){
        coinTotal += listCostCoin[0];
        UpdateCoin();
        SaveManage.Instance.SetCoinTotal(coinTotal);
        return;
    }
    public void BuyCoin(){
        string text = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.TrimStart('+');
        for(int i = 1; i < listCostCoin.Length; i++){
            if(listCostCoin[i].ToString() == text){
                coinTotal += listCostCoin[i];
                UpdateCoin();
                SaveManage.Instance.SetCoinTotal(coinTotal);
                return;
            }
        }
    }

    // 
    public void BuyItem(){
        notifi.SetActive(true);
        string text = EventSystem.current.currentSelectedGameObject.name;
        int numText = Convert.ToInt32(text);
        for(int i = 1; i <= listCostItem.Length; i++){
            if(i == numText){
                selectingItem = i - 1;
                iBuy.text = "Are you sure you want to buy this item for " + listCostItem[selectingItem] + " coins?";
            }
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
                double coin = (double)coinTotal/1000000;
                coinTotalText.text = Math.Round(coin,2).ToString() + "M";
            }
        }
    }
    // Button in notifi
    public void No(){
        notifi.SetActive(false);
    }
    public void Yes(){
        if(myNumItem[selectingItem] == 99){
            return;
        }
        CheckCoin(listCostItem[selectingItem]);
    }
    //
    public void CheckCoin(int costItem){
        int coin = coinTotal - costItem;
        if(coin < 0){
            NotifiError();
        } else {
            myNumItem[selectingItem]++;
            UpdateNumItem();
            coinTotal = coin;
            UpdateCoin();
            SaveManage.Instance.SetCoinTotal(coinTotal);
            SaveManage.Instance.SetMyNumItem(myNumItem);
        }
        notifi.SetActive(false);
    }
    public void NotifiError(){
        notifiError.transform.localScale = new Vector3(1,1,1);
        LeanTween.scale(notifiError, Vector3.zero, 0.2f).setDelay(1f).setEase(LeanTweenType.linear);
    }
    //
    public void UpdateNumItem(){
        items[selectingItem].transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = myNumItem[selectingItem].ToString();
    }
}
