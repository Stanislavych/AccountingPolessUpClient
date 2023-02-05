﻿using AccountingPolessUp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AccountingPolessUp.Implementations
{
    public class ScheduleOfClassesService
    {
        private readonly WebClient _webClient;
        public ScheduleOfClassesService()
        {
            _webClient = new WebClient
            {
                BaseAddress = "https://localhost:7273/",
                Headers = { ["Authorization"] = "Bearer " + TokenManager.AccessToken }
            };
            _webClient.Encoding = System.Text.Encoding.UTF8;
        }

        public List<ScheduleOfСlasses> Get()
        {
            var json = _webClient.DownloadString("GetScheduleOfClasses");
            var Info = JsonConvert.DeserializeObject<List<ScheduleOfСlasses>>(json);
            if (Info is null) throw new Exception("info - null");
            else return Info;
        }

        public void Create(ScheduleOfСlasses model)
        {
            var reqparm = new NameValueCollection
            {
                ["Description"] = $"{model.Description}",
                ["DateStart"] = $"{model.DateStart}",
                ["DateEnd"] = $"{model.DateEnd}",
                ["WorkSpaceLink"] = $"{model.WorkSpaceLink}",
                ["TrainingCoursesId"] = $"{model.TrainingCoursesId}"
            };
            _webClient.UploadValues("CreateScheduleOfClasses", "POST", reqparm);
        }

        public void Update(ScheduleOfСlasses model)
        {
            var reqparm = new NameValueCollection
            {
                ["Id"] = $"{model.Id}",
                ["Description"] = $"{model.Description}",
                ["DateStart"] = $"{model.DateStart}",
                ["DateEnd"] = $"{model.DateEnd}",
                ["WorkSpaceLink"] = $"{model.WorkSpaceLink}",
                ["TrainingCoursesId"] = $"{model.TrainingCoursesId}"
            };
            _webClient.UploadValues("UpdateScheduleOfClasses", "PUT", reqparm);
        }

        public void Delete(int id)
        {
            var reqparm = new NameValueCollection
            {
                ["id"] = $"{id}"
            };
            _webClient.UploadValues("DeleteScheduleOfClasses", "DELETE", reqparm);
        }
    }
}
