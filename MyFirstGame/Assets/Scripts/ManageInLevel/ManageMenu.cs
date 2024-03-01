using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageMenu : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] GameObject menuGUI, bgMenu, settingGUI;

    //
    void Start(){
        SetBool(false);
        SetUpStart();
    }

    //
    public void SetUpStart(){
        settingGUI.SetActive(false);
    }

    //
    public void SetBool(bool active){
        menuGUI.SetActive(active);
        bgMenu.SetActive(active);
    }

    //
    public void Menu(){
        Time.timeScale = 0;
        SetBool(true);
    }
    public void Resume(){
        Time.timeScale = 1;
        SetBool(false);
    }
    public void Settings(){
        menuGUI.SetActive(false);
        settingGUI.SetActive(true);
    }
    public void Home(){
        Time.timeScale = 1;
        SetBool(false);
        SceneManager.LoadScene("Main");
    }
    public void Back(){
        menuGUI.SetActive(true);
        settingGUI.SetActive(false);
    }
}
