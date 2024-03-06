using UnityEngine;

public class rotate : MonoBehaviour
{
    public float speedRotate = 150;
    void Update(){
        this.transform.Rotate(Vector3.forward * Time.deltaTime * speedRotate);
    }
}
