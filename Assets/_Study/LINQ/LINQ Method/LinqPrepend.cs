using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// .Net Framework 4.7.1 이상
/// 
/// Prepend()는 데이터 집합 시작 위치에 값을 추가하는 용도로 사용.
/// 기존 데이터 집합을 수정하지 않으며, 값이 추가된 새로운 데이터 집합을 반환.
/// </summary>
public class LinqPrepend : MonoBehaviour
{
    private void Start()
    {
        List<int> intList = new List<int>()
        {
            1, 2, 3
        };

        List<int> prependResult = intList.Prepend(0).ToList();

        CanvasLog.AddTextLine("intList의 값");
        intList.ForEach(num => CanvasLog.AddText(num + " "));

        CanvasLog.AddTextLine("\nprependResult 값");
        prependResult.ForEach(num => CanvasLog.AddText(num + " "));
    }
}
