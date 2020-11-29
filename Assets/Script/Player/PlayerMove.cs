using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private int iWinState = 0; // 0:idle, 1 : win, 2: winRight , 3: winCenter , 4: winLeft , 5 : 찐 끝
    public int iDieState = 0;
    private Vector3 DyingPos;
    public bool bDie = false;
    public PlayerAtt AttackBehavior;

    private float MovingTimer = 0.0f;

    private float SceneElapsedTime = 0;
    private Camera camera;
    private GameObject Fade;

    public float moveSpeed = 5.0f;
    private AudioSource audioSource;
    public GameObject winText;
    public GameObject overText;
    // Start is called before the first frame update
    void Start()
    {
        AttackBehavior = gameObject.GetComponent<PlayerAtt>();
        overText = GameObject.Find("OverText");
        Fade = GameObject.Find("Fade");
        audioSource = GetComponent<AudioSource>();
        GameObject sfxvolume = GameObject.Find("AudioController");
        if (sfxvolume)
            audioSource.volume = sfxvolume.GetComponent<AudioController>().SFXVolume / 100.0f;

         camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        winText = GameObject.Find("youwin");
        winText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        SceneElapsedTime += Time.deltaTime;
        switch(iDieState)
        {
            case 0:
                WinUpdateProcess();
                break;
            case 1:
                MovingTimer += Time.deltaTime;
                float yPos = (-4.0f * MovingTimer * MovingTimer + 8 * MovingTimer) ;
                transform.position = new Vector3(DyingPos.x - MovingTimer *2.5f, DyingPos.y + yPos, 0);
                if (MovingTimer * 40 < 180)
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, MovingTimer * 20));
                if (transform.position.y < -9)
                {
                    iDieState = 2;
                    Fade.GetComponent<CameraFade>().SetTrigger();
                    Fade.GetComponent<CameraFade>().bWin = false;
                    overText.GetComponent<Text>().enabled = true;

                }
                break;
        }
    }

    public void TurnToWinMode(){     if(iWinState == 0 )   iWinState = 1; }
    public void TurnToWin2Mode() { if (iWinState == 1) iWinState = 2; }

    public void TurnToWin3Mode()
    {
        int WinnningScore = 18000 - (int)(SceneElapsedTime * 100);
        iWinState = 3;
        GameObject.Find("GameManager").GetComponent<ScoreMgr>().UpdateScore(20000 + WinnningScore);
        winText.SetActive(true);

    }

    private void WinUpdate()
    {
        Vector2 Centerpos = new Vector2(-12, 0);
        float inputY = (Centerpos.y - transform.position.y) / 2.0f;
        float moveY = inputY * 3 * Time.deltaTime;

        float inputX = (Centerpos.x - transform.position.x) / 2.0f;
        float moveX = inputX * 3 * Time.deltaTime;

        transform.Translate(new Vector3(moveX, moveY, 0), Space.World);
    }

    private void Win2Update()
    {
        Vector2 Centerpos = new Vector2(0, 0);
        float inputX = (Centerpos.x - transform.position.x) / 1.5f;
        float moveX = inputX * 3 * Time.deltaTime;

        transform.Translate(new Vector3(moveX, 0, 0), Space.World);
        if (Vector2.Distance(Centerpos, new Vector2(transform.position.x, transform.position.y)) < 0.2)
        {
            TurnToWin3Mode();
            MovingTimer = 0;
        }
    }

    private void WinUpdateProcess()
    {
        switch (iWinState)
        {
            case 0:
                IdleUpdate();
                break;
            case 1:
                WinUpdate();
                break;
            case 2:
                Win2Update();
                break;
            case 3:
                MovingTimer += Time.deltaTime;
                if (MovingTimer > 3)
                {
                    iWinState = 4;
                    winText.SetActive(false);
                }
                break;
            case 4:
                Win4Update();
                break;
            default:
                break;
        }
    }
    private void Win4Update() // 다음씬
    {
        Vector2 Centerpos = new Vector2(15, 0);
        float inputX = (Centerpos.x - transform.position.x) / 1.5f;
        float moveX = inputX * 3 * Time.deltaTime;

        transform.Translate(new Vector3(moveX, 0, 0), Space.World);
        if (Vector2.Distance(Centerpos, new Vector2(transform.position.x, transform.position.y)) < 0.2)
        {
            iWinState = 5;
            Fade.GetComponent<CameraFade>().bWin = true;
            Fade.GetComponent<CameraFade>().SetTrigger();
        }
    }
    private void IdleUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 MousePosition = Input.mousePosition;
            if (MousePosition.y < 100) return;
            
            MousePosition = camera.ScreenToWorldPoint(MousePosition);
            if (MousePosition.x < -13) MousePosition.x = -13;
            if (MousePosition.y < -7.8) MousePosition.y = -7.8f;
            if (MousePosition.y > 7.8) MousePosition.y = 7.8f;

            float inputY = (MousePosition.y - transform.position.y) / 2.0f;
            float moveY = inputY * moveSpeed * Time.deltaTime;

            float inputX = (MousePosition.x - transform.position.x) / 2.0f;
            float moveX = inputX * moveSpeed * Time.deltaTime;


            transform.Translate(new Vector3(moveX, moveY, 0), Space.World);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(bDie)
        {
            if(collision.tag == "BossBullet" )
            {
                if(iDieState == 0 && iWinState == 0)
                {
                    GameObject.Find("BOSS").GetComponent<BossSound>().PlayLaugh();
                    iDieState = 1;

                    MovingTimer = 0;
                    DyingPos = transform.position;
                }
            }
            else if(collision.tag == "MonsterBullet2" || collision.tag =="Obstacle2" || collision.tag == "Monster2")
            {
                if (iDieState == 0 && iWinState == 0)
                {
                    iDieState = 1;

                    MovingTimer = 0;
                    DyingPos = transform.position;
                }
            }
        }
    }
}


