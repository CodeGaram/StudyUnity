using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class LinqGetIndex : MonoBehaviour
{
    private void Start()
    {
        //Example1();
        //QueryExample();
        MethodExample();
    }

    private void Example1()
    {
        List<string> list = new List<string>()
        {
            "Java", "C Sharp", "C++", "JavaScript", "React"
        };

        var methodResult = list.Select((item, index) => new
        {
            index,
            subject = item
        });

        foreach(var item in methodResult)
        {
            CanvasLog.AddText("index: " + item.index + " / Subject: " + item.subject);
        }
    }

    /// <summary>
    /// Query Syntax의 select절에는 인덱스를 지원하지 않음
    /// 따라서 Method Syntax의 Select()와 조합해야 함
    /// </summary>
    private void QueryExample()
    {
        List<string> list = new List<string>()
        {
            "Java", "C Sharp", "C++", "JavaScript", "React"
        };

        var queryResult = from subject in list.Select((item, index) => new { index, value = item })
                          select new
                          {
                              index = subject.index,
                              subject = subject.value
                          };

        foreach (var item in queryResult)
        {
            CanvasLog.AddText("index: " + item.index + " / Subject: " + item.subject);
        }
    }

    private void MethodExample()
    {
        List<string> list = new List<string>()
        {
            "Java", "C Sharp", "C++", "JavaScript", "React"
        };

        var methodResult = list
            .Select((item, index) => new
            {
                index,
                subject = item
            })
            .Where(item => item.subject.StartsWith("C"))
            .Select(item => new
            {
                index = item.index,
                subject = item.subject
            });

        foreach (var item in methodResult)
        {
            CanvasLog.AddText("index: " + item.index + " / Subject: " + item.subject);
        }
    }
}
