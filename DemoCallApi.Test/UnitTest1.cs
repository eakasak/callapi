using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoCallAPI;
using System.Linq;
using static DemoCallAPI.Program;
using System.Collections.Generic;

namespace DemoCallApi.Test
{
    [TestClass]
    public class UnitTest1
    {
        Program _Program = new Program();
        List<Employee> _Employee = new List<Employee>();
        int _isum = 0;


      [TestMethod]
        public void TestMethod1()
        {
           var res  = _Program.GetOrganizations();
            try
            {
                foreach (var item in res.AsParallel())
                {
                    var emp = _Program.GetEmployeeOrgChartByOrganizationId(item.organizationId);
                    InsertEmployee(emp, ref _Employee);
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        private void InsertEmployee(List<Employee> emp , ref List<Employee> empref)
        {
            foreach (var item in emp.AsParallel())
            {               
                empref.Add(item);
            }
        }
    }
}
