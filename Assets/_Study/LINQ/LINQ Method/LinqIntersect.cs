using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class LinqIntersect : MonoBehaviour
{
    private void Start()
    {
        //IntegerType();
        //StringType();
        //CustomMethod();
        //UsedAnonymousTypes();
        UsedIEqualityComparer();
    }

    private void IntegerType()
    {
        List<int> intArrayA = new List<int>() { 0, 2, 4, 5 };
        List<int> intArrayB = new List<int>() { 1, 2, 3, 5 };

        // Query + Method
        List<int> queryResult = (from num in intArrayA
                                 select num).Intersect(intArrayB).ToList();

        // Method
        List<int> methodResult = intArrayA.Intersect(intArrayB).ToList();

        CanvasLog.AddTextLine("Query + Method");
        foreach(int num in queryResult)
        {
            CanvasLog.AddTextSpace(num);
        }
        
        CanvasLog.AddTextLine("\nMethod");
        foreach(int num in methodResult)
        {
            CanvasLog.AddTextSpace(num);
        }
    }

    /// <summary>
    /// string에서 대소문자가 다르면 다른 문자열로 구분하므로 IEqualityComparer를 매개변수로 사용하는 Intersect를 사용
    /// </summary>
    private void StringType()
    {
        List<string> strArrayA = new List<string>() { "one", "two", "three" };
        List<string> strArrayB = new List<string>() { "One", "Two", "Three" };

        // Query + Method
        List<string> queryResult = (from str in strArrayA
                                    select str).Intersect(strArrayB, StringComparer.OrdinalIgnoreCase).ToList();

        // Method
        List<string> methodResult = strArrayA.Intersect(strArrayB, StringComparer.OrdinalIgnoreCase).ToList();


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

    /// <summary>
    /// Intersect는 객체의 프로퍼티까지 참조하지 않아 값이 제대로 나오지 않음
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
                                    select person).Intersect(peopleB)
                                    .ToList();

        // Method
        List<Person> methodResult = peopleA.Intersect(peopleB).ToList();


        CanvasLog.AddTextLine("Query + Method");
        foreach (Person person in queryResult)
        {
            CanvasLog.AddTextLine(person);
        }

        CanvasLog.AddTextLine("Method");
        foreach (Person person in methodResult)
        {
            CanvasLog.AddTextLine(person);
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
                                    .Intersect(peopleB.Select(person => new { person.Name, person.Age }));

        // Method
        var methodResult = peopleA.Select(person => new {person.Name, person.Age})
            .Intersect(peopleB.Select(person => new {person.Name, person.Age}));


        CanvasLog.AddTextLine("Query + Method");
        foreach (var person in queryResult)
        {
            CanvasLog.AddTextLine(person);
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (var person in methodResult)
        {
            CanvasLog.AddTextLine(person);
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
                           .Intersect(peopleB, personComparer)
                           .ToList();

        // Method
        var methodResult = peopleA.Intersect(peopleB, personComparer).ToList();


        CanvasLog.AddTextLine("Query + Method");
        foreach (var person in queryResult)
        {
            CanvasLog.AddTextLine(person);
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (var person in methodResult)
        {
            CanvasLog.AddTextLine(person);
        }
    }
}
