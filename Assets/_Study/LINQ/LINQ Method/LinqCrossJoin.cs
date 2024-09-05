using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 크로스 조인은 첫 번째 데이터 집합의 각 요소를 두 번재 데이터 집합의 각 요소와 결합하는데 사용.
/// 크로스 조인된 결과는 데카르트 곱을 반환하므로 카티전 조인이라고 함.
/// 크로스 조인은 Join 조건이 존재하지 않음.
/// </summary>
public class LinqCrossJoin : MonoBehaviour
{
    public class Employee
    {
        public string Emp_Code { get; set; }  // 사원코드
        public string Emp_Name { get; set; }  // 사원명
        public static List<Employee> GetEmployees()
        {
            return new List<Employee>()
            {
                new Employee { Emp_Code = "1", Emp_Name = "홍길동"},
                new Employee { Emp_Code = "2", Emp_Name = "호날두"},
                new Employee { Emp_Code = "3", Emp_Name = "김첨지"}
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
                new Department { Dept_Code = "2000", Dept_Name = "기획"}
            };
        }
    }


    private void Start()
    {
        Sample1();
    }

    private void Sample1()
    {
        // Query
        var queryResult= (from emp in Employee.GetEmployees()
                           from dept in Department.GetDeptments()
                           select new
                           {
                               emp,
                               dept
                           });

        // Method
        var methodResult = Employee.GetEmployees()
            .SelectMany(dept => Department.GetDeptments(),
            (emp, dept) => new {emp, dept});


        CanvasLog.AddTextLine("Query");
        foreach(var item in queryResult)
        {
            CanvasLog.AddTextLine($"사원 코드: {item.emp.Emp_Code}, 사원명: {item.emp.Emp_Name}, 부서 코드: {item.dept.Dept_Code}, 부서명: {item.dept.Dept_Name}");
        }

        CanvasLog.AddTextLine("Method");
        foreach(var item in queryResult)
        {
            CanvasLog.AddTextLine($"사원 코드: {item.emp.Emp_Code}, 사원명: {item.emp.Emp_Name}, 부서 코드: {item.dept.Dept_Code}, 부서명: {item.dept.Dept_Name}");
        }
    }


}
