using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSound : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip Laugh;
    public AudioClip Hurt;
    public AudioClip skill1;
    public AudioClip skill2;
    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        GameObject sfxvolume = GameObject.Find("AudioController");
        if (sfxvolume)
            audio.volume = sfxvolume.GetComponent<AudioController>().SFXVolume / 100.0f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLaugh()
    {
        audio.clip = Laugh;
        audio.Play();
    }
    
    public void PlaySkill1()
    {
        audio.clip = skill1;
        audio.Play();
    }
    public void PlaySkill2()
    {
        audio.clip = skill2;
        audio.Play();
    }
    public void PlayHurt()
    {
        audio.clip = Hurt;
        audio.Play();
    }
}
