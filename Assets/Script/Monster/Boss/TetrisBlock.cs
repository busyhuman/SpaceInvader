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
    public int iType = 1;    // 블록 타입

    public Vector2 Blocksize;
    public int iHP = 100;
    public int iTetrisYIndex = 0;
    private Vector3[,] TetrisPos;
    public bool bHold = true;

    public Vector3 Center;


    public virtual void CreateBlock()
    {

        GameObject mgr = GameObject.Find("ShootMgr");
        TetrisPos = GameObject.Find("ShootMgr").GetComponent<TetrisMgr>().TetrisPos;
        int iCurrentY = GameObject.Find("BOSS").GetComponent<BossBehavior>().iCurrentMoveIndex / 2;
        float xPos = 0;
        float yPos = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            BossBullet pbullet = transform.GetChild(i).GetComponent<BossBullet>();
            pbullet.iXPos = 25 - (int)(Blocksize.x) + pbullet.iXPos2;
            pbullet.iYPos = 4 - (int)(Blocksize.y) + pbullet.iYPos2;
            pbullet.TetMgr = mgr.GetComponent<TetrisMgr>();
        }

    }
    public IEnumerator Move()
    {
        bool bS = false;
        if(!bStoped)
        {
            
            for(int i = 0; i< transform.childCount; i++)
          {
                bS =  transform.GetChild(i).GetComponent<BossBullet>().Move();
            }
        }
    
        if(bS)
        {
            bStoped = true;
            SetStop();
        }
        yield return new WaitForSeconds(0.3f);
        StartCoroutine("Move");

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
           
             int Ypos = iCurrentY - iY + (int)(Blocksize.y);
             child.GetComponent<BossBullet>().iYPos = Ypos;
            child.GetComponent<BossBullet>().Launch();

        }

        StartCoroutine("Move");
    }
    public void SetStop()
    {
        
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.GetComponent<BossBullet>().SetStop();
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
}
