using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private int iWinState = 0; // 0:idle, 1 : win, 2: winRight , 3: winCenter , 4: winLeft , 5 : 찐 끝
    public int iDieState = 0;
    private Vector3 DyingPos;
    public bool bDie = true;


    private float SceneElapsedTime = 0;
    private bool canShoot = true;
    private float shootTimer = 0.0f;
    private float shootDelay = 0.2f;
    private Camera camera;
    private GameObject Fade;

    public GameObject Bullet;
    public float moveSpeed = 5.0f;
    private AudioSource audioSource;
    public GameObject winText;
    public GameObject overText;
    // Start is called before the first frame update
    void Start()
    {
        overText = GameObject.Find("OverText");
        Fade = GameObject.Find("Fade");
        audioSource = GetComponent<AudioSource>();
        GameObject sfxvolume = GameObject.Find("AudioController");
        if (sfxvolume)
            audioSource.volume = sfxvolume.GetComponent<AudioController>().SFXVolume / 100.0f;

        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        winText = GameObject.Find("WinText");
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
                shootTimer += Time.deltaTime;
                float yPos = (-4.0f * shootTimer * shootTimer + 8 * shootTimer) ;
                transform.position = new Vector3(DyingPos.x - shootTimer *2.5f, DyingPos.y + yPos, 0);
                if (shootTimer * 40 < 180)
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, shootTimer * 20));
                if (transform.position.y < -9)
                {
                    iDieState = 2;
                    Fade.GetComponent<CameraFade>().SetTrigger();
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
        winText.GetComponent<Text>().enabled = true ;

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
            shootTimer = 0;
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
                shootTimer += Time.deltaTime;
                if (shootTimer > 3)
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
            Fade.GetComponent<CameraFade>().SetTrigger();
        }
    }
    private void IdleUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 MousePosition = Input.mousePosition;
            MousePosition = camera.ScreenToWorldPoint(MousePosition);


            float inputY = (MousePosition.y - transform.position.y) / 2.0f;
            float moveY = inputY * moveSpeed * Time.deltaTime;

            float inputX = (MousePosition.x - transform.position.x) / 2.0f;
            float moveX = inputX * moveSpeed * Time.deltaTime;


            transform.Translate(new Vector3(moveX, moveY, 0), Space.World);
        }


        if (canShoot) // 쏠 수 있는 상태인지 검사합니다.
        {
            if (shootTimer > shootDelay) //쿨타임이 지났는지와, 공격키인 스페이스가 눌려있는지 검사합니다.
            {
                audioSource.Play();
                Instantiate(Bullet, transform.position, Quaternion.identity); //레이저를 생성해줍니다.
                shootTimer = 0; //쿨타임을 다시 카운트 합니다.
            }
            shootTimer += Time.deltaTime; //쿨타임을 카운트 합니다.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(bDie)
        {
            if(collision.tag == "BossBullet" || collision.tag == "MonsterBullet")
            {
                if(iDieState == 0 && iWinState == 0)
                {
                    GameObject.Find("BOSS").GetComponent<BossSound>().PlayLaugh();
                    iDieState = 1;

                    shootTimer = 0;
                    DyingPos = transform.position;
                }
            }
        }
    }
}


