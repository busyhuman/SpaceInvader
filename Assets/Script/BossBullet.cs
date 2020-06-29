using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private float fMoveSpeed;
    float fAngle = 0;
    bool bMove = true;

    // Start is called before the first frame update

    void Start()
    {
        fMoveSpeed = 0.5f;
        transform.parent = null;
        StartCoroutine("Move");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Move()
    {
        if(bMove)
        {

            float fDegree = 3.14f * fAngle / 180.0f;
            float PosX = Mathf.Cos(fDegree) * fMoveSpeed + transform.position.x;
            float PosY = Mathf.Sin(fDegree) * fMoveSpeed + transform.position.y;
            transform.position = new Vector3(PosX, PosY, 0);

            yield return new WaitForSeconds(0.1f);
            StartCoroutine("Move");
        }

    }
    public void Initialize(float _fAngle, float _fSpeed)
    {
        fMoveSpeed = _fSpeed;
        fAngle = _fAngle;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            bMove = false;
        else if(collision.gameObject.tag == "MonsterBullet")
        {
            if (transform.position.x > collision.transform.position.x)
                bMove = false;
        }
    }
}
