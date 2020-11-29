using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public int playerType = 2;
    public GameObject[] playerObjects;
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject CC = GameObject.Find("CharacterChanger");
        if(CC)
        {
            playerType = CC.GetComponent<CharacterChanger>().playertype;
        }
        GameObject obj = Instantiate(playerObjects[playerType],GameObject.Find("Main Camera").GetComponent<Transform>());
        obj.name = "Player";

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
