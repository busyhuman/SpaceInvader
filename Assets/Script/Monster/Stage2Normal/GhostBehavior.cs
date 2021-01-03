using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehavior : MonsterBehavior
{
    protected override void AttackBehavior() { }
    protected override void MovingBehavior() { }
    // Start is called before the first frame update

    // Update is called once per frame
    void  Update()
    {
         MovingBehavior();
         AttackBehavior();
         CheckDestroty();
    }
}
