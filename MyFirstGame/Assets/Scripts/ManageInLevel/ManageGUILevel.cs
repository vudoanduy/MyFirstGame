using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGUILevel : MonoBehaviour
{
    [Header("Button when player died")]
    [SerializeField] GameObject revival;
    [SerializeField] GameObject die;
    GameObject player;
    PlayerInfo playerInfo;
    int levelCurrent;
    string nameLevel;

    void Start(){
        nameLevel = SceneManager.GetActiveScene().name;
        levelCurrent = Convert.ToInt32(nameLevel);
        player = GameObject.Find("Player").gameObject;
        playerInfo = player.GetComponent<PlayerInfo>();
        SetBool(false);
    }
    void Update(){
        
    }
    // Button
    private void SetBool(bool active){
        revival.SetActive(active);
        die.SetActive(active);
    }
    public void PlayerDie(){
        SetBool(true);
    }
    public void Revival(){
        player.SetActive(true);
        
        SetBool(false);
    }
    //
    public int GetLevelCurrent(){
        return this.levelCurrent;
    }
    public string GetNameLevel(){
        return nameLevel;
    }
}
