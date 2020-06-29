using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisMgr : MonoBehaviour
{
    public Vector3[,] TetrisArray; // (x 위치, y위치, 쌓인 여부)
    public float fBlockSize = 1.1f;
    public GameObject pBlock;
    public Vector3 TetrisPos; 
    // Start is called before the first frame update
    void Start()
    {
        TetrisPos = new Vector3(-12.2f + 0.55f, -5,0);
        TetrisArray = new Vector3[6,10];
        for (int i = 0; i<6; i++)
        {
            for(int j = 0; j < 10;  j++)
            {
                TetrisArray[i, j] = new Vector3(i * fBlockSize, j * fBlockSize, 0) + TetrisPos;
                //GameObject block = Instantiate(pBlock);
                //block.transform.position = TetrisArray[i, j];
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
