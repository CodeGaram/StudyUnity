using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 계속 반복하며 값을 누적시키는 누적기
/// seed를 사용하는 오버로드 메서드와 최종 누적값을 가지고 연산하는 오버로드 메서드가 있음
/// Query Syntax는 지원하지 않음
/// </summary>
public class LinqAggregate : MonoBehaviour
{
    private void Start()
    {
        //StringType();
        //IntegerType();
        //SeedOverload();
        //SeedOverload_CustomClass();
        ResultSelectorOverload_CustomClass();
    }

    private void StringType()
    {
        List<string> strList = new List<string>()
        {
            "Java", "C#", "React", "Svelte"
        };

        string methodResult = strList.Aggregate((s1, s2) =>
        {
            CanvasLog.AddTextLine("s1: " + s1);
            CanvasLog.AddTextLine("s2: " + s2 + "\n");
            return s1 + " / " + s2;
        });

        CanvasLog.AddTextLine("Method: " +  methodResult);
    }

    private void IntegerType()
    {
        List<int> intList = new List<int>()
        {
            5, 10, 20
        };

        int methodResult = intList.Aggregate((num1, num2) =>
        {
            CanvasLog.AddTextLine("num1: " + num1);
            CanvasLog.AddTextLine("num2: " + num2 + "\n");
            return num1 * num2;
        });

        CanvasLog.AddTextLine("Method: " + methodResult);
    }

    /// <summary>
    /// 첫 루프에서 누적되는 값이 데이터 집합의 첫 번째 요소가 아니라 seed값을 가리킴.
    /// </summary>
    private void SeedOverload()
    {
        List<int> intList = new List<int>()
        {
            5, 10, 20
        };

        int methodResult = intList.Aggregate(3, (num1, num2) =>
        {
            CanvasLog.AddTextLine("num1: " + num1);
            CanvasLog.AddTextLine("num2: " + num2 + "\n");
            return num1 * num2;
        });

        CanvasLog.AddTextLine("Method: " + methodResult);
    }
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Money { get; set; }
    }

    /// <summary>
    /// TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func);
    /// <입력 타입, 반환 타입>(시드, 입력값, 반환값 리턴)
    /// </summary>
    private void SeedOverload_CustomClass()
    {
        List<Person> people = new List<Person>
        {
            new Person{Name ="Bob",  Age = 20, Money = 30000},
            new Person{Name ="Nick", Age = 30, Money = 50000},
            new Person{Name ="Tom",  Age = 40, Money = 80000}
        };

        // Query
        string queryResult = (from person in people
                              select person)
                              .Aggregate<Person, string>("Name: ", (person1, person2) =>
                              {
                                  return person1 + person2.Name + " / ";
                              });

        // Method
        string methodResult = people.Aggregate<Person, string>("Name: ", (person1, person2) =>
        {
            return person1 + person2.Name + " / ";
        });

        int queryResultLastIndex = queryResult.LastIndexOf("/");
        queryResult = queryResult.Remove(queryResultLastIndex);

        int methodResultLastIndex = methodResult.LastIndexOf("/");
        methodResult = methodResult.Remove(methodResultLastIndex);


        CanvasLog.AddTextLine("Query: \n" + queryResult);
        CanvasLog.AddTextLine("\nMethod: \n" + methodResult);
    }

    /// <summary>
    /// TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> 
    /// </summary>
    private void ResultSelectorOverload_CustomClass()
    {
        List<Person> people = new List<Person>
        {
            new Person{Name ="Bob",  Age = 20, Money = 30000},
            new Person{Name ="Nick", Age = 30, Money = 50000},
            new Person{Name ="Tom",  Age = 40, Money = 80000}
        };

        // Query
        string queryResult = (from person in people
                              select person)
                              .Aggregate<Person, string, string>("Name: ", (person1, person2) =>
                              {
                                  return person1 + person2.Name + " / ";
                              }, 
                              (result) =>
                              {
                                  int index = result.LastIndexOf("/");
                                  return result.Remove(index);
                              });

        // Method
        string methodResult = people.Aggregate<Person, string, string>("Name: ",
            (person1, person2) =>
            {
                return person1 + person2.Name + " / ";
            },
            (result) =>
            {
                int index = result.LastIndexOf("/");
                return result.Remove(index);
            });


        CanvasLog.AddTextLine("Query: \n" + queryResult);
        CanvasLog.AddTextLine("\nMethod: \n" + methodResult);
    }
}
