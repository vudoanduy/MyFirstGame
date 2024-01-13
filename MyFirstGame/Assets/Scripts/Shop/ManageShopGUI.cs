using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageShopGUI : MonoBehaviour
{
    [SerializeField] GameObject obj;

    GameObject[] listMenu;
    Image[] listImg;
    GameObject[] listScroll;
    GameObject[] listItem;

    int childCount;
    string scroll;
    string listitem;

    Vector3 startPosItem;


    void Start(){
        scroll = "Scroll";
        listitem = "ListItem";
        childCount = obj.transform.childCount;
        listMenu = new GameObject[childCount];
        listImg = new Image[childCount];
        listScroll = new GameObject[childCount];
        listItem = new GameObject[childCount];
        SetUpStart();
        SetColor(0);
    }
    public void SetUpStart(){
        for(int i = 0; i < childCount; i++){
            listMenu[i] = obj.transform.GetChild(i).gameObject;
        } 
        for(int i = 0; i < childCount; i++){
            listImg[i] = listMenu[i].GetComponent<Image>();
        }
        for(int i = 0; i < childCount; i++){
            string s = scroll + " (" + i + ")";
            listScroll[i] = GameObject.Find(s).gameObject;
        }
        for(int i = 0; i < childCount; i++){
            string s = listitem + i;
            listItem[i] = GameObject.Find(s).gameObject;
        }
        startPosItem = listItem[0].transform.localPosition;
    }
    public void SetColor(int index){
        for(int i = 0; i < childCount; i++){
            if(i == index){
                listImg[i].color = Color.HSVToRGB(0.5f,1,1);
                listScroll[i].SetActive(true);
                listItem[i].transform.localPosition = startPosItem;
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
