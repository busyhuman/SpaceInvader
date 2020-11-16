using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SignIn : MonoBehaviour
{
    public GameObject username;
    public GameObject password1;
    public GameObject password2;

    public void StartRegisterAccount()
    {
        StartCoroutine(RegisterAccount());
    }

    IEnumerator RegisterAccount()
    {
        GameObject phd = (GameObject)Instantiate(Resources.Load("HttpData/PostHttpData"));
        PostHttpData postHttpData = phd.GetComponent<PostHttpData>();
        WWWForm form = new WWWForm();
        
        form.AddField("username", username.GetComponent<InputField>().text);
        form.AddField("password1", password1.GetComponent<InputField>().text);
        form.AddField("password2", password2.GetComponent<InputField>().text);

        postHttpData.PostData("https://busyhuman.pythonanywhere.com/rest-auth/registration/", form);

        yield return null;
    }
}
