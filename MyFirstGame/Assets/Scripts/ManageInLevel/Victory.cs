using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{   

    [Header("GameObject")]
    [SerializeField] GameObject victory;

    ManageGUILevel manageGUILevel;

    void Start(){
        SetUpStart();
    }

    //
    public void SetUpStart(){
        InitializatingObject();
    }

    public void InitializatingObject(){
        manageGUILevel = GameObject.Find("ManageGUILevel").GetComponent<ManageGUILevel>();
    }

    // Button
    public void Out(){
        SceneManager.LoadScene("ManageLevel");
    }

    public void Replay(){
        TransitionScene.Instance.TransScene(manageGUILevel.GetNameLevel());
        
    }

    public void Next(){
        int totalLevel = SaveManage.Instance.GetTotalLevel();
        if(manageGUILevel.GetLevelCurrent() == totalLevel) {
            SceneManager.LoadScene("ManageLevel");
        } else {
            int nextLevel = manageGUILevel.GetLevelCurrent() + 1;
            TransitionScene.Instance.TransScene(Convert.ToString(nextLevel));
        }
    }

    //
    public void VictoryLevel(){
        LeanTween.scale(victory, new Vector3(1,1,1), 1f).setDelay(0.25f).setEase(LeanTweenType.linear);
        SaveManage.Instance.SaveGame();
    }

}
