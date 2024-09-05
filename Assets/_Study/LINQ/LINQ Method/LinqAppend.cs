using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// .Net Framework 4.7.1 이상
/// 
/// Append()는 데이터 집합 마지막 위치에 값을 추가하는 용도로 사용.
/// 기존 데이터 집합을 수정하지 않으며, 값이 추가된 새로운 데이터 집합을 반환.
/// </summary>
public class LinqAppend : MonoBehaviour
{
    private void Start()
    {
        List<int> intList = new List<int>()
        {
            1, 2, 3
        };

        List<int> appendResult = intList.Append(4).ToList();

        CanvasLog.AddTextLine("intList의 값");
        intList.ForEach(num => CanvasLog.AddText(num + " "));

        CanvasLog.AddTextLine("\nappendResult의 값");
        appendResult.ForEach(num => CanvasLog.AddText(num + " "));
    }
}
