using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JsonParser<T>
{
    public static T[] ParseJsonData(string jsonString)
    {
        return JsonHelper.FromJson<T>(jsonString);
    }
}
