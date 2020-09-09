using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISFX : MonoBehaviour
{
    public AudioClip Button1;
    public AudioClip Button2;
    public AudioClip Button3;

    private AudioSource myAudio;
    // Start is called before the first frame update
    void Start()
    {
        myAudio = gameObject.GetComponent<AudioSource>();
        GameObject AC = GameObject.Find("AudioController");
        if(AC)
        {
            AC.GetComponent<AudioController>().SFXaudio = myAudio;
            myAudio.volume = AC.GetComponent<AudioController>().SFXVolume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton1()
    {
        myAudio.clip = Button1;
        myAudio.Play();
    }

    public void PlayButton2()
    {
        myAudio.clip = Button2;
        myAudio.Play();
    }
    public void playButton3()
    {
        myAudio.clip = Button3;
        myAudio.Play();
    }
    public void SetVoulume(int volume)
    {
        myAudio.volume = volume / 100.0f;
    }
}
