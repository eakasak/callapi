using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DemoCallAPI;
using System.Linq;
using static DemoCallAPI.Program;
using System.Collections.Generic;
using DemoCallAPI.Entities;

namespace DemoCallApi.Test
{
    [TestClass]
    public class UnitTest1
    {
        Program _Program = new Program();

        [TestMethod]
        public void TestMethod1()
        {
           var res  = _Program.GetOrganizations();
            try
            {
                foreach (var item in res)
                {
                    _Program.InsertOrganization(Add_org_external(item));
                }

                var organizationIds = res.AsParallel().Select(s => s.organizationId);
                organizationIds.ForAll((x) =>
                {
                    var emp = _Program.GetMembersByOrganizationId(x);
                    _Program.InsertMember(Add_org_external_member(emp));
                });
            }
            catch (Exception ex)
            {
               
            }
        }
        
        private org_external Add_org_external(Organizations orgs)
        {
          var  empref = new org_external
            {
                // common_id = auto,
                org_id = orgs.organizationId.ToString(),
                org_code = orgs.organizationId.ToString(),
                org_name = orgs.organizationName_EN,
                org_id_parent = orgs.organizationParentServiceList.FirstOrDefault()?.organizationParentId.ToString() ?? "NULL"
            };
            return empref;
        }

        private List<org_external_member> Add_org_external_member(List<Member> emp )
        {
            List<org_external_member> org_external_members = new List<org_external_member>();
            foreach (var item in emp)
            {
                var mem = new org_external_member
                {
                    // common_id = auto,
                    org_code = item.organizationId.ToString(),
                    user_code = item.adAccount.ToString()                  
                };
                org_external_members.Add(mem);
              
            }
            return org_external_members;
        }

      
    }
}
