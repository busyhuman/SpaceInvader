using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove2 : MonoBehaviour
{
    public float fSpeed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * 10);
        transform.Translate(Vector3.up * fSpeed * Time.deltaTime);
    }
}
