using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGUILevel : MonoBehaviour
{
    [Header("Button when player died")]
    [SerializeField] GameObject revival;
    [SerializeField] GameObject die;
    GameObject player;
    PlayerInfo playerInfo;

    void Start(){
        player = GameObject.Find("Player").gameObject;
        playerInfo = player.GetComponent<PlayerInfo>();
        SetBool(false);
    }
    private void SetBool(bool active){
        revival.SetActive(active);
        die.SetActive(active);
    }
    public void PlayerDie(){
        SetBool(true);
    }
    public void Revival(){
        player.SetActive(true);
        playerInfo.MaxHP();
        SetBool(false);
    }
    public void Die(){
        SceneManager.LoadScene("Main");
        SetBool(false);
    }
}
