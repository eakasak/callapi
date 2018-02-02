using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DemoCallAPI
{
    class Program
    {
        private const string apiBaseUri = "https://scgchem-mdmdev.scg.com";
        private static string requestPath;
        private static string requestParam;
        private static ApiRequest req;

        static void Main(string[] args)
        {
           
            requestPath = "/Api/GDCEmployeeInfo/GetOrganizations";        
            requestParam ="applicationId=FB2E5B93-AA4C-4531-9071-EE4AA42300DE&password=C@mputer1";
            //For Get
            var res = HttpGet<List<Organizations>>(apiBaseUri, requestPath, requestParam);

            //For Post
            //req = new ApiRequest
            //{
            //    applicationId = "FB2E5B93-AA4C-4531-9071-EE4AA42300DE",
            //    password = "C@mputer1"
            //};
            //var res = HttpPost<ApiRequest, List<Organizations>>(apiBaseUri, requestPath, req);
            Console.ReadKey();
        }
        protected static T HttpGet<T>(string apiBaseUri, string requestPath, string requestParam)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // setup client
                    client.BaseAddress = new Uri(apiBaseUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // make request                
                
                    var response = client.GetAsync(requestPath +"?"+ requestParam).Result;
                    var returnObject = response.Content.ReadAsAsync<T>().Result;              
                    return returnObject;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected static TR HttpPost<T, TR>(string apiBaseUri, string requestPath, T requestData, string token = null)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // setup client
                    client.BaseAddress = new Uri(apiBaseUri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    if (!string.IsNullOrEmpty(token))
                    {
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                    }

                    // make request
                    var json = JsonConvert.SerializeObject(requestData);
                    HttpContent content = new StringContent(json);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                    var response = client.PostAsync(requestPath, content).Result;
                    var returnObject = response.Content.ReadAsAsync<TR>().Result;
                    var json2 = JsonConvert.SerializeObject(returnObject); 

                    return returnObject;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public class ApiRequest
        {            
            public string applicationId { get; set; }
            public string password { get; set; }
        }

        public class MemberOrganizationList
        {
            public int memberOrganizationId { get; set; }
            public int memberId { get; set; }
            public int organizationId { get; set; }
            public int memberOrganizationTypeId { get; set; }
            public int memberOrganizationRelationTypeId { get; set; }
            public DateTime? createdDate { get; set; }
            public int createdBy { get; set; }
            public bool isDeleted { get; set; }
            public int objectState { get; set; }
            public List<string> modifiedProperties { get; set; }
            public string e_FullName { get; set; }
            public string positionName { get; set; }
            public string positionNumber { get; set; }
            public string compoundKey { get; set; }
        }

        public class CostCenterOrganizationList
        {
            public int costCenterOrganizationId { get; set; }
            public string companyCode { get; set; }
            public string costCenterCodeOld { get; set; }
            public string costCenterCode { get; set; }
            public string costCenterName { get; set; }
            public string departmentId { get; set; }
            public string organizationName { get; set; }
            public int organizationId { get; set; }
            public bool isPrimary { get; set; }
            public DateTime? createdDate { get; set; }
            public int createdBy { get; set; }
            public DateTime? updatedDate { get; set; }
            public int updatedBy { get; set; }
            public bool isDeleted { get; set; }
            public int objectState { get; set; }
            public List<string> modifiedProperties { get; set; }
            public int costCenterSAPId { get; set; }
        }

        public class OrganizationParentServiceList
        {
            public int organizationParentServiceId { get; set; }
            public int organizationId { get; set; }
            public int organizationParentId { get; set; }
            public int organizationParentServiceTypeId { get; set; }
            public DateTime? createdDate { get; set; }
            public int createdBy { get; set; }
            public bool isDeleted { get; set; }
            public int objectState { get; set; }
            public List<string> modifiedProperties { get; set; }
            public string organizationName_EN { get; set; }
            public string organizationLevelName { get; set; }
            public string departmentId { get; set; }
        }

        public class Organizations
        {
            public int organizationId { get; set; }
            public bool prefix_EN { get; set; }
            public string organizationName_EN { get; set; }
            public bool prefix_TH { get; set; }
            public string organizationName_TH { get; set; }
            public string description { get; set; }
            public string departmentId { get; set; }
            public string departmentParentId { get; set; }
            public string othInfo { get; set; }
            public int organizationTypeId { get; set; }
            public int organizationLevelId { get; set; }
            public string companyCode { get; set; }
            public string companyName { get; set; }
            public string costCenter_Dept { get; set; }
            public List<MemberOrganizationList> memberOrganizationList { get; set; }
            public List<CostCenterOrganizationList> costCenterOrganizationList { get; set; }
            public List<OrganizationParentServiceList> organizationParentServiceList { get; set; }
            public DateTime? createdDate { get; set; }
            public int createdBy { get; set; }
            public DateTime? updatedDate { get; set; }
            public int updatedBy { get; set; }
            public DateTime? expiredDate { get; set; }
            public DateTime? effectivedDate { get; set; }
            public string department_Ehr { get; set; }
            public string managerPosition { get; set; }
        }

    }
}
