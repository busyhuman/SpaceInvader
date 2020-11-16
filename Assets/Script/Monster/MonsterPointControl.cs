using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPointControl : MonoBehaviour
{
    public Transform player;
    public GameObject[] points;
    private int current = 1;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        float curX = points[current].GetComponent<Transform>().position.x;
        if (curX - player.position.x < 30 )
        {
            points[current].SetActive(true);
            if(current < points.Length - 1)
                current += 1;
        }
    }
}
