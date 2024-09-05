using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinqOfType : MonoBehaviour
{
    private void Start()
    {
        //Example1();
        //UsedIsInQuery();
        QueryAndMethod();
    }

    private void Example1()
    {
        List<object> allTypeList = new List<object>()
        {
            "C Sharp", 100, true, "Java", 200, false, "React"
        };

        List<string> strList = allTypeList.OfType<string>().ToList();

        foreach(string str in strList)
        {
            CanvasLog.AddText(str);
        }
    }

    ///
    /// OfType() 메서드는 System.Linq에서 지원하지만 Query Syntax에서 사용할 수 없음.
    /// 대신 is 연산자를 사용하여 특정 타입에 해당하는 값을 추출할 수 있음.
    ///
    private void UsedIsInQuery()
    {
        List<object> allTypeList = new List<object>()
        {
            "C Sharp", 100, true, "Java", 200, false, "React"
        };

        // Query
        var queryResult = (from item in  allTypeList
                           where item is string
                           select item).ToList();

        // Method
        var methodResult = allTypeList.OfType<string>();


        CanvasLog.AddTextLine("Query");
        foreach (var value in queryResult)
        {
            CanvasLog.AddText(value);
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (var value in methodResult)
        {
            CanvasLog.AddText(value);
        }
    }

    private void QueryAndMethod()
    {
        List<object> allTypeList = new List<object>()
        {
            "C Sharp", 100, true, "Java", 200, false, "React"
        };


        // Query
        var queryResult = (from item in allTypeList.OfType<int>()
                           where item == 100
                           select item).ToList();

        // Method
        var methodResult = allTypeList.OfType<int>()
            .Where(item => item == 100)
            .Select(item => item);


        CanvasLog.AddTextLine("Query");
        foreach (var value in queryResult)
        {
            CanvasLog.AddText(value);
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (var value in methodResult)
        {
            CanvasLog.AddText(value);
        }
    }
}
