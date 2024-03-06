using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [Header("TextMeshProGUI")]
    [SerializeField] TextMeshProUGUI hpInfo;

    [Header("Image")]
    [SerializeField] Image hpImg;

    [Header("GameObject")]
    [SerializeField] GameObject shield;

    ManageMusic manageMusic;
    ManageGUILevel manageGUILevel;
    ManageMenu manageMenu;
    CoinInfo coinInfo;
    Victory victory;
    PlayerMove playerMove;

    protected int maxHP, currentHP;

    bool iImmortal;


    void Start(){
        SetUpStart();
        UpdateHP();
    }

    //
    public void SetUpStart(){
        SetParameter();
        InitializatingObject();
    }

    public void SetParameter(){
        currentHP = maxHP = SaveManage.Instance.GetMaxHP();
    }

    public void InitializatingObject(){
        manageMusic = GameObject.Find("ManageAudio").GetComponent<ManageMusic>();
        manageGUILevel = GameObject.Find("ManageGUILevel").GetComponent<ManageGUILevel>();
        coinInfo = GameObject.Find("ManageCoin").GetComponent<CoinInfo>();
        victory = GameObject.Find("Victory").GetComponent<Victory>();
        manageMenu = GameObject.Find("ManageMenu").GetComponent<ManageMenu>();
        playerMove = GetComponent<PlayerMove>();

        shield.gameObject.SetActive(false);
    }

    //
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Enemy"){
            if(iImmortal) return;
            SetCurrentHP(currentHP - 2);
            SetOnImmortal();
            Invoke("SetOffImmortal", 2);
        }
        if(other.gameObject.tag == "Monster"){
            if(iImmortal) return;
            SetCurrentHP(currentHP - 1);
            SetOnImmortal();
            Invoke("SetOffImmortal", 2);
        }
        if(other.gameObject.tag == "water"){
            if(iImmortal) return;
            SetCurrentHP(currentHP - 1);
            SetOnImmortal();
            Invoke("SetOffImmortal", 2);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        switch(other.gameObject.tag){
            case "Coin":
                manageMusic.CollectCoin();
                Destroy(other.gameObject);
                coinInfo.AddCoin(1);
                break;
            case "Door":
                manageMusic.Victory();
                SetUnlockLevel();
                victory.VictoryLevel();
                Destroy(playerMove);
                manageMenu.SetBool(false);
                break;
            case "Ray":
                if(iImmortal) return;
                SetCurrentHP(currentHP - 2);
                SetOnImmortal();
                Invoke("SetOffImmortal", 2);
                break;
            case "Monster":
                if(iImmortal) return;
                SetCurrentHP(currentHP - 1);
                SetOnImmortal();
                Invoke("SetOffImmortal", 2);
                break;
            default:
                break;
        }
    }
    public void SetUnlockLevel(){
        int levelCurrent = manageGUILevel.GetLevelCurrent();
        int maxLevelCurrent = SaveManage.Instance.GetMaxLevel();
        if(levelCurrent < maxLevelCurrent || maxLevelCurrent == SaveManage.Instance.GetTotalLevel()){
            return;
        }
        SaveManage.Instance.SetMaxLevel(levelCurrent + 1);
    }
    // HP
    public void SetCurrentHP(int currentHP){
        if(currentHP <= 0){
            currentHP = 0;
            gameObject.SetActive(false);
            Debug.Log("Game Over");
            manageMusic.GameOver();
            manageGUILevel.PlayerDie();
        }
        this.currentHP = currentHP;
        UpdateHP();
    }
    public void SetMaxHP(int maxHP){
        this.maxHP = maxHP;
        UpdateHP();
    }
    public void SetOnImmortal(){
        this.iImmortal = true;
        shield.gameObject.SetActive(true);
    }
    public void SetOffImmortal(){
        this.iImmortal = false;
        shield.gameObject.SetActive(false);
    }

    //
    public int GetCurrentHP(){
        return this.currentHP;
    }
    public int GetMaxHP(){
        return this.maxHP;
    }
    public bool GetIImmortal(){
        return this.iImmortal;
    }

    //
    public void RestoreHP(int num){
        currentHP += num;
        if(currentHP > maxHP){
            currentHP = maxHP;
        }
        UpdateHP();
    }
    public void UpdateHP(){
        hpInfo.text = currentHP + " / " + maxHP;
        hpImg.fillAmount = (float)currentHP / (float)maxHP;
    }
}
