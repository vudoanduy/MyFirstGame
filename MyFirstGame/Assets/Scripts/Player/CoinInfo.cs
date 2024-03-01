using System;
using TMPro;
using UnityEngine;

public class CoinInfo : MonoBehaviour
{
    [Header("TextMeshProGUI")]
    [SerializeField] TextMeshProUGUI coinInfo;

    [Header("GameObject")]
    [SerializeField] GameObject player;

    [Header("LayerMask")]
    [SerializeField] LayerMask coinLayer;

    private int coinTotal;

    float radius = 5f, speedMagnet = 15f;

    bool iMagnet;

    //
    void Start(){
        SetUpStart();
        UpdateCoin();
    }

    void Update(){
        CheckMagnet();
    }

    //
    public void SetUpStart(){
        SetParameter();
        SetMagnetCoin(false);
    }

    public void SetParameter(){
        this.coinTotal = SaveManage.Instance.GetCoinTotal();
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

    public void CheckMagnet(){
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
}
