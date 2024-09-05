using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

// Concat은 query syntax에서 지원하지 않음
public class LinqConcat : MonoBehaviour
{
    private void Start()
    {
        //IntegerType();
        //StringType();
        CustomClass();
    }

    private void IntegerType()
    {
        List<int> intArrayA = new List<int>() { 0, 2, 4, 5 };
        List<int> intArrayB = new List<int>() { 1, 2, 3, 5 };

        // Query + Method
        List<int> queryResult = (from num in intArrayA
                                 select num)
                                 .Concat(intArrayB)
                                 .ToList();

        // Method
        List<int> methodResult = intArrayA.Concat(intArrayB).ToList();


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

    private void StringType()
    {
        List<string> stringArrayA = new List<string>() { "one", "two", "three" };
        List<string> stringArrayB = new List<string>() { "One", "Two", "Three" };

        // Query
        List<string> queryResult = (from str in  stringArrayA
                                    select str)
                                    .Concat(stringArrayB) 
                                    .ToList();

        // Method
        List<string> methodResult = stringArrayA.Concat(stringArrayB).ToList();


        CanvasLog.AddTextLine("Query + Method");
        foreach (string str in queryResult)
        {
            CanvasLog.AddTextSpace(str);
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (string str in methodResult)
        {
            CanvasLog.AddTextSpace(str);
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return "Name: " + Name + ", Age: " + Age;
        }
    }

    private void CustomClass()
    {
        List<Person> peopleA = new List<Person>()
        {
            new Person { Name = "Bob", Age = 20 },
            new Person { Name = "Tom", Age = 25 },
            new Person { Name = "Sam", Age = 30 }
        };

        List<Person> peopleB = new List<Person>()
        {
            new Person { Name = "Ella", Age = 15 },
            new Person { Name = "Bob", Age = 20 }
        };

        // Query + Method
        List<Person> queryResult = (from person in peopleA
                                    select person)
                                    .Concat(peopleB)
                                    .ToList();

        // Method
        List<Person> methodResult = peopleA.Concat(peopleB).ToList();


        CanvasLog.AddTextLine("Query + Method");
        foreach (Person person in queryResult)
        {
            CanvasLog.AddTextLine(person);
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (Person person in methodResult)
        {
            CanvasLog.AddTextLine(person);
        }
    }
}
