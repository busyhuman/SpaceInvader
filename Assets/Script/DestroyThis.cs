using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour
{
    public float time = 5.0f;
    void Start()
    {
        Destroy(this.gameObject, time);
    }

}
