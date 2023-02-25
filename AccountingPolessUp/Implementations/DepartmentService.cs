﻿using AccountingPolessUp.Helpers;
using AccountingPolessUp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;

namespace AccountingPolessUp.Implementations
{
    internal class DepartmentService
    {
        private readonly WebClient _webClient;

        public DepartmentService()
        {
            _webClient = new WebClient
            {
                BaseAddress = "https://polessu.by/polessup/",
                Headers = { ["Authorization"] = "Bearer " + TokenManager.AccessToken }
            };
            _webClient.Encoding = System.Text.Encoding.UTF8;
        }

        public List<Department> Get()
        {
            var json = _webClient.DownloadString("GetDepartment");
            var Info = JsonConvert.DeserializeObject<List<Department>>(json);
            if (Info is null) throw new Exception("Departments - null");
            else return Info;
        }

        public void Create(Department model)
        {
            var reqparm = new NameValueCollection
            {
                ["FullName"] = $"{model.FullName}",
                ["Description"] = $"{model.Description}",
                ["DateStart"] = $"{model.DateStart}",
                ["DateEnd"] = $"{model.DateEnd}",
                ["DirectorId"] = $"{model.DirectorId}",
                ["Status"] = $"{model.Status}",
                ["OrganizationId"] = $"{model.OrganizationId}"

            };
            _webClient.UploadValues("CreateDepartment", "POST", reqparm);
        }

        public void Update(Department model)
        {
         
            var reqparm = new NameValueCollection
            {
                ["id"] = $"{model.Id}",
                ["FullName"] = $"{model.FullName}",
                ["Description"] = $"{model.Description}",
                ["DateStart"] = $"{model.DateStart}",
                ["DateEnd"] = $"{model.DateEnd}",
                ["DirectorId"] = $"{model.DirectorId}",
                ["Status"] = $"{model.Status}",
                ["OrganizationId"] = $"{model.OrganizationId}"
            };
            _webClient.UploadValues("UpdateDepartment", "PUT", reqparm);
        }

        public void Delete(int id)
        {
            var reqparm = new NameValueCollection
            {
                ["id"] = $"{id}"
            };
            _webClient.UploadValues("DeleteDepartment", "DELETE", reqparm);
        }
    }
}
