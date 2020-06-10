using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public float fAngle = 50.0f;
    public float fSpeed = 2.0f;
    public int iType = 0;
    private float fLiveTime = 0;
    // Start is called before the first frame update
    private void Awake()
    {
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 LookDirection = Vector3.forward;

        transform.Rotate(0, 0, Time.deltaTime * fAngle);
        transform.Translate(Vector3.up * fSpeed * Time.deltaTime);
        fLiveTime += Time.deltaTime;
        if (fLiveTime > 20.0f) Destroy(gameObject);
    }



}
