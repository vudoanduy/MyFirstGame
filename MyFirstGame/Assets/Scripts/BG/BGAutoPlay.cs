using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAutoPlay : MonoBehaviour
{
    Material material;
    private float scrollSpeed = 0.2f;

    void Start()
    {
        material = gameObject.GetComponent<Renderer>().material;        
    }

    void Update()
    {
        material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime / 5f, 0);
    }
}
