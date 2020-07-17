using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public float fspeed = 0.0f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * fspeed);

        if (transform.position.x < -62.0f)
            transform.position = new Vector3(30.0f, 0, 0);
    }
}
