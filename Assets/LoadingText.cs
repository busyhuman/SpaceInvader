using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{
    // Start is called before the first frame 
    public GameObject cameraobj;
    public string[] texts;
    public float fElapsedTime;
    private Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        fElapsedTime += Time.deltaTime / 2;
        int index = (int)(fElapsedTime * 10) % 10;
        text.text = texts[index];
        if (fElapsedTime >3.5)
            cameraobj.GetComponent<SceneChanger>().TurnToStage1Boss();
    }
}
