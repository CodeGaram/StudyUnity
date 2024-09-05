using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Single() 메서드는 데이터 집합에서 유일한 단일 요소를 반환.
/// Func을 사용하여 특정 조건을 만족하는 단일 요소를 반환하는 오버로드 존재.
/// * 런타임 에러 발생 조건 *
/// - 데이터가 초기화되지 않음
/// - null
/// - 단일 요소가 없음
/// - 단일 요소가 두 개 이상 존재
/// - 조건을 만족하는 단일 요소가 없음
/// 
/// SingleOrDefault() 메서드는 데이터 집합이 빈 값이거나 조건을 만족하는 요소가 없는 경우 예외를 발생하지 않음
/// * 런타임 에러 발생 조건 *
/// - null
/// - 단일 요소가 두 개 이상 존재
/// - 조건을 만족하는 단일 요소가 없음
/// </summary>
public class LinqSingle : MonoBehaviour
{
    private void Start()
    {
        //Sample1();
        //UsedFuncSample();
        //SingleOrDefaultSample1();
        //SingleOrDefault_ListIsEmpty();
        SingleOrDefault_FuncIsFalse();
    }

    private void Sample1()
    {
        List<int> intList = new List<int>() { 100 };

        int getSingleElement = intList.Single();

        CanvasLog.AddTextLine($"단일 요소: {getSingleElement}");
    }

    private void UsedFuncSample()
    {
        List<int> intList = new List<int>() { 100, 200, 300 };

        int getSingleElement = intList.Single(num => num == 200);

        CanvasLog.AddTextLine($"단일 요소: {getSingleElement}");
    }

    private void SingleOrDefaultSample1()
    {
        List<int> intList = new List<int>() { 100 };

        int getSingleElement = intList.SingleOrDefault();

        CanvasLog.AddTextLine($"단일 요소: {getSingleElement}");
    }

    private void SingleOrDefault_ListIsEmpty()
    {
        List<int> intList = new List<int>();

        int getSingleElement = intList.SingleOrDefault();

        CanvasLog.AddTextLine($"단일 요소: {getSingleElement}");
    }

    private void SingleOrDefault_FuncIsFalse()
    {
        List<int> intList = new List<int>() { 10, 20, 30 };

        int getSingleElement = intList.SingleOrDefault(num => num > 50);

        CanvasLog.AddTextLine($"단일 요소: {getSingleElement}");
    }
}
