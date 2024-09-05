using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// Repeat() 메서드는 특정 데이터를 특정 횟수만큼 생성
/// 
/// 첫 번째 매개변수는 특정 데이터를 설정
/// 두 번째 매개변수는 특정 데이터의 생성 횟수를 설정
/// -> 매개변수가 0보다 작으면 ArfumentOutOfRangeException 발생
/// </summary>
public class LinqRepeat : MonoBehaviour
{
    private void Start()
    {
        Example1();
    }

    private void Example1()
    {
        List<string> strList = Enumerable.Repeat("Hello", 5).ToList();

        foreach(string str in strList)
        {
            CanvasLog.AddTextLine(str);
        }
    }
}
