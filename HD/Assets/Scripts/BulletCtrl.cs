using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float destroyTime = 5.0f;
    public float speed = 20.0f;
    
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

}
