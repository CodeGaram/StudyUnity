using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Last() 메서드는 데이터 집합에서 마지막 요소를 반환.
/// Func 대리자를 인자로 조건을 정의하는 오버로드가 존재.
/// 데이터 집합이 null 또는 빈 값으로 할당되거나 Last() 메서드의 인자로 전달된 람다식에서 조건을 만족하는 요소가 없는 경우 런타임 에러 발생.
/// 
/// LastOrDafault() 메서드는 데이터 집합이 빈 값이거나 람다식에서 조건을 만족하는 요소가 없더라도 예외가 발생하지 않음.
/// -> 메서드를 호출한 데이터 집합 타입의 Default값을 반환
/// 단, null은 예외
/// </summary>
public class LinqLast : MonoBehaviour
{
    private void Start()
    {
        //FirstSample1();
        //UsedFuncSample1();
        //FirstOrDefaultSample1();
        //UsedFuncFirstOrDefaultSample1();
        IsNotLastElement_LastOrDefaultSample1();
    }

    private void LastSample1()
    {
        List<int> intList = new List<int>()
        {
            10, 20, 30, 40, 50
        };

        int getLastElement = intList.Last();

        CanvasLog.AddTextLine($"마지막 요소: {getLastElement}");
    }

    private void UsedFuncSample1()
    {
        List<int> intList = new List<int>()
        {
            10, 20, 30, 40, 50
        };

        int getLastElement = intList.Last(num => num % 20 == 0);

        CanvasLog.AddTextLine($"마지막 요소: {getLastElement}");
    }

    private void LastOrDefaultSample1()
    {
        List<int> intList = new List<int>()
        {
            10, 20, 30, 40, 50
        };

        int getLastElement = intList.LastOrDefault();

        CanvasLog.AddTextLine($"마지막 요소: {getLastElement}");
    }

    private void UsedFuncLastOrDefaultSample1()
    {
        List<int> intList = new List<int>()
        {
            10, 20, 30, 40, 50
        };

        int getLastElement = intList.LastOrDefault(num => num % 20 == 0);

        CanvasLog.AddTextLine($"마지막 요소: {getLastElement}");
    }

    private void IsNotLastElement_LastOrDefaultSample1()
    {
        List<int> intList = new List<int>()
        {
            10, 20, 30, 40, 50
        };

        int getLastElement = intList.LastOrDefault(num => num % 100 == 0);

        CanvasLog.AddTextLine($"마지막 요소: {getLastElement}");
    }



}
