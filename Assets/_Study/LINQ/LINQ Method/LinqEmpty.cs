using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


/// <summary>
/// 매개변수가 존재하지 않으며 TResult 타입(제네릭 타입)인 IEnumerable<TResult>를 반환
/// 
/// new 연산자를 사용하여 빈 배열 또는 컬렉션을 생성하는 경우 빈 객체가 메모리에 생성
/// -> 해당 객체를 사용하지 않는 시점 또는 애플리케이션이 종료되는 시점에 GC가 빈 객체를 메모리에서 수거
/// 
/// Empty() 메서드를 사용하면, 빈 데이터 집합이 메모리에 생성되지 않음
/// -> GC의 작업이 줄어듬
/// * TResult의 빈 시퀀스를 캐시, 반환하는 개체가 열거되면 요소가 생성되지 않음
/// ** 빈 시퀀스를 사용하는 사용자 정의 메서드를 전달하는 데 유용
/// *** Union 메서드에 대해 중립 요소를 생성하는 데 사용할 수 있음
/// 
/// </summary>
public class LinqEmpty : MonoBehaviour
{
    private void Start()
    {
        //Example1();
        Example2();
    }

    private void Example1()
    {
        List<string> strList = Enumerable.Empty<string>().ToList();
        var intList = Enumerable.Empty<int>();

        CanvasLog.AddTextLine("strList의 Count: " + strList.Count());
        CanvasLog.AddTextLine("intList의 Count: " + intList.Count());
    }

    private void Example2()
    {
        List<string> strList = null ?? Enumerable.Empty<string>().ToList(); // => null로 초기화 후 아래서 Count()를 호출하면 ArgumentNullException 발생
                                                                            // 때문에 Empty로 초기화 시킴

        CanvasLog.AddTextLine("strList의 Count: " + strList?.Count());
    }

}
