using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    private AudioSource aSfx;
    public AudioClip[] audioClips;
    // Start is called before the first frame update
    private void Awake()
    {
        aSfx = GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMonsterDamaged()
    {
        aSfx.clip = audioClips[0];
        aSfx.Play();
    }
}
