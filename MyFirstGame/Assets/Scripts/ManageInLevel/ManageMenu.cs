using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageMenu : MonoBehaviour
{
    [SerializeField] GameObject menuGUI;
    [SerializeField] GameObject bgMenu;
    [SerializeField] GameObject settingGUI;

    void Start(){
        SetBool(false);
        settingGUI.SetActive(false);
    }
    private void SetBool(bool active){
        menuGUI.SetActive(active);
        bgMenu.SetActive(active);
    }
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
