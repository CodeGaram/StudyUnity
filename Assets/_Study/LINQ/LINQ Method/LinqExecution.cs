using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// 즉시 실행(Immediate Execution)
/// - count, average, min, max, ToArray, ToList 등.. 즉시 실행에 포함 됨.
/// 
/// 지연 실행(Deffered Execution)
/// - select, SelectManay, where, Take, Skip 등.. 지연 실행에 포함 됨.
/// </summary>
public class LinqExecution : MonoBehaviour
{
    private void Start()
    {
        //ImmediateExecution();
        DefferedExecution();
    }

    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }


    private void ImmediateExecution()
    {
        List<Person> people = new List<Person>()
        {
            new Person() {ID = 1000, Name = "둘리", Age = 20},
            new Person() {ID = 2000, Name = "마이콜", Age = 30},
            new Person() {ID = 3000, Name = "고길동", Age = 40},
        };

        // 1. people에 ToList() 메서드를 사용하여 결과를 바로 가져옴.
        IEnumerable<Person> immediateResult = people.ToList();

        // 2. people에 새로운 데이터를 추가.
        people.Add(new Person { ID = 4000, Name = "또치", Age = 50 });

        // 3. 1번 과정에서 반환받은 결과를 출력.
        foreach(Person person in immediateResult )
        {
            CanvasLog.AddTextLine("{ ID: " + person.ID +
                ", Name: " + person.Name +
                ", Age: " + person.Age + "}");
        }
    }

    private void DefferedExecution()
    {
        List<Person> people = new List<Person>()
        {
            new Person() {ID = 1000, Name = "둘리", Age = 20},
            new Person() {ID = 2000, Name = "마이콜", Age = 30},
            new Person() {ID = 3000, Name = "고길동", Age = 40},
        };

        // 1. people에 select절을 사용하여 결과를 나중에 가져옴.
        IEnumerable<Person> deferredResult = (from person in people
                                              select person);

        // 2. people에 새로운 데이터를 추가
        people.Add(new Person { ID = 4000, Name = "또치", Age = 50 });

        // 3. 1번 과정에서 반환받은 결과를 출력
        foreach( Person person in deferredResult)
        {
            CanvasLog.AddTextLine("{ ID: " + person.ID +
                ", Name: " + person.Name +
                ", Age: " + person.Age + "}");
        }
    }
}
