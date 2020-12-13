using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SignInMgr : MonoBehaviour
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

        yield return StartCoroutine(WaitMessage(postHttpData, 5.0f));
        
        if (postHttpData.getErrorMessage() == postHttpData.DefaultErrorMessage && postHttpData.getMessage() != postHttpData.DefaultMessage)
        {
            yield return StartCoroutine(RegisterUserList());
        }

        yield return null;
    }

    IEnumerator WaitMessage(PostHttpData postHttpData, float time)
    {
        float WAITING_TIME = time;
        float CURRENT_TIME = 0.0f;
        float CHECK_CYCLE_TIME = 0.5f;

        string defaultMessage = postHttpData.DefaultMessage;

        // time시간동안 메시지가 오는 지 체크
        while (CURRENT_TIME < WAITING_TIME)
        {
            yield return new WaitForSeconds(CHECK_CYCLE_TIME);
            CURRENT_TIME += CHECK_CYCLE_TIME;

            if (postHttpData.getMessage() != defaultMessage)
            {
                break;
            }
        }

        yield return null;
    }

    IEnumerator RegisterUserList()
    {
        GameObject phd = (GameObject)Instantiate(Resources.Load("HttpData/PostHttpData"));
        PostHttpData postHttpData = phd.GetComponent<PostHttpData>();
        WWWForm form = new WWWForm();

        form.AddField("ID", username.GetComponent<InputField>().text);

        postHttpData.PostData("https://busyhuman.pythonanywhere.com/users/", form);

        yield return null;
    }
}
