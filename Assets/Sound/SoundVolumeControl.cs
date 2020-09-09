using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundVolumeControl : MonoBehaviour
{
    public UISFX SFXSource;
    private int SFXVolume = 100;
    private int BGMVolume = 100;

    public int OriginBGMVolume ;
    public int OriginSFXVolume ;

    public Sprite[] images = new Sprite[11];
    private AudioController soundmgr;
    private Image barimage;
    // Start is called before the first frame update
    void Start()
    {

        OriginBGMVolume = BGMVolume;
        OriginSFXVolume = SFXVolume;
       barimage = gameObject.GetComponent<Image>();
        GameObject smgr = GameObject.Find("AudioController");
        if(smgr)
               soundmgr = smgr.GetComponent<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LowerSFXVolume()
    {
        if (SFXVolume > 0)
            SFXVolume -= 10;
        if(soundmgr)
         soundmgr.SetSFXVolume(SFXVolume);
        SetSFXbar();

    }
    public void HigherSFXVolume()
    {
        if (SFXVolume < 100)
            SFXVolume += 10;
        if (soundmgr)
            soundmgr.SetSFXVolume(SFXVolume);
        SetSFXbar();
       
    }
    public void LowerBGMVolume()
    {
        if (BGMVolume > 0)
            BGMVolume -= 10;
        if (soundmgr)
            soundmgr.SetBGMVolume(BGMVolume);
        SetBGMbar();

    }
    public void HigherBGMVolume()
    {
        if (BGMVolume < 100)
            BGMVolume += 10;
        if (soundmgr)
            soundmgr.SetBGMVolume(BGMVolume);
        SetBGMbar();
    }
    public void SetBGMbar()
    {
        barimage.sprite = images[BGMVolume / 10];
    }
    public void SetSFXbar()
    {
        barimage.sprite = images[SFXVolume / 10];
    }

    public void CancelBGMVolume()
    {
        BGMVolume = OriginBGMVolume;
        if(soundmgr)
            soundmgr.SetBGMVolume(BGMVolume);
        SetBGMbar();
    }
    public void CancelSFXVolume()
    {
        SFXVolume = OriginSFXVolume;
        SetSFXbar();
    }
    public void ApplyAudio()
    {
        OriginSFXVolume = SFXVolume;
        OriginBGMVolume = BGMVolume;
    }
}
