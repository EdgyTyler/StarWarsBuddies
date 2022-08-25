
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

class Solution
{
    public static void Main(string[] args)
    {



        Root r = JsonConvert.DeserializeObject<Root>(GetSomething("https://swapi.dev/api/people").Result);

        while (r.next != null)
        { 


        List<string> Buddies = new List<string>();
        Dictionary<string, string> dictBuddies = new Dictionary<string, string>();

        foreach (Result ResultOb in r.results)

        {
            string Filmsss = string.Join(",", ResultOb.films.ToArray());

            if (dictBuddies.ContainsKey(Filmsss))
            {
                dictBuddies[Filmsss] = dictBuddies[Filmsss] + " , " + ResultOb.name;
            }
            else
            {

                dictBuddies.Add(Filmsss, ResultOb.name);
            }

        }

        foreach (KeyValuePair<string, string> b in dictBuddies)
        {

            if (b.Value.Split(",").Length > 1)
            {
                Console.WriteLine(b.Value);
            }

        }

             r = JsonConvert.DeserializeObject<Root>(GetSomething(r.next).Result);

        }

    }


    static async Task<string>  GetSomething(string url)
    {

        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);
 
        return  await response.Content.ReadAsStringAsync(); ;
    }

    public class Result
    {
        public string name;
        public List<string> films;
    }

    public class Root
    {
        public string next;
        public List<Result> results;
    }



}

