using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchLaze : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] GameObject ray;

    float countTime = 0;

    void Start(){
        Launch();
    }

    void Update(){  
        countTime += Time.deltaTime;

        if(countTime < 3) return;
        countTime = 0;
        Launch();
    }

    public void Launch(){
        LeanTween.scale(ray, new Vector3(1,1,1), 0.5f).setEase(LeanTweenType.easeOutQuad);
        LeanTween.scale(ray, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuad).setDelay(1);
    }
}
