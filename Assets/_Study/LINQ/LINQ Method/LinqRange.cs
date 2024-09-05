using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// Range() 메서드는 지정된 범위의 정수를 생성
/// IEnumerable<int> 타입의 객체를 반환
/// 
/// 첫 번째 매개변수는 지정된 범위에서 시작하는 값을 설정
/// 두 번째 매개변수는 start부터 생성될 개수를 생성
/// </summary>
public class LinqRange : MonoBehaviour
{
    private void Start()
    {
        //Example1();
        Example2();
    }

    private void Example1()
    {
        List<int> intList = Enumerable.Range(1, 5).ToList<int>();

        foreach(int num in intList)
        {
            CanvasLog.AddTextLine(num);
        }
    }

    private void Example2()
    {
        List<int> intList = Enumerable.Range(1, 50)
            .Where(num => num % 10 == 0)
            .ToList<int>();

        foreach(int num in intList)
        {
            CanvasLog.AddTextLine(num);
        }
    }
}
