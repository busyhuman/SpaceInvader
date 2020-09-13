using System;
using UnityEngine;

public class DrawStatLine : MonoBehaviour
{
    public Vector3 originPos;
    private LineRenderer lineRenderer;
    public Vector3[] Positions = new Vector3[7];
    public float[] stats = new float[6]; // 공격력, 탄속도, 탄크기, 캐릭터 크기, 이동속도, 공격속도
    private bool bAnimated = false;
    // Start is called before the first frame update
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
            for (int i = 0; i < 6; i++)
            {
                float distance = Vector3.Distance(Positions[i], originPos);
                if (distance < stats[i] / 3.5f)
                {
                    float fShootAngle = i * 60 + 30;
                    float posX =  (float)Math.Cos(fShootAngle * Math.PI / 180);
                    float posY =  (float)Math.Sin(fShootAngle * Math.PI / 180);
                    Vector3 angle =  new Vector3(posX, posY, 0);
                    Positions[i] +=  angle * Time.deltaTime;
                }
            }
             Positions[6] = Positions[0];
            lineRenderer.SetPositions(Positions);
        
    }

    private void OnEnable()
    {
        bAnimated = false;
        for (int i = 0; i < 6; i++)
        {
            Positions[i] = originPos;
        }
    }
}
