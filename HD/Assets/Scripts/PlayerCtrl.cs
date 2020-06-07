using UnityEngine;
using System.Collections;
using UnityEditorInternal;

public class PlayerCtrl : MonoBehaviour
{
    public float speed = 20.0f;

    Vector2 touchPosition;
    Camera camera = null;
    bool isMove = false;


    void Awake()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }



    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if(t.phase == TouchPhase.Began)
            {
                touchPosition = t.position;
                touchPosition = camera.ScreenToWorldPoint(touchPosition);
                isMove = true;
            }
        }

        if (isMove)
        {
            if (Vector2.Distance(transform.position, touchPosition) < 0.01f)
            {
                isMove = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, touchPosition, speed * Time.deltaTime);
            }
                
        }
        
    }

}