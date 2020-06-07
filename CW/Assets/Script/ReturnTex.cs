using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTex : MonoBehaviour
{
    public Texture2D myTex;
    // Start is called before the first frame update
    void Start()
    {
        myTex = GetComponent<SpriteRenderer>().sprite.texture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
