using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour
{
    // Start is called before the first frame update
    private Image image;
    private float fElapsedTime;
    public float fBlinkSpeed = 10;
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color = image.color;
        fElapsedTime += Time.deltaTime * 200;
        float falpha = (float) (Math.Cos(fElapsedTime / 180 * Math.PI));
        image.color = new Color (color.r, color.g, color.b,falpha);

    }
}
