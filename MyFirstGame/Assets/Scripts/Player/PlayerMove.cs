using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] Transform transPlayer;
    [SerializeField] Animator anim;

    protected float speed = 8f;
    private int countJump = 0;
    private float countTime = 0f;
    protected float force = 10f;
    private bool isJump;
    private bool isGround;
    // Start is called before the first frame update
    void Start()
    {
        isJump = true;
        isGround = true;    
    }

    // Update is called once per frame
    void Update()
    {
        Move(speed);
        Jump(force);
        Idle();
        if(countTime > 0.3f && !isGround){
            anim.Play("playerJumpDown");
        }
    }

    protected void Move(float speed)
    {
        if(Input.GetKey(KeyCode.A))
        {
            player.velocity = new Vector2(-speed, player.velocityY);
            transPlayer.localScale = new Vector3(-1,1,1);
            if(isGround){
                anim.Play("playerRun");
            }
        }
        if(Input.GetKey(KeyCode.D))
        {
            player.velocity = new Vector2(speed, player.velocityY);
            transPlayer.localScale = new Vector3(1,1,1);
            if(isGround){
                anim.Play("playerRun");
            }
        }
    }
    
    protected void Jump(float force)
    {
        countTime += Time.deltaTime;
        if(countJump > 1){
            return;
        }
        if (Input.GetKeyDown(KeyCode.W) && isJump)
        {
            // player.AddForce(new Vector2(0,10 * ++countJump *force));
            player.velocity = new Vector2(player.velocityX,  4 * force);
            isJump = false;
            isGround = false;
            countJump++;
            anim.Play("playerJumpUp");
            countTime = 0;
        }
        if(Input.GetKeyUp(KeyCode.W)){
            isJump = true;
        }
    }

    protected void Idle(){
        if(player.velocityX == 0 && player.velocityY == 0){
            anim.Play("playerIdle");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Layer1")
        {
            isJump = true;
            isGround = true;
            countJump = 0;
        }
    }
}
