using Unity.VisualScripting;
using UnityEngine;

public class camController : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] GameObject pointCutScene1;

    [Header("Layer Mask")]
    [SerializeField] LayerMask playerMask;

    PlayerMove playerMove;

    Animator anim;

    bool isPlayer, isCutScene1 = true;

    void Start(){
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
        anim = this.gameObject.GetComponent<Animator>();
    }

    void FixedUpdate(){
        isPlayer = Physics2D.OverlapBox(pointCutScene1.transform.position, new Vector2(1,10), 0, playerMask);

        if(isPlayer && isCutScene1){
            anim.SetBool("cutscene1", true);
            playerMove.enabled = false;
            Invoke("ChangeCutScene2", 3);
            Invoke("ResumeGame", 6);
            Invoke("SetMove", 8);
            isCutScene1 = false;
        }
    }

    void ChangeCutScene2(){
        anim.SetBool("cutscene2", true);
    }

    void ResumeGame(){
        anim.SetBool("cutscene1", false);
    }
    void SetMove(){
        playerMove.enabled = true;
    }
}
