using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public TetrisMgr TetMgr;
    private float fMoveSpeed;
    float fAngle = 0;
    public bool bMove = true;
    public int iXPos = 30;
    public int iYPos = 00;
    public int iXPos2 = 0;
    public int iYPos2 = 0;
    public int iStack = 0;
    public bool bHold = true;
    // Start is called before the first frame update

    void Start()
    {
       
        fMoveSpeed = 0.55f;
    }

    public void Launch()
    {
        bHold = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public  bool Move()
    {

        GameObject TetrisMgr = GameObject.Find("ShootMgr");
        Vector3[,] TetrisPos = TetrisMgr.GetComponent<TetrisMgr>().TetrisPos;

        if (bMove)
        {
            //float fDegree = 3.14f * fAngle / 180.0f;
            float PosX = TetrisPos[iXPos, iYPos].x;
            float PosY = TetrisPos[iXPos, iYPos].y;
            transform.position = new Vector3(PosX, PosY, 0);
            iXPos--;

            if (TetMgr.GetComponent<TetrisMgr>().CheckStacked(iXPos, iYPos) || iXPos <= 1)
            {
                SetStop();
                bMove = false;
                return true;
            }
            else return false;

        }
        else return true;

    }
    public void Initialize(float _fAngle, float _fSpeed)
    {
        fMoveSpeed = _fSpeed;
        fAngle = _fAngle;
    }

    public void SetStop()
    {
        bMove = false;
        GameObject TetrisMgr = GameObject.Find("ShootMgr");
        TetrisMgr.GetComponent<TetrisMgr>().SetStopTetris(iXPos, iYPos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            bMove = false;
    }
    public void DestroyBullet()
    {
    
        if (!bHold)
        {
            gameObject.transform.parent.GetComponent<TetrisBlock>().GetDamage(10);

        }
    }
}
