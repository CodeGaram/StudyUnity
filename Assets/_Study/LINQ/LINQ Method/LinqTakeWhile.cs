using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// TakeWhile() 메서드는 데이터 집합의 데이터를 처음 위치에서 순회.
/// 데이터 집합을 순회하는 동안 TakeWhile() 메서드에 전달된 조건문의 결과가 false인 경우 반복문을 중단하고 반복문이 실행되는 동안 조건을 충족했던 데이터를 반환.
/// 데이터 집합의 인덱스를 접근할 수 있는 Func 오버로드 버전이 존재.
/// </summary>
public class LinqTakeWhile : MonoBehaviour
{
    private void Start()
    {
        //Example1_IntegerType();
        //DiffWhereToTakeWhile();
        UsedIndexFunc();
    }

    
    private void Example1_IntegerType()
    {
        List<int> intList = new List<int>()
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10
        };

        List<int> takeWhileResult = intList.TakeWhile(num => num < 5).ToList();

        foreach(int num in takeWhileResult)
        {
            CanvasLog.AddTextLine(num);
        }

    }

    /// <summary>
    /// TakeWhile() 메서드는 조건문이 false인 경우 반복문이 중단 -> 데이터 집합을 순회하는 동안 조건문을 충족했던 데이터 반환
    /// Where절은 조건을 만족하는 모든 데이터를 반환
    /// </summary>
    private void DiffWhereToTakeWhile()
    {
        List<int> intList = new List<int>()
        {
            1, 2, 3, 4, 5, 1, 2, 3, 4, 5
        };

        List<int> takeWhileResult = intList.TakeWhile(num => num < 5).ToList();

        CanvasLog.AddTextLine("TakeWhile() 메서드 결과");
        foreach(int num in takeWhileResult)
        {
            CanvasLog.AddText(num + " ");
        }

        List<int> whereResult = intList.Where(num => num < 5).ToList();

        CanvasLog.AddTextLine("\nWhere절 결과");
        foreach(int num in whereResult)
        {
            CanvasLog.AddText(num + " ");
        }

    }


    private void UsedIndexFunc()
    {
        List<string> strList = new List<string>()
        {
            "One", "Two", "Three", "Four", "Five"
        };

        List<string> takeWhileResult = strList
                                        .TakeWhile((strValue, index) => index < 3)
                                        .ToList();

        foreach(string str in takeWhileResult)
        {
            CanvasLog.AddTextLine(str);
        }
    }
}
