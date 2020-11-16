using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class  MonsterBulletBehavior : MonoBehaviour
{
    public float MovingSpeed = 1;
    protected abstract void MovingBehavior();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovingBehavior();
    }
}
