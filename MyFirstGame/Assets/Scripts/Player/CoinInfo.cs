using System;
using TMPro;
using UnityEngine;

public class CoinInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinInfo;
    [SerializeField] GameObject player;
    [SerializeField] LayerMask coinLayer;
    float radius = 5f;
    float speedMagnet = 15f;
    bool iMagnet;

    private int coinTotal;

    void Start(){
        this.coinTotal = SaveManage.Instance.GetCoinTotal();
        UpdateCoin();
        SetUpStart();
    }
    void Update(){
        if(iMagnet){
            Collider2D[] coins = Physics2D.OverlapCircleAll(player.transform.localPosition, this.radius, coinLayer);
            if(coins != null){
                for(int i = 0; i < coins.Length; i++){
                    Collider2D coin = coins[i];
                    coin.transform.position = Vector3.MoveTowards(coin.transform.position, player.transform.localPosition, speedMagnet * Time.deltaTime);
                }
            }
        }
    }

    public void SetUpStart(){
        SetMagnetCoin(false);
    }

    //
    public void AddCoin(int value){
        coinTotal += value;
        UpdateCoin();
        SaveManage.Instance.SetCoinTotal(this.coinTotal);
    }
    public void UpdateCoin(){
        coinInfo.text = Convert.ToString(coinTotal);
    }

    //
    public void SetMagnetCoin(bool iMagnet){
        this.iMagnet = iMagnet;
    }
}
