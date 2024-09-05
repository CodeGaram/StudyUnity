using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LinqWhere : MonoBehaviour
{
    private void Start()
    {
        //Example1();
        //Example2();
        CustomClass();
    }

    private void Example1()
    {
        List<int> nums = new List<int>()
        {
            1, 3, 5, 6, 7, 9, 10
        };

        // Query
        List<int> queryResult = (from num in nums
                                 where num > 5
                                 select num).ToList();

        // Method
        List<int> methodResult = nums.Where((num) => num > 5).ToList();

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

    private void Example2()
    {
        List<int> nums = new List<int>()
        {
            1, 3, 5, 6, 7, 9, 10
        };

        // Query
        List<int> queryResult = (from num in nums
                                 where num > 5 && num % 2 != 0
                                 select num).ToList();

        // Method
        List<int> methodResult = nums.Where((num) => num > 5 && num % 2 != 0).ToList();

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

    class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }
        public override string ToString()
        {
            return "ID: " + ID + ", Name: " + Name + ", Age: " + Age + ", Salary: " + Salary;
        }
    }

    private void CustomClass()
    {
        List<Person> people = new List<Person>
        {
            new Person() { ID = 100, Name = "Bob",     Age = 20, Salary = 30000  },
            new Person() { ID = 200, Name = "Tim",     Age = 25, Salary = 40000 },
            new Person() { ID = 300, Name = "Charles", Age = 30, Salary = 50000 }
        };

        // Query
        List<Person> queryResult = (from person in people
                                    where person.Age >= 25 && person.Salary < 45000
                                    select person).ToList();

        // Method
        List<Person> methodResult = people.Where((person) => person.Age >= 25 && person.Salary < 45000).ToList();


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
}
