using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] Transform transPlayer;
    [SerializeField] Animator anim;

    protected float speed;
    private int countJump = 0;
    private float countTime = 0f;
    protected float force; 
    private bool isJump;
    private bool isGround;

    Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        isJump = true;
        isGround = true;
        speed = SaveManage.Instance.GetSpeed();
        force = SaveManage.Instance.GetForce();
        scale = SaveManage.Instance.GetScale();
        scale = new Vector3(1,1,1);   
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

    public void Move(float speed)
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.velocity = new Vector2(-speed, player.velocityY);
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
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && isJump)
        {
            // player.AddForce(new Vector2(0,10 * ++countJump *force));
            player.velocity = new Vector2(player.velocityX,  4 * force);
            isJump = false;
            isGround = false;
            countJump++;
            anim.Play("playerJumpUp");
            countTime = 0;
        }
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Space)){
            isJump = true;
        }
    }

    public void Idle(){
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

    public void SetSpeed(float speed){
        this.speed = speed;
    }
    public void SetScale(Vector3 scale){
        this.scale = scale;
    }
    public void SetForce(float force){
        this.force = force;
    }

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
