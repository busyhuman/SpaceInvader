using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosnterSpawner : MonoBehaviour
{
    public GameObject Monster;
    float fTime = 0;
    float fBreakTime = 0;
    bool Spawning = true;
    int Count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Spawning)
        {
            fTime += Time.deltaTime;
            if(fTime >= 0.5f)
            {
                GameObject newMonster = Instantiate(Monster, transform);
                fTime = 0.0f;
                Count++;
            }
            if (Count > 4) Spawning = false;

        }
        else
        {
            fBreakTime += Time.deltaTime;
            if(fBreakTime > 1.5f)
            {
                fBreakTime = 0.0f;
                Spawning = true;
                Count = 0;
            }
        }
    }
}
