using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageMain : MonoBehaviour
{
    public void Shop(){
        SceneManager.LoadScene("ShopGUI");
    }

    public void Play(){
        SceneManager.LoadScene("ManageLevel");
    }
}
