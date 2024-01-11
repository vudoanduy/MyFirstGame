using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BGParallax : MonoBehaviour
{
    [SerializeField] Transform cam;
    GameObject[] objBG;
    Material[] matsBG;

    float[] calSpeedBG;
    float[] distanceBG;

    float maxDisBG = 0;
    int countBG;
    float speedBG = 0.07f;
    float camStartPosX;
    Vector3 startPosBG;
    void Start()
    {
        camStartPosX = Camera.main.transform.localPosition.x;
        startPosBG = transform.localPosition;
        countBG = transform.childCount;
        objBG = new GameObject[countBG];
        matsBG = new Material[countBG];
        calSpeedBG = new float[countBG];
        distanceBG = new float[countBG];
        for(int i = 0; i < countBG; i++){
            objBG[i] = transform.GetChild(i).gameObject;
            matsBG[i] = objBG[i].GetComponent<Renderer>().material;
        }
        CalculatorBG();
    }

    void FixedUpdate()
    {
        SetBGParallax();    
    }

    private void CalculatorBG(){
        // Tim distanceBG max va distanceBG cua tung BG so voi player theo truc z 
        for(int i = 0; i < countBG; i++){
            distanceBG[i] = objBG[i].transform.localPosition.z - cam.localPosition.z;
            if(maxDisBG < distanceBG[i]){
                maxDisBG = distanceBG[i];
            }
        }
        maxDisBG += 5f;
        //Tinh toan toc do cua tung BGParallax
        for(int i = 0; i < countBG; i++){
            calSpeedBG[i] = (1 - (distanceBG[i] / maxDisBG)) * speedBG; 
        } 
    }
    private void SetBGParallax(){
        float distance = cam.localPosition.x - camStartPosX;
        transform.position = new Vector3(startPosBG.x + distance, startPosBG.y, startPosBG.z);
        for(int i = 0; i < countBG; i++){
            matsBG[i].SetTextureOffset("_MainTex", new Vector2(distance,0) * calSpeedBG[i]);
        }
    }

}
