using System;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D enemy;
    Animator anim;
    Vector3 vector3;

    float pointA, pointB, startPosEnemy, currentPosEnemy, currentPoint;
    public float speed, distanceMove;

    //
    void Start(){
        SetUpStart();
        anim.Play("playerRun");
    }

    void FixedUpdate(){
        Move();
    }

    //
    public void SetUpStart(){
        SetParameter();
        InitializatingObject();
    }

    public void SetParameter(){
        SetPoint();
        vector3 = transform.localScale;
    }

    public void InitializatingObject(){
        enemy = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void SetPoint(){
        startPosEnemy = transform.localPosition.x;
        pointA = startPosEnemy - distanceMove;
        pointB = startPosEnemy + distanceMove;
        currentPoint = pointB;
    }

    //
    public void Move(){
        currentPosEnemy = transform.position.x;
        if(currentPoint == pointB){
            enemy.velocity = new Vector2(speed,0);
        } else {
            enemy.velocity = new Vector2(-speed,0);
        }
        if(MathF.Abs(currentPoint - currentPosEnemy) < 0.5f && currentPoint == pointB){
            currentPoint = pointA;
            vector3.x *= -1;
            transform.localScale = vector3;
        } 
        if(MathF.Abs(currentPoint - currentPosEnemy) < 0.5f && currentPoint == pointA){
            currentPoint = pointB;
            vector3.x *= -1;
            transform.localScale = vector3;
        } 
    }
}
