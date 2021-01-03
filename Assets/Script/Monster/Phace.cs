using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phace : MonoBehaviour
{
    public GameObject Next;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount==0)
        {
            if (Next)
                Next.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}
