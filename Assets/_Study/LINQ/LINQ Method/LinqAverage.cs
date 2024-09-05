using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinqAverage : MonoBehaviour
{
    private void Start()
    {
        //IntegerType();
        //UsedWhere();
        //CustomClass();
        UsedWhereCustomClass();
    }

    private void IntegerType()
    {
        List<int> intList = new List<int>()
        {
          1, 2, 3, 4, 5, 6
        };

        // Query
        var queryResult = (from num in intList
                           select num)
                           .Average();

        // Method
        var methodResult = intList.Average();


        CanvasLog.AddTextLine("Query: " + queryResult);
        CanvasLog.AddTextLine("Method: " + methodResult);
    }

    private void UsedWhere()
    {
        List<int> intList = new List<int>()
        {
          5, 7, 10, 12, 15
        };

        // Query
        var queryResult = (from num in intList
                           where num > 10
                           select num)
                           .Average();

        // Method
        var methodResult = intList.Where(num => num > 10).Average();


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
                           select person).Average(person => person.Money);

        // Method
        var methodResult = people.Average(person => person.Money);

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
                           where person.Age > 15
                           select person)
                           .Average(person => person.Money);

        // Method
        var methodResult = people.Where(person => person.Age > 15).Average(person => person.Money);

        CanvasLog.AddTextLine("Query: " + queryResult);
        CanvasLog.AddTextLine("Method: " + methodResult);
    }
}
