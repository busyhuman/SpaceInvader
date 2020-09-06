using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    private float elapsedTIme = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTIme += Time.deltaTime;
        if (elapsedTIme > 2)
            Destroy(gameObject);
    }
}
