using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    ManageMusic manageMusic;

    void Start(){
        manageMusic = GameObject.Find("ManageAudio").GetComponent<ManageMusic>();
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Block"){
            Destroy(other.gameObject);
            manageMusic.DestroyBlock();
        }
    }
}
