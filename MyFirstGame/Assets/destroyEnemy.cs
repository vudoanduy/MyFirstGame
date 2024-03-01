using UnityEngine;

public class destroyEnemy : MonoBehaviour
{

    Rigidbody2D rbPlayer;
    public Vector2 force = new Vector2();

    void Start(){
        rbPlayer = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){
            Destroy(other.gameObject);
            rbPlayer.velocity = force;
        }
    }
}
