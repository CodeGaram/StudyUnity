using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// SequenceEqual() 메서드는 두 시퀀스를 비교하여 동일하면 true를, 아니면 false를 반환
/// 동일하다 == 동일한 수의 데이터가 존재하고, 동일한 순서로 정렬되어 있는 경우
/// IEqualityComparer 타입의 매개변수를 받아 두 시퀀스의 데이터를 비교하는 기준을 정의하는 오버로드 메서드가 존재.
/// </summary>
public class LinqSequenceEqual : MonoBehaviour
{
    private void Start()
    {
        //Sample1_IntegerType();
        //IfNotSorted_Integertype();
        //OrderByAfterSequenceEqual_Integertype();
        //Sample1_StringType();
        //UsedIEqualityComparer_StringType();
        //CustomClass();
        //UsedIEqualityComparer();
        UsedAnonymousTypes();
    }

    private void Sample1_IntegerType()
    {
        List<int> numList1 = new List<int>()
        { 10, 20, 30 };

        List<int> numList2 = new List<int>()
        { 10, 20, 30 };

        bool isSequenveEqual = numList1.SequenceEqual(numList2);

        CanvasLog.AddTextLine(isSequenveEqual);
    }

    private void IfNotSorted_Integertype()
    {
        List<int> numList1 = new List<int>()
        { 10, 20, 30 };

        List<int> numList2 = new List<int>()
        { 30, 20, 10 };

        bool isSequenceEqual = numList1.SequenceEqual(numList2);

        CanvasLog.AddTextLine(isSequenceEqual);
    }

    private void OrderByAfterSequenceEqual_Integertype()
    {
        List<int> numList1 = new List<int>() { 10, 20, 30 };
        List<int> numList2 = new List<int>() { 30, 20, 10 };

        bool isSequenceEqual = numList1.SequenceEqual(numList2.OrderBy(num => num));

        CanvasLog.AddTextLine(isSequenceEqual);
    }

    private void Sample1_StringType()
    {
        List<string> strList1 = new List<string>() { "ONE", "TWO", "THREE" };
        List<string> strList2 = new List<string>() { "one", "two", "three" };

        bool isSequenceEqual = strList1.SequenceEqual(strList2);

        CanvasLog.AddTextLine(isSequenceEqual); // false
    }

    private void UsedIEqualityComparer_StringType()
    {
        List<string> strList1 = new List<string>() { "ONE", "TWO", "THREE" };
        List<string> strList2 = new List<string>() { "one", "two", "three" };

        bool isSequenceEqual = strList1.SequenceEqual(strList2, StringComparer.OrdinalIgnoreCase);

        CanvasLog.AddTextLine(isSequenceEqual); // true
    }

    public class Employee
    {
        public string Emp_Code { get; set; }
        public string Emp_Name { get; set; }
    }

    /// <summary>
    /// 객체의 값이 아닌 객체의 참조 값을 비교하기 때문에 동일한 데이터라도 false를 반환함.
    /// =>
    /// 1. IEqualityComparer 인터페이스를 구현한 클래스를 정의하고 해당 인스턴스를 SequenceEqual() 메서드에 전달
    /// 2. Linq의 기능을 활용하여 익명 타입으로 값을 비교
    /// 3. Student 클래스에서 Equals() 및 GetHashCode() 메서드를 재정의
    /// </summary>
    private void CustomClass()
    {
        List<Employee> empList1 = new List<Employee>()
        {
            new Employee(){Emp_Code = "100", Emp_Name = "고길동"}
        };

        List<Employee> empList2 = new List<Employee>()
        {
            new Employee(){Emp_Code = "100", Emp_Name = "고길동"}
        };

        bool isSequenceEqual = empList1.SequenceEqual(empList2);

        CanvasLog.AddTextLine(isSequenceEqual); // false
    }


    public class EmployeeComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.Emp_Code == y.Emp_Code && x.Emp_Name == y.Emp_Name;
        }

        public int GetHashCode(Employee obj)
        {
            return obj.Emp_Code.GetHashCode() ^ obj.Emp_Name.GetHashCode();
        }
    }

    private void UsedIEqualityComparer()
    {
        List<Employee> empList1 = new List<Employee>()
        {
            new Employee(){Emp_Code = "100", Emp_Name = "고길동"}
        };

        List<Employee> empList2 = new List<Employee>()
        {
            new Employee(){Emp_Code = "100", Emp_Name = "고길동"}
        };

        EmployeeComparer employeeComparer = new EmployeeComparer();

        bool isSequenceEqual = empList1.SequenceEqual(empList2, employeeComparer);

        CanvasLog.AddTextLine(isSequenceEqual); // true
    }

    private void UsedAnonymousTypes()
    {
        List<Employee> empList1 = new List<Employee>()
        {
            new Employee(){Emp_Code = "100", Emp_Name = "고길동"}
        };

        List<Employee> empList2 = new List<Employee>()
        {
            new Employee(){Emp_Code = "100", Emp_Name = "고길동"}
        };

        bool isSequenceEqual = empList1.Select(emp => new {emp.Emp_Code, emp.Emp_Name})
            .SequenceEqual(empList2.Select(emp => new {emp.Emp_Code, emp.Emp_Name}) );

        CanvasLog.AddTextLine(isSequenceEqual); // true

    }

    public class Employee2
    {
        public string Emp_Code { get; set; }
        public string Emp_Name { get; set; }

        public override bool Equals(object obj)
        {
            return this.Emp_Code == ((Employee)obj).Emp_Code && this.Emp_Name == ((Employee)obj).Emp_Name;
        }

        public override int GetHashCode()
        {
            return this.Emp_Code.GetHashCode() ^ this.Emp_Name.GetHashCode();
        }
    }
}
