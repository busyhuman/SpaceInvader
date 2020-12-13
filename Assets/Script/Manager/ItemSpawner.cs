using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemSpawner : MonoBehaviour
{
    private float CurElapsedTime = 0;
    public float ItemTurm = 5;
    public GameObject[] Itemtype;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void SpawnItem()
    {
      if(!player)
            player = GameObject.Find("player");

        Vector3 playerpos = player.transform.position;
        int types = Itemtype.Length;
        int randnum = Random.Range(0, types);
        GameObject obj = Instantiate(Itemtype[randnum]);
        obj.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-4, 4), 0);

    }
    // Update is called once per frame
    void Update()
    {
        CurElapsedTime += Time.deltaTime;
        if(CurElapsedTime> ItemTurm)
        {
            CurElapsedTime = 0;
            SpawnItem();
        }
    
    }
}
