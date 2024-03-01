using UnityEngine;

public class SetAppearBoard : MonoBehaviour
{
    [Header("Layer Mask")]
    [SerializeField] LayerMask player;
    [Header("Set Size Collider")]
    [SerializeField] Vector2 sizeCol = new Vector2();
    [SerializeField] float scaleX = 0;

    Vector3 defaultScale;


    int countAppear = 0;

    float timeScale = 0.35f, scaleY;
    
    bool isPlayer;

    void Start(){
        scaleY = this.transform.GetChild(0).transform.localScale.y;
        defaultScale = new Vector3(0, scaleY, this.transform.GetChild(0).transform.localScale.z);
        this.transform.GetChild(0).transform.localScale = defaultScale;
    }

    void FixedUpdate(){
        isPlayer = Physics2D.OverlapBox(this.transform.position, sizeCol, 0, player);
        if(isPlayer && countAppear != 1){
            countAppear = 1;
            LeanTween.scale(this.transform.GetChild(0).gameObject, new Vector3(scaleX, scaleY, 1f), timeScale).setEase(LeanTweenType.linear);
            Debug.Log("abv");
        } else if(!isPlayer && countAppear != 0) {
            countAppear = 0;
            LeanTween.scale(this.transform.GetChild(0).gameObject, defaultScale, timeScale).setEase(LeanTweenType.linear);
            Debug.Log("abs");
        }
    }
    
}
