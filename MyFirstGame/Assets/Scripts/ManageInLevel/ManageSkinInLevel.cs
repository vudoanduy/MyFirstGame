using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ManageSkinInLevel : MonoBehaviour
{
    SpriteRenderer skinPlayer;

    float[,] skin;
    int selectedSkin;
    // Start is called before the first frame update
    void Start()
    {
        skinPlayer = GetComponent<SpriteRenderer>();
        skin = new float[,]{{0,0,1},{0.3333f,0.87f,0.93f},{0.8125f,0.93f,0.9f}};
        selectedSkin = SaveManage.Instance.GetSelectedSkin();
        SetSkin(selectedSkin);
    }

    public void SetSkin(int id){
        skinPlayer.color = Color.HSVToRGB(skin[id,0],skin[id,1],skin[id,2]);
    }
}
