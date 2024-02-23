using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpInfo;
    [SerializeField] Image hpImg;

    ManageMusic manageMusic;
    ManageGUILevel manageGUILevel;
    ManageMenu manageMenu;
    CoinInfo coinInfo;
    Victory victory;
    PlayerMove playerMove;

    protected int maxHP;
    protected int currentHP; // Money the player actually owns
    bool iImmortal;


    void Start(){
        manageMusic = GameObject.Find("ManageAudio").GetComponent<ManageMusic>();
        manageGUILevel = GameObject.Find("ManageGUILevel").GetComponent<ManageGUILevel>();
        coinInfo = GameObject.Find("ManageCoin").GetComponent<CoinInfo>();
        victory = GameObject.Find("Victory").GetComponent<Victory>();
        manageMenu = GameObject.Find("ManageMenu").GetComponent<ManageMenu>();
        playerMove = GetComponent<PlayerMove>();
        currentHP = maxHP = SaveManage.Instance.GetMaxHP();
        UpdateHP();
    }

    void Update(){
        
    }

    void OnTriggerEnter2D(Collider2D other){
        switch(other.gameObject.tag){
            case "Enemy":
                if(iImmortal) return;
                SetCurrentHP(currentHP - 3);
                SetOnImmortal();
                Invoke("SetOffImmortal", 2);
                break;
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
        if(currentHP < 0){
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
    }
    public void SetOffImmortal(){
        this.iImmortal = false;
    }

    public int GetCurrentHP(){
        return this.currentHP;
    }
    public int GetMaxHP(){
        return this.maxHP;
    }
    public bool GetIImmortal(){
        return this.iImmortal;
    }

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
