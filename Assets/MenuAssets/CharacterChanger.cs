using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerType
{
    PLAYER_PANG,
    PLAYER_HARRY,
    PLAYER_BASE
}
public class CharacterChanger : MonoBehaviour
{
    public int playertype;
    public GameObject[] characterInfo;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPlayer()
    {
        if (playertype >= 2) return;

        characterInfo[playertype].SetActive(false);
        playertype++;

        characterInfo[playertype].SetActive(true);
    }

    public void PriorPlayer()
    {
        if (playertype <= 0) return;

        characterInfo[playertype].SetActive(false);
        playertype--;

        characterInfo[playertype].SetActive(true);
    }
}
