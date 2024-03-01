using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{   
    [Header("Rigidbody2D")]
    [SerializeField] Rigidbody2D player;

    [Header("Transform")]
    [SerializeField] Transform transPlayer;

    [Header("Animator")]
    [SerializeField] Animator anim;

    [Header("Transform")]
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform wallCheck;
    [SerializeField] Transform goreCheck;

    [Header("LayerMask")]
    [SerializeField] LayerMask Ground;
    [SerializeField] LayerMask Board;


    private int countJump = 0;

    private float countTime = 0f, force, speed;

    public float wallSlidingSpeed = -1;

    private bool isJump, isGround, isWall, isSliding;

    public Vector2 wallJumpForce = new Vector2(30,40);

    Vector3 scale;
    
    //
    void Start()
    {
        SetUpStart(); 
    }

    void Update()
    {   
        isGround = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.6f, 0.15f), 0, Ground);
        isWall = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.2f, 0.6f), 0, Ground);

        if(isGround){
            isJump = true;
            countJump = 0;
        }

        if(isWall){
            if(Input.GetKey(KeyCode.Space)){
                player.velocity = new Vector2(-wallJumpForce.x * transform.localScale.x, wallJumpForce.y);
            }
            isSliding = true;
        } else{
            isSliding = false;
        }

        if(isSliding){
            player.velocity = new Vector2(player.velocityX, Math.Clamp(player.velocityY, wallSlidingSpeed, float.MaxValue));
        }



        Move(speed);
        Jump(force);
        Idle();
        if(countTime > 0.3f && !isGround){
            anim.Play("playerJumpDown");
        }
    }

    //
    public void SetUpStart(){
        SetParameter();
    }

    public void SetParameter(){
        isJump = true;
        speed = SaveManage.Instance.GetSpeed();
        force = SaveManage.Instance.GetForce();
        scale = SaveManage.Instance.GetScale();
        scale = transform.localScale;
    }

    //
    public void Move(float speed)
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.velocity = new Vector2(-speed, player.velocityY);
            if(isWall && !isGround){
                player.velocity = new Vector2(0, player.velocityY);               
            }
            Vector3 scaleX = scale;
            scaleX.x = -MathF.Abs(scaleX.x);
            transPlayer.localScale = scaleX;
            if(isGround){
                anim.Play("playerRun");
            }
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            player.velocity = new Vector2(speed, player.velocityY);
            if(isWall && !isGround){
                player.velocity = new Vector2(0, player.velocityY);               
            }
            Vector3 scaleX = scale;
            scaleX.x = MathF.Abs(scaleX.x);
            transPlayer.localScale = scaleX;
            if(isGround){
                anim.Play("playerRun");
            }
        }
    }
    
    public void Jump(float force)
    {
        countTime += Time.deltaTime;
        if(countJump > 1){
            return;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) && isJump)
        {
            player.velocity = new Vector2(player.velocityX,  4 * force);
            isJump = false;
            countJump++;
            anim.Play("playerJumpUp");
            countTime = 0;
        }
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)){
            isJump = true;
        }
    }

    public void Idle(){
        if(player.velocityX == 0 && player.velocityY == 0){
            anim.Play("playerIdle");
        }
    }

    //
    public void SetSpeed(float speed){
        this.speed = speed;
    }
    public void SetScale(Vector3 scale){
        this.scale = scale;
    }
    public void SetForce(float force){
        this.force = force;
    }

    //
    public float GetSpeed(){
        return this.speed;
    }
    public Vector3 GetScale(){
        return this.scale;
    }
    public float GetForce(){
        return this.force;
    }
}
