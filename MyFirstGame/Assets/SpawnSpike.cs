using UnityEngine;

public class SpawnSpike : MonoBehaviour
{
    [Header("GameObject (Prefabs)")]
    [SerializeField] GameObject spikeMac;

    float countTime = 0;

    void Start(){
        Spawn();
    }

    void FixedUpdate(){
        countTime += Time.deltaTime;

        if(countTime < 1) return;
        countTime = 0;
        Spawn();
    }

    public void Spawn(){
        GameObject newSpike = Instantiate(spikeMac);
        newSpike.transform.SetParent(this.transform);
        newSpike.transform.position = this.transform.position;
    }
}
