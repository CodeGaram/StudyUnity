using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// 데이터를 변경하는 것이 아니라 데이터의 순서만 변경
/// OrderBy(): 오름차순 정렬 / OrderByDescending(): 내림차순 정렬
/// orderby A (ascending): 오름차순 정렬 / orderby A descending: 내림차순 정렬
/// </summary>
public class LinqOrderBy : MonoBehaviour
{
    private void Start()
    {
        //IntegerType();
        //StringType();
        //CustomClass();
        UsedWithWhere();
    }


    private void IntegerType()
    {
        List<int> intArray = new List<int>() { 2, 4, 3, 5, 1, 0 };

        // Query - Ascending
        List<int> queryResultAscending = (from num in intArray
                                         orderby num
                                         select num).ToList();

        // Query - Descending
        List<int> queryResultDescending = (from num in intArray
                                           orderby num descending
                                           select num).ToList();

        // Method - Ascending
        List<int> methodResultAscending = intArray.OrderBy(num => num).ToList();

        // Method - Descending
        List<int> methodResultDescending = intArray.OrderByDescending(num => num).ToList();


        CanvasLog.AddTextLine("Query - Ascending");
        foreach (int num in queryResultAscending)
        {
            CanvasLog.AddTextSpace(num);
        }

        CanvasLog.AddTextLine("\nQuery - Descending");
        foreach (int num in queryResultDescending)
        {
            CanvasLog.AddTextSpace(num);
        }

        CanvasLog.AddTextLine("\nMethod - Ascending");
        foreach (int num in methodResultAscending)
        {
            CanvasLog.AddTextSpace(num);
        }

        CanvasLog.AddTextLine("\nMethod - Descending");
        foreach (int num in methodResultDescending)
        {
            CanvasLog.AddTextSpace(num);
        }
    }

    private void StringType()
    {
        List<string> strArray = new List<string>()
        {
            "Starbucks", "Costco", "P&G", "Apple", "amazon"
        };

        // Query - Ascending
        List<string> queryResultAscending = (from str in strArray
                                             orderby str ascending
                                             select str).ToList();

        // Query - Descending
        List<string> queryResultDescending = (from str in strArray
                                              orderby str descending
                                              select str).ToList();

        // Method - Ascending
        List<string> methodResultAscending = strArray.OrderBy(str => str).ToList();

        // Method - Descending
        List<string> methodResultDescending = strArray.OrderByDescending(str => str).ToList();


        CanvasLog.AddTextLine("Query - Ascending");
        foreach (string str in queryResultAscending)
        {
            CanvasLog.AddTextSpace(str);
        }

        CanvasLog.AddTextLine("\nQuery - Descending");
        foreach (string str in queryResultDescending)
        {
            CanvasLog.AddTextSpace(str);
        }

        CanvasLog.AddTextLine("\nMethod - Ascending");
        foreach (string str in methodResultAscending)
        {
            CanvasLog.AddTextSpace(str);
        }

        CanvasLog.AddTextLine("\nMethod - Descending");
        foreach (string str in methodResultDescending)
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
        List<Person> people = new List<Person>()
        {
            new Person { Name = "Tom", Age = 30 },
            new Person { Name = "Nick", Age = 20 },
            new Person { Name = "Elsa", Age = 28 },
            new Person { Name = "Jack", Age = 15 },
            new Person { Name = "Sam", Age = 33 }
        };

        // Query - Ascending
        List<Person> queryResultAscending = (from person in people
                                             orderby person.Age ascending
                                             select person).ToList();

        // Query - Descending
        List<Person> queryResultDescending = (from person in people
                                               orderby person.Age descending
                                               select person).ToList();

        // Method - Ascending
        List<Person> methodResultAscending = people.OrderBy(person => person.Age).ToList();

        // Method - descending
        List<Person> methodResultDescending = people.OrderByDescending(person => person.Age).ToList();


        CanvasLog.AddTextLine("Query - Ascending");
        foreach (Person person in queryResultAscending)
        {
            CanvasLog.AddTextLine(person);
        }

        CanvasLog.AddTextLine("\nQuery - Descending");
        foreach (Person person in queryResultDescending)
        {
            CanvasLog.AddTextLine(person);
        }

        CanvasLog.AddTextLine("\nMethod - Ascending");
        foreach (Person person in methodResultAscending)
        {
            CanvasLog.AddTextLine(person);
        }

        CanvasLog.AddTextLine("\nMethod - Descending");
        foreach (Person person in methodResultDescending)
        {
            CanvasLog.AddTextLine(person);
        }
    }
    
    private void UsedWithWhere()
    {
        List<Person> people = new List<Person>()
        {
            new Person { Name = "Tom", Age = 30 },
            new Person { Name = "Nick", Age = 20 },
            new Person { Name = "Elsa", Age = 28 },
            new Person { Name = "Jack", Age = 15 },
            new Person { Name = "Sam", Age = 33 }
        };

        // Query - Ascending
        List<Person> queryResultAscending = (from person in people
                                             where person.Age > 20
                                             orderby person.Age ascending
                                             select person).ToList();

        // Query - Descending
        List<Person> queryResultDescending = (from person in people
                                              where person.Age > 20
                                              orderby person.Age descending
                                              select person).ToList();

        // Method - Ascending
        List<Person> methodResultAscending = people.Where(person => person.Age > 20).OrderBy(person => person.Age).ToList();

        // Method - descending
        List<Person> methodResultDescending = people.Where(person => person.Age > 20).OrderByDescending(person => person.Age).ToList();


        CanvasLog.AddTextLine("Query - Ascending");
        foreach (Person person in queryResultAscending)
        {
            CanvasLog.AddTextLine(person);
        }

        CanvasLog.AddTextLine("\nQuery - Descending");
        foreach (Person person in queryResultDescending)
        {
            CanvasLog.AddTextLine(person);
        }

        CanvasLog.AddTextLine("\nMethod - Ascending");
        foreach (Person person in methodResultAscending)
        {
            CanvasLog.AddTextLine(person);
        }

        CanvasLog.AddTextLine("\nMethod - Descending");
        foreach (Person person in methodResultDescending)
        {
            CanvasLog.AddTextLine(person);
        }
    }
}
