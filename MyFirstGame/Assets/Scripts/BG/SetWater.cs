using UnityEngine;

public class SetWater : MonoBehaviour
{   
    [Header("GameObject")]
    [SerializeField] GameObject water;
    [SerializeField] GameObject waterChild;

    public int number;

    void Start(){
        SetPos();
    }

    public void SetPos(){
        for(int i = 0; i < number; i++){
            GameObject newWater = Instantiate(waterChild);
            newWater.transform.position = waterChild.transform.position + Vector3.right * 1.28f * (i+1);
            newWater.transform.SetParent(water.transform);
        }
    }

}
