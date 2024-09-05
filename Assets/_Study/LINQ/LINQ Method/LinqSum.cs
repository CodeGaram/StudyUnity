using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinqSum : MonoBehaviour
{
    private void Start()
    {
        //IntegerTypeSum();
        //UsedWhereInteger();
        //UsedFuncInteger();
        //CustomClass();
        CustomClassWithFunc();
    }

    private void IntegerTypeSum()
    {
        List<int> intList = new List<int>()
        {
            5, 10, 15, 20, 25
        };

        int sum = intList.Sum();

        CanvasLog.AddText("Sum: " + sum);
    }
    
    private void UsedWhereInteger()
    {
        List<int> intList = new List<int>()
        {
            5, 10, 15, 20, 25
        };

        // Query
        int queryResult = (from num in intList
                           where num < 20
                           select num).Sum();

        // Method
        int methodResult = intList.Where(num => num < 20).Sum();


        CanvasLog.AddTextLine("Query: " + queryResult);
        CanvasLog.AddTextLine("Method: " + methodResult);

    }

    private void UsedFuncInteger()
    {
        List<int> intList = new List<int>()
        {
            5, 10, 15, 20, 25
        };

        // Query
        int queryResult = (from num in intList
                          select num)
                          .Sum(num =>
                          {
                              if (num < 20) return num;
                              else return 0;
                          });

        // Method
        int methodResult = intList.Sum(num =>
        {
            if (num < 20) return num;
            else return 0;
        });

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
        int queryResult = (from person in people
                           select person).Sum(person => person.Money);

        // Method
        int methodResult = people.Sum(person => person.Money);


        CanvasLog.AddTextLine("Query: " + queryResult);
        CanvasLog.AddTextLine("Method: " + methodResult);

    }

    private void CustomClassWithFunc()
    {
        List<Person> people = new List<Person>
        {
            new Person{Name ="Bob",  Age = 20, Money = 30000},
            new Person{Name ="Nick", Age = 30, Money = 50000},
            new Person{Name ="Tom",  Age = 40, Money = 80000}
        };

        // Query
        int queryResult = (from person in people
                           select person)
                           .Sum(person =>
                           {
                               if (person.Age > 20) return person.Money;
                               else return 0;
                           });

        // Method
        int methodResult = people.Sum(person =>
        {
            if (person.Age > 20) return person.Money;
            else return 0;
        });


        CanvasLog.AddTextLine("Query: " + queryResult);
        CanvasLog.AddTextLine("Method: " + methodResult);

    }
}
