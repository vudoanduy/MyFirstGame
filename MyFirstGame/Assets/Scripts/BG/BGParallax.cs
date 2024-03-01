using UnityEngine;

public class BGParallax : MonoBehaviour
{   
    [Header("Transform")]
    [SerializeField] Transform cam;

    GameObject[] objBG;
    Material[] matsBG;

    int countBG;
    float maxDisBG = 0, speedBG = 0.07f, camStartPosX;

    float[] calSpeedBG, distanceBG;

    Vector3 startPosBG;

    //
    void Start()
    {
        SetUpStart();
        CalculatorBG();
    }

    void FixedUpdate()
    {
        SetBGParallax();    
    }

    //
    public void SetUpStart(){
        SetParameter();
        InitializatingObject();
    }

    public void SetParameter(){
        camStartPosX = Camera.main.transform.localPosition.x;
        startPosBG = transform.localPosition;
        countBG = transform.childCount;
    }

    public void InitializatingObject(){
        objBG = new GameObject[countBG];
        matsBG = new Material[countBG];
        calSpeedBG = new float[countBG];
        distanceBG = new float[countBG];

        //
        for(int i = 0; i < countBG; i++){
            objBG[i] = transform.GetChild(i).gameObject;
            matsBG[i] = objBG[i].GetComponent<Renderer>().material;
        }
    }

    //

    public void CalculatorBG(){
        for(int i = 0; i < countBG; i++){
            distanceBG[i] = objBG[i].transform.localPosition.z - cam.localPosition.z;
            if(maxDisBG < distanceBG[i]){
                maxDisBG = distanceBG[i];
            }
        }
        maxDisBG += 5f;
        for(int i = 0; i < countBG; i++){
            calSpeedBG[i] = (1 - (distanceBG[i] / maxDisBG)) * speedBG; 
        } 
    }

    public void SetBGParallax(){
        float distance = cam.localPosition.x - camStartPosX;
        transform.position = new Vector3(startPosBG.x + distance, cam.transform.position.y, startPosBG.z);
        for(int i = 0; i < countBG; i++){
            matsBG[i].SetTextureOffset("_MainTex", Vector2.right * distance * calSpeedBG[i]);
        }
    }

    //
}
