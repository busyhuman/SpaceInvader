using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private int CurrentStage;
    // Start is called before the nfofirst frame update
    void Start()
    {
        GameObject User = GameObject.Find("UserData");
        if (User != null)
        {
            CurrentStage = User.GetComponent<UserInfo>().GetStage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TurnToStage()
    {
        switch(CurrentStage)
        {
            case 1:
                TurnToStage1Normal();
                break;
            case 2:
                TurnToStage1Boss();
                break;
        }
    }
    
    public void TurnToStage1Normal()
    {

        GameObject audiosource = GameObject.Find("AudioController");
        if (audiosource)
            audiosource.GetComponent<AudioController>().SetBGM(3);
        SceneManager.LoadScene("StageNormal1");

    }
    public void TurnToStage1Boss()
    {

        GameObject audiosource = GameObject.Find("AudioController");
        if (audiosource)
            audiosource.GetComponent<AudioController>().SetBGM(1);
        SceneManager.LoadScene("StageBoss1");

    }
    public void TurnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        GameObject User = GameObject.Find("UserData");
        if (User != null)
        {
             User.GetComponent<UserInfo>().SetStage(1);
        }
    }
    public void CharacterSelect()
    {
        SceneManager.LoadScene("CharacterSelect");

    }
    public void TurnToLoading()
    {
        GameObject audiosource = GameObject.Find("AudioController");
        if(audiosource)
            audiosource.GetComponent<AudioController>().SetBGM(0);
        SceneManager.LoadScene("Loading");
    }
    public void TurnToRanking()
    {
        GameObject audiosource = GameObject.Find("AudioController");

        if (audiosource)
            audiosource.GetComponent<AudioController>().SetBGM(2);
        SceneManager.LoadScene("Ranking");
    }
}
