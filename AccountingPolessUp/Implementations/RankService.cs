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
    public class RankService
    {
        private readonly WebClient _webClient;
        public RankService()
        {
            _webClient = new WebClient
            {
                BaseAddress = "https://localhost:5001/",
                Headers = { ["Authorization"] = "Bearer " + TokenManager.AccessToken }
            };
            _webClient.Encoding = System.Text.Encoding.UTF8;
        }

        public List<Rank> Get()
        {
            var json = _webClient.DownloadString("GetRank");
            var Info = JsonConvert.DeserializeObject<List<Rank>>(json);
            if (Info is null) throw new Exception("Ranks - null");
            else return Info;
        }

        public void Create(Rank model)
        {
            var reqparm = new NameValueCollection
            {
                ["RankName"] = $"{model.RankName}",
                ["Description"] = $"{model.Description}",
                ["OrganizationId"] = $"{model.OrganizationId}",
                ["MaxMmr"] = $"{model.MaxMmr}",
                ["MinMmr"] = $"{model.MinMmr}"
            };
            _webClient.UploadValues("CreateRank", "POST", reqparm);
        }

        public void Update(Rank model)
        {
            var reqparm = new NameValueCollection
            {
                ["id"] = $"{model.Id}",
                ["RankName"] = $"{model.RankName}",
                ["MaxMmr"] = $"{model.MaxMmr}",
                ["MinMmr"] = $"{model.MinMmr}",
                ["Description"] = $"{model.Description}",
                ["OrganizationId"] = $"{model.OrganizationId}"
            };
            _webClient.UploadValues("UpdateRank", "PUT", reqparm);
        }

        public void Delete(int id)
        {
            var reqparm = new NameValueCollection
            {
                ["id"] = $"{id}"
            };
            _webClient.UploadValues("DeleteRank", "DELETE", reqparm);
        }
    }
}