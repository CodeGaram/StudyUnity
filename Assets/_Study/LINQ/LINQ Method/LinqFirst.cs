using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// First() 메서드는 데이터 집합에서 첫 번째 요소를 반환
/// 데이터 집합에서 특정 조건을 가지는 첫 번째 요소를 반환하는 오버로드 된 메서드 존재
/// First() 메서드를 호출하는 데이터 집합이 null 도는 빈 값으로 할당되거나 람다식에서 조건을 만족하는 요소가 없는 경우 런타임 에러
/// 
/// FirstOrDefault() 메서드는 First() 메서드와 다르게 데이터 집합이 빈 값이거나 인자로 전달된 람다식에서 조건을 만족하는 요소가 없더라도 예외가 발생하지 않음
/// => 호출한 데이터 집합 타입의 Default값을 반환
/// 데이터 집합에서 특정 조건을 가지는 첫 번째 요소를 반환하는 오버로드 된 메서드 존재
/// 단, null 예외 발생
/// </summary>
public class LinqFirst : MonoBehaviour
{
    private void Start()
    {
        //FirstSample1();
        //UsedFuncSample1();
        //FirstOrDefaultSample1();
        //UsedFuncFirstOrDefaultSample1();
        IsNotFirstElement_FirstOrDefaultSample1();
    }

    private void FirstSample1()
    {
        List<int> intList = new List<int>()
        {
            10, 20, 30, 40, 50
        };

        int getFirstElement = intList.First();

        CanvasLog.AddTextLine($"첫 번째 요소: {getFirstElement}");
    }

    private void UsedFuncSample1()
    {
        List<int> intList = new List<int>()
        {
            10, 20, 30, 40, 50
        };

        int getFirstElement = intList.First(num => num % 20 == 0);

        CanvasLog.AddTextLine($"첫 번째 요소: {getFirstElement}");
    }

    private void FirstOrDefaultSample1()
    {
        List<int> intList = new List<int>()
        {
            10, 20, 30, 40, 50
        };

        int getFirstElement = intList.FirstOrDefault();

        CanvasLog.AddTextLine($"첫 번째 요소: {getFirstElement}");
    }

    private void UsedFuncFirstOrDefaultSample1()
    {
        List<int> intList = new List<int>()
        {
            10, 20, 30, 40, 50
        };

        int getFirstElement = intList.FirstOrDefault(num => num % 20 == 0);

        CanvasLog.AddTextLine($"첫 번째 요소: {getFirstElement}");
    }

    private void IsNotFirstElement_FirstOrDefaultSample1()
    {
        List<int> intList = new List<int>()
        {
            10, 20, 30, 40, 50
        };

        int getFirstElement = intList.FirstOrDefault(num => num % 100 == 0);

        CanvasLog.AddTextLine($"첫 번째 요소: {getFirstElement}");
    }
}
