using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.VisualScripting;

public class ManageShopGUI : MonoBehaviour
{
    [SerializeField] GameObject obj;

    Image[] listImg;
    GameObject[] listMenu;
    GameObject[] listScroll;
    GameObject[] listItem;

    int childCount;

    Vector3[] startPosItem;


    void Start(){
        childCount = obj.transform.childCount;
        listImg = new Image[childCount];
        listMenu = new GameObject[childCount];
        listScroll = new GameObject[childCount];
        listItem = new GameObject[childCount];
        startPosItem = new Vector3[childCount];
        SetUpStart();
        SetColor(0);
    }
    public void SetUpStart(){
        // Lay gameObject cua thang con
        for(int i = 0; i < childCount; i++){
            listMenu[i] = obj.transform.GetChild(i).gameObject;
            listImg[i] = listMenu[i].transform.GetChild(0).gameObject.GetComponent<Image>();
            listScroll[i] = listMenu[i].transform.GetChild(1).gameObject;
            listItem[i] = listScroll[i].transform.GetChild(0).transform.GetChild(0).gameObject;
        }
        // Tinh toan startPosItem
        for(int i = 0; i < childCount; i++){
            int count = listItem[i].transform.childCount;
            int width = count * 400 + (count - 1) * 70;
            startPosItem[i] = new Vector3(width / 2, 0, 0);
        }
    }
    public void SetColor(int index){
        for(int i = 0; i < childCount; i++){
            if(i == index){
                listImg[i].color = Color.HSVToRGB(0.5f,1,1);
                listScroll[i].SetActive(true);
                listItem[i].transform.localPosition = startPosItem[i];
            } else {
                listImg[i].color = Color.HSVToRGB(0,0,1);
                listScroll[i].SetActive(false);
            }
        }
    }
    public void Close(){
        SceneManager.LoadScene("Main");
    }
}
