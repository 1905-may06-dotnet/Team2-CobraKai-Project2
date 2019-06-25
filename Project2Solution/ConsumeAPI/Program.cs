using Project.Client.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ConsumeApi
{
    class Program
    {
        static void Main(string[] args)
        {
            using (HttpClient client = new HttpClient())
            {
                //3. create the URL that needs to used to access the resource
                client.BaseAddress = new Uri("https://localhost:44361/api/person");
                //4. call the controller which has action to provide resource
                var response = client.GetAsync("person");
                //5. Async tasks needed to be awaited
                response.Wait();
                //6. Get the result set
                var result = response.Result;
                //7. Check for the Status code if its ok
                if (result.IsSuccessStatusCode)
                {
                    Console.WriteLine("Successfully got the response");
                    //Deserialize the Json object to the data object
                    var data = result.Content.ReadAsAsync<IEnumerable<Person>>();
                    data.Wait();
                    //get deserialized results
                    var persons = data.Result;

                    //iterate over the results
                    foreach (var person in persons)
                    {
                        Console.WriteLine($"{person.Id} {person.Firstname} {person.Lastname}");
                    }
                }
            }
        }
    }
}