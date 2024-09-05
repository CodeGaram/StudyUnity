using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// 네 가지 오버로드 버전이 존재 중 Func<TSource, TElemnet> elementSelector를 사용하는 메서드 예제
/// </summary>
public class LinqToDictionary : MonoBehaviour
{
    private void Start()
    {
        //Sample1();
        //ChangeValue();
        UsedCustomClass();
    }

    private void Sample1()
    {
        List<int> intList = new List<int>()
        {
            1, 2, 3, 4, 5
        };

        Dictionary<string, int> linqToDictionary = intList
            .Where(num => num > 2)
            .ToDictionary(item => item.ToString());

        foreach (var item in linqToDictionary)
        {
            CanvasLog.AddTextLine("Key: " + item.Key + ", Value: " + item.Value);
        }
    }

    private void ChangeValue()
    {
        List<int> intList = new List<int>()
        {
            1, 2, 3, 4, 5
        };

        Dictionary<string, int> linqToDictionary = intList
            .Where(num => num > 2)
            .ToDictionary(
            (item => item.ToString()),
            (item => item * 100));

        foreach (var item in linqToDictionary)
        {
            CanvasLog.AddTextLine("Key: " + item.Key + ", Value: " + item.Value);
        }
    }

    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    private void UsedCustomClass()
    {
        List<Person> people = new List<Person>()
        {
            new Person() {ID = 1000, Name = "둘리", Age = 20},
            new Person() {ID = 2000, Name = "마이콜", Age = 30},
            new Person() {ID = 3000, Name = "고길동", Age = 40},
        };

        Dictionary<int, Person> linqToDictionary = people
            .Where(person => person.Age > 25)
            .ToDictionary(person => person.ID);

        foreach(var person in linqToDictionary)
        {
            CanvasLog.AddTextLine("Key: " + person.Key +
                ", Value: { ID: " + person.Value.ID +
                ", Name: " + person.Value.Name +
                ", Age: " + person.Value.Age + " }");
        }
    }
}
