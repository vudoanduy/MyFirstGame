using UnityEngine;

public class ManageEnemy : MonoBehaviour
{
    GameObject enemy;

    void Start(){
        enemy = GameObject.Find("Enemy").gameObject;
        GameObject enemy1 = Instantiate(enemy);
        enemy1.transform.localPosition -= new Vector3(15,0,0);
        enemy1.tag = "Enemy";
    }
}
