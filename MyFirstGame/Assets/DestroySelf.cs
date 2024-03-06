using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] GameObject spikeMac;

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Ground"){
            Destroy(spikeMac);
        }
    }
}
