using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisMgr : MonoBehaviour
{
    public Vector3[,] TetrisArray; // (x 위치, y위치, 쌓인 여부)
    public float fBlockSize = 1.1f;
    public GameObject pBlock;
    public bool CanBool = false;

    public Vector3[,] TetrisPos;



    // Start is called before the first frame update
    void Start()
    {
        TetrisPos = new Vector3[20, 11];
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                TetrisPos[i, j].x = -12.75f + i * 1.05f;
                TetrisPos[i, j].y = -5.55f + 1.05f * j;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CheckBomb()
    {
        bool bBomb = false;
        int iBombRaw = 0;
        for(int i = 0; i< 20; i++) 
        {
            int iRaw = 0;
            for(int j = 0; j< 11; j++ )
            {
                if (TetrisPos[i, j].z != 0)
                    iRaw++;
            }
            if (iRaw == 20)
            {
                iBombRaw = i;
                bBomb = true;
                break;
            }
            
        }

        if(bBomb)
        {
            for(int k = 0; k< transform.childCount; k++)
            {
                GameObject block = transform.GetChild(k).gameObject;
                block.GetComponent<TetrisBlock>().BombRaw(iBombRaw);
            }
        }
    }
    public void SetStopTetris(int _X, int _Y)
    {
        TetrisPos[_X, _Y].z = 1;
    }
    public bool CheckStacked(int _X, int _Y)
    {
        if (TetrisPos[_X - 1, _Y].z == 1) return true;
        else return false;
    }
}
