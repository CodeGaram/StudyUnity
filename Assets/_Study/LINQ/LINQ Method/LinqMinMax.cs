using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// query syntax 지원하지 않음
/// </summary>
public class LinqMinMax : MonoBehaviour
{
    private void Start()
    {
        //IntegerType();
        //StringType();
        //UsedWhereInteger();
        //UsedWhereInteger_Error();
        //UsedFuncMin();
        //UsedFuncMax();
        CustomClass();
    }

    private void IntegerType()
    {
        List<int> intList = new List<int>()
        {
            5, 10, 15, 20, 25
        };

        int minValue = intList.Min();
        int maxValue = intList.Max();

        CanvasLog.AddTextLine("minValue: " + minValue);
        CanvasLog.AddTextLine("maxValue: " + maxValue);
    }

    /// <summary>
    /// ASCII 값을 비교
    /// </summary>
    private void StringType()
    {
        List<string> stringList = new List<string>()
        {
            "AAAAAAAA", "B", "CD", "CDE", "B"
        };

        string minValue = stringList.Min();
        string maxValue = stringList.Max();

        CanvasLog.AddTextLine("minValue: " + minValue);
        CanvasLog.AddTextLine("maxValue: " + maxValue);
    }

    private void UsedWhereInteger()
    {
        List<int> intList = new List<int>
        {
            5, 10, 15, 20, 25, 30, 35, 40
        };

        // Query
        int queryResultMin = (from num in intList
                           where num > 20 && num < 40
                           select num).Min();

        int queryResultMax = (from num in intList
                              where num > 20 && num < 40
                              select num).Max();

        // Method
        int methodResultMin = intList.Where(num => num > 20 && num < 40).Min();
        int methodResultMax = intList.Where(num => num > 20 && num < 40).Max();


        CanvasLog.AddTextLine("queryResultMin: " + queryResultMin);
        CanvasLog.AddTextLine("queryResultMax: " + queryResultMax);
        CanvasLog.AddTextLine("methodResultMin: " + methodResultMin);
        CanvasLog.AddTextLine("methodResultMax: " + methodResultMax);
    }

    private void UsedWhereInteger_Error()
    {
        List<int> intList = new List<int>
        {
            5, 10, 15, 20, 25, 30, 35, 40
        };

        // Query
        int queryResultMin = (from num in intList
                              where num > 50
                              select num).Min();

        int queryResultMax = (from num in intList
                              where num > 50
                              select num).Max();

        // Method
        int methodResultMin = intList.Where(num => num > 50).Min();
        int methodResultMax = intList.Where(num => num > 50).Max();


        CanvasLog.AddTextLine("queryResultMin: " + queryResultMin);
        CanvasLog.AddTextLine("queryResultMax: " + queryResultMax);
        CanvasLog.AddTextLine("methodResultMin: " + methodResultMin);
        CanvasLog.AddTextLine("methodResultMax: " + methodResultMax);
    }

    private void UsedFuncMin()
    {
        List<int> intList = new List<int>
        {
            5, 10, 15, 20, 25, 30, 35, 40
        };

        // Query
        int queryResultMin = (from num in intList
                              where num > 20
                              select num)
                              .Min(num =>
                              {
                                  if (num > 20) return num;
                                  else return 0;
                              });

        // Method
        int methodResultMin = intList
            .Where(num => num > 20)
            .Min(num =>
            {
                if (num > 20) return num;
                else return 0;
            });


        CanvasLog.AddTextLine("queryResultMin: " + queryResultMin);
        CanvasLog.AddTextLine("methodResultMin: " + methodResultMin);
    }
    private void UsedFuncMax()
    {
        List<int> intList = new List<int>
        {
            5, 10, 15, 20, 25, 30, 35, 40
        };

        // Query
        int queryResultMin = (from num in intList
                              select num)
                              .Max(num =>
                              {
                                  if (num < 30) return num;
                                  else return 0;
                              });

        // Method
        int methodResultMin = intList.Max(num =>
        {
            if (num < 30) return num;
            else return 0;
        });


        CanvasLog.AddTextLine("queryResultMin: " + queryResultMin);
        CanvasLog.AddTextLine("methodResultMin: " + methodResultMin);
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Money { get; set; }
    }

    private void CustomClass()
    {
        List<Person> people = new List<Person>
        {
            new Person{Name ="Bob",  Age = 20, Money = 30000},
            new Person{Name ="Nick", Age = 30, Money = 50000},
            new Person{Name ="Tom",  Age = 40, Money = 80000}
        };

        // Query
        int queryResultMin = (from person in people
                              select person).Min(person => person.Money);
        int queryResultMax = (from person in people
                              select person.Money).Max();

        // Method
        int methodResultMin = people.Min(person => person.Money);
        int methodResultMax = people.Max(person => person.Money);


        CanvasLog.AddTextLine("queryResultMin: " + queryResultMin);
        CanvasLog.AddTextLine("queryResultMax: " + queryResultMax);
        CanvasLog.AddTextLine("methodResultMin: " + methodResultMin);
        CanvasLog.AddTextLine("methodResultMax: " + methodResultMax);
    }
}
