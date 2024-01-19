using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpInfo;

    ManageMusic manageMusic;

    protected int maxHP;
    protected int currentHP;
    float timeNoDamage = 2f;

    void Start(){
        manageMusic = GameObject.Find("ManageAudio").GetComponent<ManageMusic>();
        currentHP = maxHP = 2;
        hpInfo.text = currentHP + " / " + maxHP;
    }

    void Update(){
        timeNoDamage += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){
            if(timeNoDamage < 2f) return;
            timeNoDamage = 0;
            currentHP--;
            hpInfo.text = currentHP + " / " + maxHP;
            if(currentHP == 0){
                Destroy(this.gameObject);
                Debug.Log("Game Over");
                manageMusic.GameOver();
            }
        }
    }

    
}
