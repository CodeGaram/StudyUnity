using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class LinqToList : MonoBehaviour
{
    private void Start()
    {
        //Exapmle1_IntegerType();
        //UsedCustomClass();
        ChangeValueAtCustomClass();
    }

    private void Exapmle1_IntegerType()
    {
        List<int> intList = new List<int>()
        {
            1, 2, 3
        };

        List<int> linqToList = intList.Where(num => num > 1).ToList();

        CanvasLog.AddTextLine("linqToList 결과");
        linqToList.ForEach(num => CanvasLog.AddText(num + " "));
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    private void UsedCustomClass()
    {
        List<Person> personList = new List<Person>()
        {
            new Person() {Name = "둘리", Age = 20},
            new Person() {Name = "마이콜", Age = 30},
            new Person() {Name = "고길동", Age = 40},
        };

        List<Person> linqToList = personList.Where(num => num.Age > 25).ToList();

        linqToList.ForEach((person) => CanvasLog.AddTextLine("Name: " + person.Name + ", Age: " + person.Age));
    }

    private void ChangeValueAtCustomClass()
    {
        List<Person> personList = new List<Person>()
        {
            new Person() {Name = "둘리", Age = 20},
            new Person() {Name = "마이콜", Age = 30},
            new Person() {Name = "고길동", Age = 40},
        };

        List<Person> linqToList = personList
            .Where(num => num.Age > 25)
            .Select(item => new Person { Name = item.Name, Age = item.Age * 10})
            .ToList();

        linqToList.ForEach(person => CanvasLog.AddTextLine("Name: " + person.Name + ", Age: " + person.Age));
    }

}
