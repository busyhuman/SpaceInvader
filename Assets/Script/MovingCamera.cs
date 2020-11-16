using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    public float MovingSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {

        GameObject MonsterListControl = GameObject.Find("Monsters");
        if (MonsterListControl)
            MonsterListControl.GetComponent<MonsterPointControl>().player = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * MovingSpeed * Time.deltaTime;
    }
}
