using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlemove : MonoBehaviour
{
    private Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        dir.x = Random.Range(-1.0f, 1.0f);
        dir.y = Random.Range(-1.0f, 1.0f);
        dir.Normalize();
        dir *= Random.Range(1.0f, 2.0f);
    }

    // Update is called once per frame 
    void Update()
    {
        transform.Translate(dir.x *Time.deltaTime , dir.y * Time.deltaTime , 0, Space.Self);
        Color c = GetComponent<SpriteRenderer>().color;
        c.a -= Time.deltaTime;
        GetComponent<SpriteRenderer>().color = c;
        if (c.a <= 0)
            Destroy(gameObject);
    }
}
