using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApi
{
    class Users
    {
        public Users(string phoneNo, string password, string lan1, string lan2, string lat1, string lat2)
        {
            PhoneNo = phoneNo;
            Password = password;
            Lan1 = lan1;
            Lan2 = lan2;
            Lat1 = lat1;
            Lat2 = lat2;


        }

        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public string Lan1 { get; set; }
        public string Lan2 { get; set; }
        public string Lat1 { get; set; }
        public string Lat2 { get; set; }
    }

    public class Response
    {
        public string AccessToken { get; set; }
        public string Status { get; set; }
        public string RequestId { get; set; }
        //public string PhoneNo { get; set; }
        //public string Password { get; set; }
        public Error Error { get; set; }

    }

    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
   }

    class Program
    {
        static void Main(string[] args)
        {

            //var user = new Users();

            //user.Username = "9975960644";
            //user.Password = "cci12345";
            var url = "http://auth.bungii.ccigoa:8189/";

            IEnumerable<Users> result = File.ReadAllLines(@"C:\Users\prathamesh.mashelkar\Downloads\CustomerUsers.csv")
          .Select(y => y.Split(','))
                      .Select(x => new {
                          PhoneNo = x[0],
                          password = x[1],
                          lan1 = x[2],
                          lan2 = x[3],
                          lat1 = x[4],
                          lat2 = x[5],
                      }).Select(x => new Users(x.PhoneNo, x.password, x.lan1, x.lan2, x.lat1, x.lat2))
                         .ToList();

            foreach (var o in result)
            {
                PostData(o, url);
                //Console.WriteLine(o);
            }
            Console.WriteLine("...........Finished...........");
            Console.ReadKey();
        

       

            
        }

        private static void PostData(Users user,string url)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(url);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", "Zjg5ZTY2ZDQtZjhmMi00NDhhLWFmYTUtYjdjMDk0OTNmMTk2OmRjZGFmNDFiLWFmMzctNGRhYy05Y2ZlLThkNTUyMzZmYmY1Zg==");

            var response = client.PostAsJsonAsync("api/customer/login", user).Result;

            if (response.IsSuccessStatusCode)
            {
                var respon = response.Content.ReadAsAsync<Response>().Result;
                Console.WriteLine("Error Code" +
               response.StatusCode + " : Message - " + response.ReasonPhrase + "|| " + user.PhoneNo + "|| " + "|| " + user.Password + "|| "+respon.Status);
                //usergrid.ItemsSource = users;
            }
            else
            {
                var respon = response.Content.ReadAsAsync<Response>().Result;
                //Console.WriteLine("Error Code" +
                //response.StatusCode + " : Message - " + response.ReasonPhrase + " " + respon.Status + " " + respon.Error.Code + " " + respon.Error.Message);
               
            }
        }
    }
}
