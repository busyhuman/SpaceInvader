using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    public int iScore = 0;
    private int iIndex = 0;

    void Awake()
    {
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    IEnumerator UpdateScore()
    {
        while(iIndex <6)
        {
            transform.GetChild(iIndex).GetComponent<ScoreUnit>().bJump = true;
            iIndex++;
            yield return new WaitForSeconds(0.08f);
        }
        iIndex = 0;
    }
}
