using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// Skip() 메서드에 int타입의 값 n을 전달하면 n번째 위치부터 시작하는 모든 값이 반환.
/// 
/// 데이터 집합이 null로 할당된 경우 ArgumentNullException 발생
/// </summary>
public class LinqSkip : MonoBehaviour
{
    private void Start()
    {
        //Sample1_IntegerType();
        //SkipAfterFiltering();
        //CountOver();
        DifferentSkipToTake();
    }


    private void Sample1_IntegerType()
    {
        List<int> intList = new List<int>()
        {
            50, 10, 40, 20, 30
        };

        List<int> skipResult = intList.Skip(3).ToList(); // 3개를 스킵 -> 20, 30 반환

        foreach(int num in skipResult)
        {
            CanvasLog.AddTextLine(num);
        }
    }

    private void SkipAfterFiltering()
    {
        List<int> intList = new List<int>()
        {
            10, 20, 30, 40, 50, 60, 70, 80
        };

        List<int> skipResult = intList.Where(num => num > 30).Skip(3).ToList();

        foreach (int num in skipResult)
        {
            CanvasLog.AddTextLine(num);
        }
    }

    private void CountOver()
    {
        List<int> intList = new List<int>()
        {
            50, 10, 40, 20, 30
        };

        List<int> skipResult = intList.Skip(10).ToList();

        CanvasLog.AddTextLine(skipResult.Count); // 0 반환

    }

    private void DifferentSkipToTake()
    {
        List<int> intList = new List<int>()
        {
            50, 10, 40, 20, 30
        };

        List<int> skipResult = intList.Skip(3).ToList();    // 3개 건너뛰고 나머지 데이터를 반환

        CanvasLog.AddTextLine("Skip() 메서드 결과");
        foreach(int num in skipResult)
        {
            CanvasLog.AddText(num + " ");                   // 20, 30
        }

        List<int> takeResult = intList.Take(3).ToList();    // 처음부터 3개를 반환

        CanvasLog.AddTextLine("\nTake() 메서드 결과");
        foreach(int num in takeResult)
        {
            CanvasLog.AddText(num + " ");                   // 50, 10, 40
        }
    }
}
