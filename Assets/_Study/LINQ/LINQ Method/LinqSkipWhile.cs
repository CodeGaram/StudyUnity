using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// SkipWhile() 메서드는 데이터 집합의 데이터를 처음 위치에서 순회.
/// 데이터 집합을 순회하는 동안 SkipWhile() 메서드에 전달된 조건문의 결과가 false인 경우 반복문을 중단하고 나머지 데이터를 반환.
/// 
/// Func 대리자의 매개변수가 다른 오버로드가 있음.
/// -> 데이터 집합의 요소 접근 / 데이터 집합의 요소와 인덱스를 접근.
/// </summary>
public class LinqSkipWhile : MonoBehaviour
{
    private void Start()
    {
        //Sample1_IntegerType();
        //NotSortedList();
        Sample_Overload();
    }


    private void Sample1_IntegerType()
    {
        List<int> intList = new List<int>()
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10
        };

        List<int> skipWhileResult = intList.SkipWhile(num => num < 5).ToList();

        foreach(int num in skipWhileResult)
        {
            CanvasLog.AddTextLine(num);
        }
    }

    private void NotSortedList()
    {
        List<int> intList = new List<int>()
        {
            1, 3, 4, 2, 5
        };

        List<int> skipWhileResult = intList.SkipWhile(num => num < 3).ToList();

        foreach(int num in skipWhileResult)
        {
            CanvasLog.AddTextLine(num);
        }
    }

    private void Sample_Overload()
    {
        List<string> strList = new List<string>()
        {
            "One", "Two", "Three", "Four", "Five"
        };

        List<string>skipWhileResult = strList
                                        .SkipWhile((strValue, index) => index < 3)
                                        .ToList();

        foreach(string value in skipWhileResult)
        {
            CanvasLog.AddTextLine(value);
        }
    }
}
