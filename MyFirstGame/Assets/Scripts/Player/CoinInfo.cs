using System;
using TMPro;
using UnityEngine;

public class CoinInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinInfo;

    private int coinTotal;

    void Start(){
        this.coinTotal = SaveManage.Instance.GetCoinTotal();
        UpdateCoin();
    }

    public void AddCoin(int value){
        coinTotal += value;
        UpdateCoin();
        SaveManage.Instance.SetCoinTotal(this.coinTotal);
    }
    public void UpdateCoin(){
        coinInfo.text = Convert.ToString(coinTotal);
    }
}
