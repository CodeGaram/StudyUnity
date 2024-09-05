using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinqIEnumerableIQueryable : MonoBehaviour
{
    private void Start()
    {
        //StartToA_LengthOver3();
        Example_IQeuryable();
    }

    private void StartToA_LengthOver3()
    {
        string[] strArr = { "Apple", "Banana", "Car", "Angular", "Add", "Sum" };

        var linqResult = (from str in strArr
                          where str.StartsWith("A") && str.Length > 3
                          select str);

        foreach(var str in linqResult)
        {
            Debug.Log(str);
        }
    }

    // IEnumerable: 모든 데이터를 C# application에 가져와서 필터 -> 메모리에 데이터가 로드된 경우 유용
    // IQueryable: DataBase에서 필터 된 데이터를 가져옴 -> DataBase 서버가 존재하는 경우 사용하면 application의 작업 부담을 덜어줄 수 있음

    private void Example_IQeuryable()
    {
        string[] strArr = { "Apple", "Banana", "Car", "Angular", "Add", "Sum" };

        IQueryable<string> linqResult = (from str in strArr
                                         where str.StartsWith("A") && str.Length > 3
                                         select str).AsQueryable();

        foreach(var str in linqResult)
        {
            Debug.Log(str);
        }
    }
}
