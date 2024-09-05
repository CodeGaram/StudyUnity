using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// LeftJoin / LeftOuterJoin은 두 번째 데이터 집합에 일치하는 데이터가 있는지 관계없이 첫 번째 데이터 집합의 모든 데이터가 반환되는 Join
/// Linq에서는 LeftJoint 메서드와 연산자를 지원하지 않음.
/// C#에서 Linq를 사용하여 LeftJoin을 구현하려면, DefaultIfEmpty() 메서드와 into 키워드를 사용해야 함
/// </summary>
public class LinqLeftJoin : MonoBehaviour
{
    public class Employee
    {
        public string Emp_Code { get; set; }  // 사원코드
        public string Emp_Name { get; set; }  // 사원명
        public string Dept_Code { get; set; } // 부서코드
        public static List<Employee> GetEmployees()
        {
            return new List<Employee>()
            {
                new Employee { Emp_Code = "1", Emp_Name = "홍길동", Dept_Code = "1000"},
                new Employee { Emp_Code = "2", Emp_Name = "호날두", Dept_Code = "1000"},
                new Employee { Emp_Code = "3", Emp_Name = "김첨지", Dept_Code = "2000"},
                new Employee { Emp_Code = "4", Emp_Name = "다람쥐", Dept_Code = "2000"},
                new Employee { Emp_Code = "5", Emp_Name = "나랑두", Dept_Code = "3000"}
            };
        }
    }

    public class Department
    {
        public string Dept_Code { get; set; } // 부서코드
        public string Dept_Name { get; set; } // 부서명
        public static List<Department> GetDeptments()
        {
            return new List<Department>()
            {
                new Department { Dept_Code = "1000", Dept_Name = "영업"},
                new Department { Dept_Code = "2000", Dept_Name = "기획"},
                new Department { Dept_Code = "4000", Dept_Name = "연구"},
                new Department { Dept_Code = "5000", Dept_Name = "분석"}
            };
        }
    }


    private void Start()
    {
        //QuerySample1();
        MethodSample1();
    }


    /// <summary>
    /// Query Syntax에서 Left Join을 구현하려면 그룹 조인 결과에 대해 DefaultIfEmpty() 메서드를 호출해야 함
    /// 1. 첫 번재 단계로 그룹 조인을 수행.
    /// 2. 그룹 조인 결과에 대해 DefaultIfEmpty() 메서드를 호출. -> TOuter 리스트가 TInner 리스트에 일치하는지 관계없이 항상 반환되어야 하기 때문
    /// DefaultIfEmpty() 메서드를 호출한 데이터 집합의 데이터가 비어 있으면, 기본 값을 가지는 컬렉션을 반환함.
    /// </summary>
    private void QuerySample1()
    {
        var queryResult = (from employee in Employee.GetEmployees()             // TOuter
                           join department in Department.GetDeptments()         // TInner
                           on employee.Dept_Code equals department.Dept_Code    // on outerKeySelector equals innerKeySelector
                           into EmployeeDeptGroup                               // Result => GroupJoin
                           from department in EmployeeDeptGroup.DefaultIfEmpty()// LeftJoin
                           select new { employee, department });                // Select

        foreach(var item in queryResult)
        {
            CanvasLog.AddTextLine("사원명: " + item.employee.Emp_Name + ", 부서명: " + item.department?.Dept_Name);
        }
    }

    /// <summary>
    /// Method Syntax에서 Left Join을 구현하려면, GroupJoin() 메서드 결과에 대해 SelectMany() 메서드와 DefaultIfEmpty() 메서드를 호출
    /// </summary>
    private void MethodSample1()
    {
        var methodResult = Employee.GetEmployees()                      // TOuter
            .GroupJoin(                                                 // GroupJoin
            Department.GetDeptments(),                                  // TInner
            (employee) => employee.Dept_Code,                           // outerKeySelector
            (department) => department.Dept_Code,                       // innerKeySelector
            (employee, department) => new { employee, department })     // Result
            .SelectMany(                                                // SelectMany
            item => item.department.DefaultIfEmpty(),                   // LeftJoin
            (employee, department) => new { employee.employee, department });    // Select

        foreach(var item in methodResult)
        {
            CanvasLog.AddTextLine("사원명: " + item.employee.Emp_Name + ", 부서명: " + item.department?.Dept_Name);
        }
    }

}
