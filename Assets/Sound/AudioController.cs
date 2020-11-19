using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip LoadingBGM;
    public AudioClip Stage1BossBGM;
    public AudioClip Stage1NormalBGM;
    public AudioClip RankingBGM;

    public AudioSource BGMaudio;
    public AudioSource SFXaudio;

    public int BGMVolume = 100;
    public int SFXVolume = 100;

    private void Awake()
    {
        BGMaudio = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetBGM(int index)
    {
        switch (index)
        {
            case 0:
                BGMaudio.clip = LoadingBGM;
                BGMaudio.Play();
                break;
            case 1:
                BGMaudio.clip = Stage1BossBGM;
                BGMaudio.Play();
                break;
            case 2:
                BGMaudio.clip = RankingBGM;
                BGMaudio.Play();
                break;
            case 3:
                BGMaudio.clip = Stage1NormalBGM;
                BGMaudio.Play();
                break;

        }
    }
    public void SetBGMVolume(int volume)
    {
        Debug.Log("hey");
        BGMaudio.volume = volume / 100.0f;
    }

    public void SetSFXVolume(int volume)
    {
        SFXVolume = volume;
        SFXaudio.volume = volume / 100.0f;
    }

}
