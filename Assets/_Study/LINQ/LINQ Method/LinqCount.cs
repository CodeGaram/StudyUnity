using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinqCount : MonoBehaviour
{
    private void Start()
    {
        //Example1();
        //IfPropertyNotInit();
        //IfPropertyIsNull();
        //UsedWhere();
        //CustomClass();
        //UsedWhereCustomClass();
        UsedFuncCustomClass();
    }

    private void Example1()
    {
        List<int> intList = new List<int>()
        {
            1, 2, 3, 4, 5, 6
        };

        // Query
        var queryResult = (from num in intList
                           select num).Count();

        // Method
        var methodResult = intList.Count();

        CanvasLog.AddTextLine("Query: " + queryResult);
        CanvasLog.AddTextLine("Method: " + methodResult);
    }

    private void IfPropertyNotInit()
    {
        List<int> intList = new List<int>();

        // Query
        var queryResult = (from num in intList
                           select num).Count();

        // Method
        var methodResult = intList.Count();


        CanvasLog.AddTextLine("Query: " + queryResult);
        CanvasLog.AddTextLine("Method: " + methodResult);
    }

    private void IfPropertyIsNull()
    {
        List<int> intList = null;

        // Query
        var queryResult = (from num in intList
                           select num).Count();

        // Method
        var methodResult = intList.Count();


        CanvasLog.AddTextLine("Query: " + queryResult);
        CanvasLog.AddTextLine("Method: " + methodResult);
    }

    private void UsedWhere()
    {
        List<int> intList = new List<int>()
        {
            1, 2, 3, 4, 5, 6
        };

        // Query
        var queryResult = (from num in intList
                           where num > 4
                           select num).Count();

        // Method
        var methodResult = intList.Where(num => num > 4).Count();


        CanvasLog.AddTextLine("Query: " + queryResult);
        CanvasLog.AddTextLine("Method: " + methodResult);

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
        var queryResult = (from person in people
                           select person).Count();

        // Method
        var methodResult = people.Count();

        CanvasLog.AddTextLine("Query: " + queryResult);
        CanvasLog.AddTextLine("Method: " + methodResult);
    }

    private void UsedWhereCustomClass()
    {
        List<Person> people = new List<Person>
        {
            new Person{Name ="Bob",  Age = 20, Money = 30000},
            new Person{Name ="Nick", Age = 30, Money = 50000},
            new Person{Name ="Tom",  Age = 40, Money = 80000}
        };

        // Query
        var queryResult = (from person in people
                           where person.Money > 40000
                           select person).Count();

        // Method
        var methodResult = people.Where(person => person.Money > 40000).Count();


        CanvasLog.AddTextLine("Query: " + queryResult);
        CanvasLog.AddTextLine("Method: " + methodResult);
    }

    private void UsedFuncCustomClass()
    {
        List<Person> people = new List<Person>
        {
            new Person{Name ="Bob",  Age = 20, Money = 30000},
            new Person{Name ="Nick", Age = 30, Money = 50000},
            new Person{Name ="Tom",  Age = 40, Money = 80000}
        };

        // Query
        var queryResult = (from person in people
                           select person)
                           .Count(person => person.Money > 40000);

        // Method
        var methodResult = people.Count(person => person.Money > 40000);


        CanvasLog.AddTextLine("Query: " + queryResult);
        CanvasLog.AddTextLine("Method: " + methodResult);

    }
}
