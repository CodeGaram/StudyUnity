using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// LINQ의 Reverse()는 원본 데이터를 변경하지 않고 반전된 데이터를 반환.
/// Reverse() 메서드는 System.Linq 및 System.Collections.Generic 네임스페이스에 존재
/// .Net Framework에서 제공하는 Reverse()는 두 개이며, 이 둘은 네임스페이스가 다르므로 서로 다른 메서드
/// System.Linq에 존재하는 Reverse()는 Enumerable 클래스에서 구현되며 IEnumerable<TSource>를 반환
/// System.Collections.Generic에 존재하는 Reverse()는 void를 반환
/// </summary>
public class LinqReverse : MonoBehaviour
{
    private void Start()
    {
        //SystemLinqReverse();
        //SystemCollectionsGenericReverse();
        ApplySystemLinqReverseToList();
    }

    /// <summary>
    /// List 객체에서 Reverse()를 호출하면 System.Collections.Generic 네임스페이스의 Reverse()가 호출되기 때문에
    /// System.Linq의 Reverse()를 호출하기 위해서는 제네릭 타입을 명시해야 함.
    /// </summary>
    private void SystemLinqReverse()
    {
        List<int> intList = new List<int>()
        {
            5, 10, 15, 20, 25, 30
        };

        // Query
        List<int> queryResult = (from num in intList
                                 select num).Reverse().ToList();

        // Method
        List<int> methodResult = intList.Reverse<int>().ToList();


        CanvasLog.AddTextLine("Query + Method");
        foreach (int num in queryResult)
        {
            CanvasLog.AddTextSpace(num);
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (int num in methodResult)
        {
            CanvasLog.AddTextSpace(num);
        }
    }

    private void SystemCollectionsGenericReverse()
    {
        List<int> intList = new List<int>()
        {
            5, 10, 15, 20, 25, 30
        };

        intList.Reverse();

        CanvasLog.AddTextLine("original data");
        foreach (int num in intList)
        {
            CanvasLog.AddTextSpace(num);
        }
    }

    private void ApplySystemLinqReverseToList()
    {
        List<int> intList = new List<int>()
        {
            5, 10, 15, 20, 25, 30
        };

        // Call AsQueryable() Method
        List<int> queryResult = intList.AsQueryable().Reverse().ToList();

        // Call AsEnumerable() Method
        List<int> enumerableResult = intList.AsEnumerable().Reverse().ToList();



        CanvasLog.AddTextLine("AsQueryable");
        foreach (int num in queryResult)
        {
            CanvasLog.AddTextSpace(num);
        }

        CanvasLog.AddTextLine("\nAsEnumerable");
        foreach (int num in enumerableResult)
        {
            CanvasLog.AddTextSpace(num);
        }
    }
}
