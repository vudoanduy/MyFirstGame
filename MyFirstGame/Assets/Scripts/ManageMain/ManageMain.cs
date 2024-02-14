using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageMain : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelMaxCurrent;
    [SerializeField] TextMeshProUGUI coinInfo;

    void Start(){
        levelMaxCurrent.text = "Level " + SaveManage.Instance.GetMaxLevel().ToString();
        coinInfo.text = SaveManage.Instance.GetCoinTotal().ToString();
    }

    public void Shop(){
        SceneManager.LoadScene("ShopGUI");
    }

    public void Play(){
        SceneManager.LoadScene("ManageLevel");
    }
}
