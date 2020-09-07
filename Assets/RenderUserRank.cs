using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RenderUserRank : MonoBehaviour
{
    public Text Rank;
    public Text ID;
    public Text Score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string _rank, string _ID, string _score)
    {
        int j = 0;
        string ScoreStr = "";
        for (int i = 0; i < 7 - _score.Length; i++)
            ScoreStr += "0";
        ScoreStr += _score;

        Score.text = ScoreStr;
        Rank.text = _rank;
        ID.text = _ID;
    }
}
