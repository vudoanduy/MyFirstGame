using UnityEngine;

public class ManageSkinInLevel : MonoBehaviour
{
    SpriteRenderer skinPlayer;

    float[,] skin;
    int selectedSkin;

    // 
    void Start()
    {
        SetUpStart();
        SetSkin(selectedSkin);
    }

    //
    public void SetUpStart(){
        SetParameter();
        InitializatingObject();
    }

    public void SetParameter(){
        skin = new float[,]{{0,0,1},{0.3333f,0.87f,0.93f},{0.8125f,0.93f,0.9f}};
        selectedSkin = SaveManage.Instance.GetSelectedSkin();   
    }

    public void InitializatingObject(){
        skinPlayer = GetComponent<SpriteRenderer>();
    }

    //
    public void SetSkin(int id){
        skinPlayer.color = Color.HSVToRGB(skin[id,0],skin[id,1],skin[id,2]);
    }
}
