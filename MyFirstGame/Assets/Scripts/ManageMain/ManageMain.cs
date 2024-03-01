using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageMain : MonoBehaviour
{
    [Header("TextMeshProGUI")]
    [SerializeField] TextMeshProUGUI levelMaxCurrent, coinInfo;

    //
    void Start(){
        SetUpStart();
    }

    //
    public void SetUpStart(){
        SetParameter();
    }

    public void SetParameter(){
        levelMaxCurrent.text = "Level " + SaveManage.Instance.GetMaxLevel().ToString();
        coinInfo.text = SaveManage.Instance.GetCoinTotal().ToString();
    }

    //
    public void Shop(){
        SceneManager.LoadScene("ShopGUI");
    }

    public void Play(){
        SceneManager.LoadScene("ManageLevel");
    }
}
