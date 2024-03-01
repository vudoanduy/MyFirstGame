using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManageInventory : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] GameObject inventory, listItem, player, listItemUsed, itemUsed;

    [Header("Source Img")]
    [SerializeField] Sprite speedSprite, giantSprite, immortalSprite, magnetSprite;

    PlayerInfo playerInfo;
    PlayerMove playerMove;
    CoinInfo coinInfo;

    GameObject[] items;
    TextMeshProUGUI[] countItemsText;

    int countItems, selectedItem, currentHP, maxHP;
    int[] myNumItemDefault = {0,0,0,0,0,0,0};

    float timeSpeedCurrent = 20, timeGiantCurrent = 10, timeImmortalCurrent = 5, timeMagnetCurrent = 30;
    float timeSpeedDefault = 20, timeGiantDefault = 10, timeImmortalDefault = 5, timeMagnetDefault = 30;
    float speed, force;

    bool iUseItem, useSpeed, useGiant, useImmortal, useMagnet;

    List<int> myNumItem;

    //
    void Start(){
        SetUpStart();
    }
    void Update(){
        CheckTime();
    }

    //
    public void SetUpStart(){
        SetParameter();
        InitializatingObject();
        inventory.SetActive(false);
    }

    public void SetParameter(){
        countItems = listItem.transform.childCount;
        useSpeed = useGiant = useImmortal = useMagnet = false;
    }

    public void InitializatingObject(){
        items = new GameObject[countItems];
        countItemsText = new TextMeshProUGUI[countItems];
        playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        coinInfo = GameObject.Find("ManageCoin").GetComponent<CoinInfo>();

        // setup myNumItem
        if(SaveManage.Instance.GetMyNumItem().Count != 0){
            myNumItem = SaveManage.Instance.GetMyNumItem();
        } else {
            myNumItem = new List<int>(myNumItemDefault);
            SaveManage.Instance.SetMyNumItem(myNumItem);
        }
        int count = myNumItem.Count;
        for(int i = 0; i < myNumItem.Count; i++){

        }
        // setup others
        for(int i = 0; i < countItems; i++){
            items[i] = listItem.transform.GetChild(i).gameObject;
        }
        for(int i = 0; i < count; i++){
            countItemsText[i] = items[i].transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
            countItemsText[i].text = myNumItem[i].ToString();
        }
    }


    // Button 
    public void Bag(){
        inventory.SetActive(true);
        Time.timeScale = 0;
    }
    public void Close(){
        inventory.SetActive(false);
        Time.timeScale = 1;
    }

    // Item
    public void Item(){
        iUseItem = false;
        string s = EventSystem.current.currentSelectedGameObject.name;
        selectedItem = Convert.ToInt32(s);
        CheckUseItem();
        if(iUseItem){
            UseItem();
        }
    }
    public void CheckUseItem(){
        int count = myNumItem[selectedItem] - 1;
        if(count < 0){
            count = 0;
            return;
        }
        iUseItem = true;
        countItemsText[selectedItem].text = count.ToString();
        myNumItem[selectedItem] = count;
    }
    public void UseItem(){
        switch(selectedItem){
            case 0:
                maxHP = playerInfo.GetMaxHP();
                playerInfo.RestoreHP(maxHP / 4);
                break;
            case 1:
                maxHP = playerInfo.GetMaxHP();
                playerInfo.RestoreHP(maxHP / 2);
                break;
            case 2:
                maxHP = playerInfo.GetMaxHP();
                playerInfo.RestoreHP(maxHP);
                break;
            case 3:
                if(useSpeed){
                    timeSpeedCurrent = timeSpeedDefault;
                    return;
                }
                CreateGameObjectChildren("speedImg", speedSprite);
                speed = playerMove.GetSpeed();
                playerMove.SetSpeed(speed * 2);
                useSpeed = true;
                break;
            case 4:
                if(useGiant) {
                    timeGiantCurrent = timeGiantDefault;
                    return;
                }
                CreateGameObjectChildren("giantImg", giantSprite);
                // Increase HP and Size
                maxHP = playerInfo.GetMaxHP();
                currentHP = playerInfo.GetCurrentHP();
                playerInfo.SetMaxHP(maxHP * 2);
                playerInfo.SetCurrentHP(currentHP * 2);
                Vector3 scale = playerMove.GetScale();
                scale *= 2;
                playerMove.SetScale(scale);
                player.transform.localScale = scale;
                // Increase speed and force
                force = playerMove.GetForce();
                playerMove.SetForce(force * 1.3f);
                useGiant = true;
                break;
            case 5:
                if(useImmortal){
                    timeImmortalCurrent = timeImmortalDefault;
                    return;
                }
                CreateGameObjectChildren("immortalImg", immortalSprite);
                playerInfo.SetOnImmortal();
                useImmortal = true;
                break;
            case 6:
                if(useMagnet){ 
                    timeMagnetCurrent = timeMagnetDefault;
                    return;
                }
                CreateGameObjectChildren("magnetImg", magnetSprite);
                coinInfo.SetMagnetCoin(true);
                useMagnet = true;
                break;
        }
    }

    //
    public void SetDefaultHP(){
        currentHP = playerInfo.GetCurrentHP();
        int maxHPDefault = SaveManage.Instance.GetMaxHP();
        if(currentHP > maxHPDefault){
            currentHP = maxHPDefault;
        }
        playerInfo.SetCurrentHP(currentHP);
        playerInfo.SetMaxHP(maxHPDefault);
    }
    public void SetDefaultSpeed(){
        playerMove.SetSpeed(SaveManage.Instance.GetSpeed());
    }
    public void SetDefaultForce(){
        playerMove.SetForce(SaveManage.Instance.GetForce());
    }
    public void SetDefaultScale(){
        playerMove.SetScale(SaveManage.Instance.GetScale());
        player.transform.localScale = SaveManage.Instance.GetScale();
    }
    public void SetOffImmortal(){
        playerInfo.SetOffImmortal();
        useImmortal = false;
    }
    public void SetOffMagnetCoin(){
        coinInfo.SetMagnetCoin(false);
        useMagnet = false;  
    }

    //
    public void CheckTime(){
        if(useSpeed){
            timeSpeedCurrent -= Time.deltaTime;
            Debug.Log(timeSpeedCurrent);
            if(timeSpeedCurrent < 0){
                SetDefaultSpeed();
                useSpeed = false;
                timeSpeedCurrent = timeSpeedDefault;
                DestroyGameObject("speedImg");
            }
            GameObject.Find("speedImg").transform.GetChild(0).GetComponent<Image>().fillAmount = timeSpeedCurrent / timeSpeedDefault;
        }
        if(useGiant){
            timeGiantCurrent -= Time.deltaTime;
            if(timeGiantCurrent < 0){
                SetDefaultHP();
                SetDefaultForce();
                SetDefaultScale();
                useGiant = false;
                timeGiantCurrent = timeGiantDefault;
                DestroyGameObject("giantImg");
            }
            GameObject.Find("giantImg").transform.GetChild(0).GetComponent<Image>().fillAmount = timeGiantCurrent / timeGiantDefault;
        }
        if(useImmortal){
            timeImmortalCurrent -= Time.deltaTime;
            if(timeImmortalCurrent < 0){
                SetOffImmortal();
                timeImmortalCurrent = timeImmortalDefault;
                DestroyGameObject("immortalImg");
            }
            GameObject.Find("immortalImg").transform.GetChild(0).GetComponent<Image>().fillAmount = timeImmortalCurrent / timeImmortalDefault;
        }
        if(useMagnet){
            timeMagnetCurrent -= Time.deltaTime;
            if(timeMagnetCurrent < 0){
                SetOffMagnetCoin();
                timeMagnetCurrent = timeMagnetDefault;
                DestroyGameObject("magnetImg");
            }
            GameObject.Find("magnetImg").transform.GetChild(0).GetComponent<Image>().fillAmount = timeMagnetCurrent / timeMagnetDefault;
        }
    }
    //
    public void CreateGameObjectChildren(string name, Sprite sprite){
        GameObject obj = Instantiate(itemUsed, listItemUsed.transform);
        obj.name = name;
        obj.transform.localScale = new Vector2(1,1);
        obj.gameObject.GetComponent<Image>().sprite = sprite;
    }
    public void DestroyGameObject(string name){
        Destroy(GameObject.Find(name));
    }
}
