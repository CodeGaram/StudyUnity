using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 프로퍼티가 여러개인 경우 OrderBy() 다음에 ThenBy() / ThenByDescending() 사용
/// Query Syntax에는 orderby절에서 콤마로 이어서 작성
/// </summary>
public class LinqThenBy : MonoBehaviour
{
    private void Start()
    {
        //Example1();
        //SortPropertyCountOver3();
        UsedWithWhere();
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

    private void Example1()
    {
        List<Person> people = new List<Person>() 
        {
            new Person{ Name = "Tom",  Age=30},
            new Person{ Name = "Tom",  Age=33},
            new Person{ Name = "Nick", Age=23},
            new Person{ Name = "Nick", Age=20},
            new Person{ Name = "Elsa", Age=15},
            new Person{ Name = "Elsa", Age=40},
            new Person{ Name = "Elsa", Age=28},
        };

        // Query
        List<Person> queryResult = (from person in people
                                   orderby person.Name ascending, person.Age descending
                                   select person).ToList();

        // Method
        List<Person> methodResult = people.OrderBy(person => person.Name)
            .ThenByDescending(person => person.Age)
            .ToList();


        CanvasLog.AddTextLine("Query");
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

    public class Person1
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public override string ToString()
        {
            return "Name: " + Name + ", Age: " + Age + ", Salary: " + Salary;
        }
    }

    private void SortPropertyCountOver3()
    {
        List<Person1> people = new List<Person1>() {
            new Person1{ Name = "Tom",  Age=30, Salary = 50000},
            new Person1{ Name = "Tom",  Age=30, Salary = 40000},
            new Person1{ Name = "Tom",  Age=33, Salary = 45000},
            new Person1{ Name = "Tom",  Age=33, Salary = 30000},
            new Person1{ Name = "Nick", Age=23, Salary = 60000},
            new Person1{ Name = "Nick", Age=23, Salary = 100000},
            new Person1{ Name = "Nick", Age=20, Salary = 55000},
            new Person1{ Name = "Nick", Age=20, Salary = 65000},
            new Person1{ Name = "Elsa", Age=15, Salary = 5000},
            new Person1{ Name = "Elsa", Age=15, Salary = 15000},
            new Person1{ Name = "Elsa", Age=40, Salary = 10000},
            new Person1{ Name = "Elsa", Age=40, Salary = 20000},
        };

        // Query
        List<Person1> queryResult = (from person in people
                                     orderby person.Name ascending, person.Age descending, person.Salary ascending
                                     select person).ToList();

        // Method
        List<Person1> methodResult = people.OrderBy(person => person.Name)
            .ThenByDescending(person => person.Age)
            .ThenBy(person => person.Salary)
            .ToList();


        CanvasLog.AddTextLine("Query");
        foreach (Person1 person in queryResult)
        {
            CanvasLog.AddTextLine(person);
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (Person1 person in methodResult)
        {
            CanvasLog.AddTextLine(person);
        }
    }

    private void UsedWithWhere()
    {
        List<Person1> people = new List<Person1>() {
            new Person1{ Name = "Tom",  Age=30, Salary = 50000},
            new Person1{ Name = "Tom",  Age=30, Salary = 40000},
            new Person1{ Name = "Tom",  Age=33, Salary = 45000},
            new Person1{ Name = "Tom",  Age=33, Salary = 30000},
            new Person1{ Name = "Nick", Age=23, Salary = 60000},
            new Person1{ Name = "Nick", Age=23, Salary = 100000},
            new Person1{ Name = "Nick", Age=20, Salary = 55000},
            new Person1{ Name = "Nick", Age=20, Salary = 65000},
            new Person1{ Name = "Elsa", Age=15, Salary = 5000},
            new Person1{ Name = "Elsa", Age=15, Salary = 15000},
            new Person1{ Name = "Elsa", Age=40, Salary = 10000},
            new Person1{ Name = "Elsa", Age=40, Salary = 20000},
        };

        // Query
        List<Person1> queryResult = (from person in people
                                     where person.Age > 20 && person.Salary > 30000
                                     orderby person.Name, person.Age descending, person.Salary
                                     select person)
                                     .ToList();

        // Method
        List<Person1> methodResult = people.Where(person => person.Age > 20 && person.Salary > 30000)
            .OrderBy(person => person.Name)
            .ThenByDescending(person => person.Age)
            .ThenBy(person => person.Salary)
            .ToList();


        CanvasLog.AddTextLine("Query");
        foreach (Person1 person in queryResult)
        {
            CanvasLog.AddTextLine(person);
        }

        CanvasLog.AddTextLine("\nMethod");
        foreach (Person1 person in methodResult)
        {
            CanvasLog.AddTextLine(person);
        }
    }
}
