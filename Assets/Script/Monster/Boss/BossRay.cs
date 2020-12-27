using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRay : MonoBehaviour
{
    // Start is called before the first frame update
    public float MovingSpeed = 3.0f;
    private int childCount = 13;
    private GameObject[] childs;
    void Start()
    {
        transform.parent = null;
        childCount = transform.childCount;
        childs = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
            childs[i] = transform.GetChild(i).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i< childCount; ++i)
        {
           float posx = childs[i].transform.position.x;
            if (posx > 9)
                childs[i].SetActive(false);
            else
                childs[i].SetActive(true);
        }
        transform.Translate(Vector3.left * Time.deltaTime * MovingSpeed);
        if (transform.position.x < -25)
            GameObject.Destroy(gameObject);

    }
}
