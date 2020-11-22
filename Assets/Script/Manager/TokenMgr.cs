using UnityEngine;

public class TokenMgr : MonoBehaviour
{
    private static TokenMgr instance = null;
    public string Token;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static TokenMgr Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }

    }

    public void SetToken(string str)
    {
        Token = str;
    }

    public string GetToken()
    {
        return Token;
    }
}