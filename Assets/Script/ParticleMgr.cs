using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMgr : MonoBehaviour
{
    private static ParticleMgr Instacne;
    public static ParticleMgr GetInstance()
    {
        if(!Instacne)
        {
            Instacne = new ParticleMgr();
        }
        return Instacne;
    }

    public GameObject square; // 2D Sprite 객체

    // Start is called before the first frame update
    void Start()
    {
        if (Instacne == null)
            Instacne = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CreateDestroyedParticles(GameObject obj, int ParticleNum ) // obj : 부딪힌 몬스터
    {
        Texture2D tex = obj.GetComponent<SpriteRenderer>().sprite.texture;
        Color[] pixs;
        pixs = tex.GetPixels();

        List<Color> newPix = new List<Color>();
        for (int i = 0; i < pixs.Length; )
        {
            if (pixs[i].a == 1)  
            {
                newPix.Add(pixs[i]);
                GameObject particle = Instantiate(square, new Vector3(obj.transform.position.x, obj.transform.position.y), Quaternion.identity );
                particle.GetComponent<SpriteRenderer>().color = pixs[i];
            }
            i += ParticleNum; // 개수 조절을 위해
        }
        Destroy(obj);
    }
}
