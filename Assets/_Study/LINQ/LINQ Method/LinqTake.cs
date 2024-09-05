using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 데이터 집합에서 특정 개수만큼 데이터를 가져오는 메서드.
/// 데이터 집합의 처음 위치에서 n개의 데이터를 가져오며, 특정 개수는 Take() 메서드의 매개변수로 전달
/// 
/// 만약, Take() 메서드를 호출하는 데이터 집합이 null인 경우 ArgumentNullException 발생.
/// </summary>
public class LinqTake : MonoBehaviour
{
    private void Start()
    {
        //Example1_IntegerType();
        //Example_QuerySyntax();
        TakeAfterFiltering();
    }

    private void Example1_IntegerType()
    {
        List<int> intList = new List<int>()
        {
            50, 10, 40, 20, 30
        };

        List<int> takeResult = intList.Take(3).ToList();

        foreach(int num in takeResult)
        {
            CanvasLog.AddTextLine(num);
        }
    }

    private void Example_QuerySyntax()
    {
        List<int> intList = new List<int>()
        {
            50, 10, 40, 20, 30
        };

        List<int> takeResult = (from num in intList
                                select num).Take(3).ToList();

        foreach (int num in takeResult)
        {
            CanvasLog.AddTextLine(num);
        }
    }

    private void TakeAfterFiltering()
    {
        List<int> intList = new List<int>()
        {
            10, 20, 30, 40, 50, 60, 70, 80
        };

        List<int> takeResult = intList.Where(num => num > 30).Take(3).ToList();

        foreach (int num in takeResult)
        {
            CanvasLog.AddTextLine(num);
        }
    }
}
