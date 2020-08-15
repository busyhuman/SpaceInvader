using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDiamondShape : MonoBehaviour
{
    public GameObject Bubble;
    public int DiamondLength = 3;
    public float BubbleLength = 1.2f;
    public float BiggerTick = 0.0f;
    public float BiggerSpeed = 0.2f;
    private void Awake()
    {
        MakeDiamond();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BiggerTick += Time.deltaTime;
        if (DiamondLength > 7)
        {
            if(BiggerTick > 5.0f)
            {
                transform.parent.gameObject.GetComponent<BossBehavior>().bShiled = false;
               Destroy(gameObject);
            }
        }
        else
        {
            if (BiggerTick > BiggerSpeed)
            {
                DiamondLength++;
                MakeDiamond();
                BiggerTick = 0;
            }
        }
    }
    void MakeDiamond()
    {
        for(int i = 0; i< transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        float DiaLength = BubbleLength * (DiamondLength - 1);

        Vector3 Top = new Vector3(0, DiaLength, 0) + transform.position;
        Vector3 Right = new Vector3(DiaLength, 0, 0) + transform.position;
        Vector3 Left = new Vector3(-DiaLength, 0, 0) + transform.position;
        Vector3 Bottom = new Vector3(0, -DiaLength, 0) + transform.position;

        Vector3 pos;
        Instantiate(Bubble, Top, Quaternion.identity,transform); // top
        for(pos = Top; pos.x < Right.x-1; )
        {
            pos.x += BubbleLength;
            pos.y -= BubbleLength;
            Instantiate(Bubble, pos, Quaternion.identity, transform); 
        }
        Instantiate(Bubble, Right, Quaternion.identity, transform); 
        for (pos = Right; pos.x > Bottom.x+1;)
        {
            pos.x -= BubbleLength;
            pos.y -= BubbleLength;
            Instantiate(Bubble, pos, Quaternion.identity, transform);
        }
        Instantiate(Bubble, Bottom, Quaternion.identity, transform); 
        for (pos = Bottom; pos.x > Left.x+1;)
        {
            pos.x -= BubbleLength;
            pos.y += BubbleLength;
            Instantiate(Bubble, pos, Quaternion.identity, transform); 
        }
        Instantiate(Bubble, Left, Quaternion.identity, transform);

        for (pos = Left; pos.x < Top.x-1;)
        {
            pos.x += BubbleLength;
            pos.y += BubbleLength;
            Instantiate(Bubble, pos, Quaternion.identity, transform);
        }
    }
}
