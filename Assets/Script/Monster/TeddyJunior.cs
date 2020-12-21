using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyJunior : MonsterBehavior
{
    private Vector3 DestPosition;
    protected override void AttackBehavior() {}
    protected override void MovingBehavior() {}
    // Start is called before the first frame update
    void Start()
    {
        curHP = maxHP;
        SetRandomDestination();
    }

    private void SetRandomDestination()
    {
        float x = Random.Range(-12.0f, 12.0f);
        float y = Random.Range(-4.0f, 4.8f);
        DestPosition = new Vector3(x, y, 0);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 Normal = Vector3.Normalize(DestPosition - transform.position);

        if (Vector3.Distance(DestPosition, transform.position) < 0.3f)
        {
            SetRandomDestination();
            return;
        }
        if(Normal.x < 0)
            transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
        else
            transform.localScale = new Vector3(-0.3f, 0.3f, 1.0f);

        transform.position += Normal * fMovingSpeed * Time.deltaTime;
        CheckDestroty();
    }

}
