using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamLogo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time < 2.0f)
        {
            Vector3 sc = transform.localScale;
            transform.localScale = sc + new Vector3(0.1f,0.1f,0.1f) * Time.deltaTime;
        }
        else
            SceneManager.LoadScene("Logo");
    }
}
