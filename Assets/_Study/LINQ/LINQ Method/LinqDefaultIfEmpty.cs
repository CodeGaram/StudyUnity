using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// DefaultIfEmpty() 메서드를 호출하는 시퀀스 또는 데이터 집합이 비어 있지 않으면 시퀀스 또는 데이터 집합이 가지고 있는 데이터가 반환.
/// 데이터가 존재하지 않으면 해당 타입의 Default값이 반환
/// Default값을 설정할 수 있는 오버로드 존재
/// </summary>
public class LinqDefaultIfEmpty : MonoBehaviour
{
    private void Start()
    {
        //DataIsNotEmpty();
        //DataIsEmpty();
        //DetaIsEmpty_BoolType();
        //SetDefaultValue();
        SetDefaultValue_BoolType();
    }


    private void DataIsNotEmpty()
    {
        List<int> intList = new List<int>() { 10, 20, 30 };

        IEnumerable<int> result = intList.DefaultIfEmpty();

        foreach(int num in result)
        {
            CanvasLog.AddTextLine(num);
        }
    }

    private void DataIsEmpty()
    {
        List<int> intList = new List<int>() { };

        IEnumerable<int> result = intList.DefaultIfEmpty();

        foreach(int num in result)
        {
            CanvasLog.AddTextLine(num);
        }
    }

    private void DetaIsEmpty_BoolType()
    {
        List<bool> boolList = new List<bool>();

        IEnumerable<bool> result = boolList.DefaultIfEmpty();

        foreach(bool item in result)
        {
            CanvasLog.AddTextLine(item);
        }
    }

    private void SetDefaultValue()
    {
        List<int> numList = new List<int>();

        IEnumerable<int> result = numList.DefaultIfEmpty(10);

        foreach(int num in result)
        {
            CanvasLog.AddTextLine(num);
        }
    }

    private void SetDefaultValue_BoolType()
    {
        List<bool> boolList = new List<bool>();

        IEnumerable<bool> result = boolList.DefaultIfEmpty(true);

        foreach (bool item in result)
        {
            CanvasLog.AddTextLine(item);
        }
    }
}
