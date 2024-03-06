using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleMap : MonoBehaviour
{
    public string mapTele = "";

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            TransitionScene.Instance.TransScene(mapTele);
        }
    }
}
