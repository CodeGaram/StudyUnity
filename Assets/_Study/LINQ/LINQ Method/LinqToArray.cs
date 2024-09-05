using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class LinqToArray : MonoBehaviour
{
    private void Start()
    {
        //Sample1_IntegerType();
        ChangeValueAtCustomClass();
    }

    private void Sample1_IntegerType()
    {
        List<int> intList = new List<int>()
        {
            1, 2, 3
        };

        int[] linqToArray = intList.Where(num => num > 1).ToArray();

        CanvasLog.AddTextLine("linqToArray 결과");
        foreach (var item in linqToArray)
        {
            CanvasLog.AddText(item + " ");
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    private void ChangeValueAtCustomClass()
    {
        List<Person> personList = new List<Person>()
        {
            new Person() {Name = "둘리", Age = 20},
            new Person() {Name = "마이콜", Age = 30},
            new Person() {Name = "고길동", Age = 40},
        };

        Person[] linqToArray = personList
            .Where(num => num.Age > 25)
            .Select(item => new Person { Name = item.Name, Age = item.Age * 10 })
            .ToArray();

        foreach (var item in linqToArray)
        {
            CanvasLog.AddTextLine("Name: " + item.Name + ", Age: " + item.Age);
        }
    }

}
