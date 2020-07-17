using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private bool canShoot = true;
    private float shootTimer = 0.0f;
    private float shootDelay = 0.2f;
    private Camera camera;

    public GameObject Bullet;
    public float moveSpeed = 5.0f;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(Input.GetMouseButton(0))
        {
            Vector2 MousePosition = Input.mousePosition;
            MousePosition = camera.ScreenToWorldPoint(MousePosition);


            float inputY = (MousePosition.y  - transform.position.y) / 2.0f;
            float moveY= inputY * moveSpeed * Time.deltaTime;

            float inputX = (MousePosition.x - transform.position.x) / 2.0f;
            float moveX = inputX * moveSpeed * Time.deltaTime;


            transform.Translate(new Vector3(moveX, moveY,0),Space.World);
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

}
