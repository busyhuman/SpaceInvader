using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public static class EngToKor
{
    public static string Translate(string str)
    {
        string[] ENG = 
        {
            "{\"password\":[\"This field may not be blank.\"]}",
            "{\"non_field_errors\":[\"Unable to log in with provided credentials.\"]}",
            "{\"username\":[\"This field may not be blank.\"],\"password1\":[\"This field may not be blank.\"],\"password2\":[\"This field may not be blank.\"]}",
            "{\"password1\":[\"This field may not be blank.\"],\"password2\":[\"This field may not be blank.\"]}",
            "{\"password1\":[\"This password is too short. It must contain at least 8 characters.\"]}",
            "{\"username\":[\"This field may not be blank.\"],\"password1\":[\"This password is too short. It must contain at least 8 characters.\"]}",
            "{\"non_field_errors\":[\"Must include \\\"username\\\" and \\\"password\\\".\"]}",
            "{\"username\":[\"A user with that username already exists.\"],\"password1\":[\"This field may not be blank.\"],\"password2\":[\"This field may not be blank.\"]}",
            "{\"username\":[\"This field may not be blank.\"]}",
            "{\"username\":[\"A user with that username already exists.\"],\"password1\":[\"This password is too short. It must contain at least 8 characters.\"]}",
            "{\"username\":[\"A user with that username already exists.\"]}",
            "{\"ID\":[\"user with this ID already exists.\"]}",
            "{\"username\":[\"Enter a valid username. This value may contain only letters, numbers, and @/./+/-/_ characters.\"],\"password1\":[\"This field may not be blank.\"],\"password2\":[\"This field may not be blank.\"]}",

        };

        string[] KOR =
        {
            "패스워드를 입력해주세요.",
            "입력한 정보로 로그인을 할 수 없습니다.",
            "아이디와 패스워드를 입력하세요.",
            "비밀번호를 입력하세요.",
            "비밀번호를 8글자 이상 입력하세요.",
            "아이디를 입력하시고 비밀번호를 8글자 이상 입력하세요.",
            "아이디와 패스워드를 입력해주세요.",
            "이미 존재하는 아이디입니다.",
            "아이디를 입력해주세요.",
            "이미 존재하는 아이디입니다",
            "이미 존재하는 아이디입니다",
            "이미 존재하는 아이디입니다",
            "아이디에 특수 문자는 사용할 수 없습니다",
        };

        for (int i= 0;i<ENG.Length;i++)
        {
            if( str.Equals(ENG[i]))
            {
                return KOR[i];
            }
        }

        return str;

    }

}
