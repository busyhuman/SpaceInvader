using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public bool bStoped = false;
    public GameObject Tile;
    private ScoreMgr ScoreMgr;
    private AudioSource audio;
    public AudioClip ExplosionSound;
    public AudioClip HitSound;

    public float TileSize; //타일 사이즈
    public float interval; // 타일 간격

    public Rigidbody2D rigid;
    public int iTileX = 3;
    public int iTileY = 2;      //블록 위치
    public int iType = 1;    // 블록 타입
    public int iHP = 100;
    public int iTetrisYIndex = 0;
    public int[] iTiles;
    private Vector3[,] TetrisPos;
    public bool bHold = true;

    public Vector3 Center;
    public Vector2 BlockArea; //블록 범위

    
    public virtual void CreateBlock()
    {
        TetrisPos = GameObject.Find("ShootMgr").GetComponent<TetrisMgr>().TetrisPos;
        int iCurrentY = GameObject.Find("BOSS").GetComponent<BossBehavior>().iCurrentMoveIndex / 2;
        float xPos = 0;
        float yPos = 0;

        for (int y = 0; y < iTiles.Length; y += iTileX)
        {
            for (int x = 0; x < iTileX; x++)    
            {
                xPos = (float)x * interval + transform.position.x;
                yPos = -(int)(y / iTileX) * interval + transform.position.y;

                int iType = iTiles[x + y];

                if (iType != 0)
                {
                    int Ypos = iCurrentY + -(int)(y / iTileX) + iTileY;
                    if (iCurrentY + iTileY > 10)
                        Ypos -= iTileY;
                    GameObject block = Instantiate(Tile, new Vector3(xPos, yPos, 0), Quaternion.identity, transform);
                    block.GetComponent<BossBullet>().iXPos = 19 - iTileX + x;
                    block.GetComponent<BossBullet>().iYPos = Ypos;


                    block.GetComponent<BossBullet>().iXPos2 = x;
                    block.GetComponent<BossBullet>().iYPos2 = (int)(y / iTileX);

                    block.GetComponent<BossBullet>().iStack = x;
                    block.transform.localScale = new Vector3(TileSize, TileSize, TileSize);
                    block.transform.SetParent(transform);
                }
            }
        }
        BlockArea.x = xPos - transform.position.x;
        BlockArea.y = yPos - transform.position.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        GameObject sfxvolume = GameObject.Find("AudioController");
        if (sfxvolume)
            audio.volume = sfxvolume.GetComponent<AudioController>().SFXVolume / 100.0f;
        audio.Play();

        ScoreMgr = GameObject.Find("GameManager").GetComponent<ScoreMgr>();
        CreateBlock();
    }

    // Update is called once per frame
    void Update()
    {
        GetCenter();
        if (!bStoped)
            CheckStop();
    }
    public void GetDamage(int iDam)
    {
        iHP -= iDam;
        if (iHP < 0)
        {
            GameObject.Destroy(gameObject);
            ScoreMgr.UpdateScore(323);
            audio.clip = ExplosionSound;
            audio.Play();
        }
        else
        {

            audio.clip = HitSound;
            audio.Play();

        }
    }
    public void Launch()
    {
        transform.parent = null;
        int nChilds = gameObject.transform.childCount;
        int iCurrentY = GameObject.Find("BOSS").GetComponent<BossBehavior>().iCurrentMoveIndex / 2;

        for (int i = 0; i < nChilds; i++)
        {
             GameObject child = transform.GetChild(i).gameObject;
            int iY = child.GetComponent<BossBullet>().iYPos2;
             int Ypos = iCurrentY - iY + 1;
             child.GetComponent<BossBullet>().iYPos = Ypos;
            child.GetComponent<BossBullet>().Launch();

        }

    }

    public void CheckStop()
    {
        GameObject mgr = GameObject.Find("ShootMgr");
        bool bTemp = false ;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            int xIndex = child.GetComponent<BossBullet>().iXPos;
            int yIndex = child.GetComponent<BossBullet>().iYPos;
            if (child.GetComponent<BossBullet>().iStack == 0 )
            {
                if(xIndex == 0)
                {
                    bTemp = true;
                    bStoped = true;
                    break;
                }
            }
            if (mgr.GetComponent<TetrisMgr>().CheckStacked(xIndex, yIndex))
            {
                bTemp = true;
                bStoped = true;
                break;
            }
        }

        if(bTemp)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                GameObject child = gameObject.transform.GetChild(i).gameObject;
                child.GetComponent<BossBullet>().bMove = false;
                child.GetComponent<BossBullet>().SetStop();
            }
        }
    }

    
    public void BombRaw(int Raw)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.GetComponent<BossBullet>().iXPos == Raw)
                Destroy(child);
        }
    }
    virtual public void GetCenter()
    {
        Vector3 position = transform.position;

        Center.x = position.x + BlockArea.x / 2.0f;
        Center.y = position.y + BlockArea.y / 2.0f;
    }
}
