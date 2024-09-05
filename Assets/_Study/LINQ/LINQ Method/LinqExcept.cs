using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class LinqExcept : MonoBehaviour
{
    private void Start()
    {
        //TypeInt();
        //TypeString();
        //TypeStringUsedIEqualityComparer();
        //CustomMethod();
        //UsedAnonymousTypes();
        UsedIEqualityComparer();
    }

    private void TypeInt()
    {
        List<int> intArrayA = new List<int>() { 0, 2, 4, 5 };
        List<int> intArrayB = new List<int>() { 1, 2, 3, 5 };

        // Query + Method
        List<int> queryResult = (from num in intArrayA
                                 select num).Except(intArrayB).ToList();

        // Method
        List<int> methodResult = intArrayA.Except(intArrayB).ToList();


        CanvasLog.AddTextLine("Query");
        foreach (int num in queryResult)
        {
            CanvasLog.AddText(num);
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (int num in methodResult)
        {
            CanvasLog.AddText(num);
        }
    }

    private void TypeString()
    {
        List<string> strArrayA = new List<string>() { "one", "two", "three" };
        List<string> strArrayB = new List<string>() { "One", "Two", "Three" };

        // Query + Method
        List<string> queryResult = (from str in strArrayA
                                    select str).Except(strArrayB).ToList();

        // Method
        List<string> methodResult = strArrayA.Except(strArrayB).ToList();


        CanvasLog.AddTextLine("Query");
        foreach (string str in queryResult)
        {
            CanvasLog.AddText(str);
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (string str in methodResult)
        {
            CanvasLog.AddText(str);
        }
    }

    private void TypeStringUsedIEqualityComparer()
    {
        List<string> strArrayA = new List<string>() { "one", "two", "three" };
        List<string> strArrayB = new List<string>() { "One", "Two", "Three" };

        // Query + Method
        List<string> queryResult = (from str in strArrayA
                                    select str).Except(strArrayB, StringComparer.OrdinalIgnoreCase).ToList();

        // Method
        List<string> methodResult = strArrayA.Except(strArrayB, StringComparer.OrdinalIgnoreCase).ToList();


        CanvasLog.AddTextLine("Query");
        foreach (string str in queryResult)
        {
            CanvasLog.AddText(str);
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (string str in methodResult)
        {
            CanvasLog.AddText(str);
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

    /// <summary>
    /// 사용자 정의 클래스는 위와 같이 Except이 되지 않음.
    /// </summary>
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
                                    .Except(peopleB)
                                    .ToList();

        // Method
        List<Person> methodResult = peopleA.Except(peopleB).ToList();


        CanvasLog.AddTextLine("Query");
        foreach (Person person in queryResult)
        {
            CanvasLog.AddText(person.ToString());
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (Person person in methodResult)
        {
            CanvasLog.AddText(person.ToString());
        }
    }

    private void UsedAnonymousTypes()
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
        var queryResult = (from person in peopleA
                            select new { person.Name, person.Age })
                            .Except(peopleB.Select((person) => new { person.Name, person.Age }));

        // Method
        var methodResult = peopleA.Select(person => new { person.Name, person.Age })
            .Except(peopleB.Select(person => new { person.Name, person.Age }));


        CanvasLog.AddTextLine("Query");
        foreach (var person in queryResult)
        {
            CanvasLog.AddText(person.ToString());
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (var person in methodResult)
        {
            CanvasLog.AddText(person.ToString());
        }

    }

    public class PersonComparer : IEqualityComparer<Person>
    {
        public bool Equals(Person x, Person y)
        {
            if (object.ReferenceEquals(x, y)) return true;
            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null)) return false;

            return x.Name == y.Name && x.Age == y.Age;
        }

        public int GetHashCode(Person obj)
        {
            if(obj == null) return 0;

            int NameHashCode = obj.Name == null ? 0 : obj.Name.GetHashCode();
            int AgeHashCode = obj.Age.GetHashCode();

            return NameHashCode ^ AgeHashCode;
        }
    }

    private void UsedIEqualityComparer()
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

        PersonComparer personComparer = new PersonComparer();

        // Query + Method
        List<Person> queryResult = (from person in peopleA
                                    select person)
                                    .Except(peopleB, personComparer)
                                    .ToList();

        // Method
        List<Person> methodResult = peopleA.Except(peopleB, personComparer).ToList();

        CanvasLog.AddTextLine("Query + Method");
        foreach(Person person in queryResult)
        {
            CanvasLog.AddText(person.ToString());
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach(Person person in methodResult)
        {
            CanvasLog.AddText(person.ToString());
        }
    }
}
