using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// ElementAt() 메서드는 데이터 집합에서 특정 인덱스의 값을 반환
/// ElementAt() 메서드를 호출하는 데이터 집합이 null로 초기화되거나 ElementAt() 메서드의 인자로 전달된 인덱스 값이 데이터 집합의 범위를 벗어나면 런타임 에러가 발생
/// 
/// ElementAtOrDefault() 메서드는 ElementAt() 메서드와 동일한 작업을 수행하지만,
/// 데이터 집합이 빈 값이거나 ElementAtOrDefault() 메서드의 인자로 전달된 값이 데이터 집합의 범위를 벗어나더라도 예외가 발생하지 않음
/// 단, null은 예외 발생
/// => 호출한 데이터 집합 타입의 Default값을 반환
/// </summary>
public class LinqElementAt : MonoBehaviour
{
    private void Start()
    {
        ElementAtSample1();
    }


    private void ElementAtSample1()
    {
        List<int> intList = new List<int>()
        {
            10, 20, 30, 40, 50
        };

        int getElement = intList.ElementAt(3);

        CanvasLog.AddTextLine($"3번째 인덱스의 값: {getElement}");
    }


}
