using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetMacXMove : MonoBehaviour
{
    public float speedUp = 2f;

    Vector3 posStart;

    Rigidbody2D rb;

    void Start(){
        posStart = this.gameObject.transform.position;
        rb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        MoveUp();
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Ground" || other.gameObject.tag == "Player"){
            rb.simulated = false;
        }
    }

    public void MoveUp(){
        if(this.transform.position.y >= posStart.y){
            rb.simulated = true;
        }
        if(!rb.simulated){
            this.transform.position = new Vector3(posStart.x, this.transform.position.y + speedUp * Time.fixedDeltaTime, posStart.z);
        }
    }
}
