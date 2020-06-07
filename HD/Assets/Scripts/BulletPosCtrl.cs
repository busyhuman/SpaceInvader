using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPosCtrl : MonoBehaviour
{
    public GameObject bullet;
    public float bulletIteration = 0.4f;

    void Start()
    {
        StartCoroutine(GenerateBullet());
    }

    IEnumerator GenerateBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(bulletIteration);
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }
}
