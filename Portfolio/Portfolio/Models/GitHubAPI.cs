using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Portfolio.Models
{
    public class GitHubAPI
    {
        public int id { get; set; }
        public string name { get; set; }
        public string html_url { get; set; }
        public string description { get; set; }
        public int stargazers_count { get; set; }

        public static List<GitHubAPI> GithubRepos()
        {
            RestClient client = new RestClient("https://api.github.com");
            RestRequest request = new RestRequest("search/repositories", Method.GET);
            request.AddParameter("q", "user:FrankNgo");
            request.AddParameter("sort", "stars");
            request.AddParameter("per_page", "3");
            request.AddHeader("User-Agent", "FrankNgo");
            RestResponse response = new RestResponse();

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();

            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            return JsonConvert.DeserializeObject<List<GitHubAPI>>(jsonResponse["items"].ToString());
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}