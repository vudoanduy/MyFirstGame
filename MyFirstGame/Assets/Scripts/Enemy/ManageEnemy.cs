using UnityEngine;

public class ManageEnemy : MonoBehaviour
{
    GameObject enemy;

    void Start(){
        SetUpStart();
        // SpawnEnemy();
    }

    //
    public void SetUpStart(){
        InitializatingObject();
    }

    public void InitializatingObject(){
        enemy = GameObject.Find("Enemy").gameObject;
    }

    //
    public void SpawnEnemy(){
        GameObject enemy1 = Instantiate(enemy);
        enemy1.transform.localPosition -= new Vector3(15,0,0);
        enemy1.tag = "Enemy";
    }
}
