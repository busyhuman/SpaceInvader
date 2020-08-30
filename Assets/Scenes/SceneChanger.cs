using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TurnToStage1Boss()
    {

        SceneManager.LoadScene("StageBoss1");

    }
    public void TurnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void CharacterSelect()
    {
        SceneManager.LoadScene("CharacterSelect");

    }
    public void TurnToLoading()
    {
        GameObject audiosource = GameObject.Find("Audio Source");
        if(audiosource)
        {
            Destroy(audiosource);
        }
        SceneManager.LoadScene("Loading");
    }
}
