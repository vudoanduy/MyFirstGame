using UnityEngine;

public class BGAutoPlay : MonoBehaviour
{
    Material material;

    [Header("Setting parameters")]
    [Range(0,1)]
    public float scrollSpeed = 0.2f;

    //
    void Start()
    {
        material = gameObject.GetComponent<Renderer>().material;        
    }

    void Update()
    {
        material.mainTextureOffset += Vector2.right * scrollSpeed * Time.deltaTime;
    }
}
