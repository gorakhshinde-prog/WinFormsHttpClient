using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace WinFormsHttpClient
{
    public partial class Form1 : Form
    {
        LoginResponse? _loginResponse;
        public Form1()
        {
            InitializeComponent();
            button2.Enabled = true;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                HttpClient client = new HttpClient();

                // Put the following code where you want to initialize the class
                // It can be the static constructor or a one-time initializer
                client.BaseAddress = new Uri("http://localhost:5254/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Assuming http://localhost:5254/api/ as BaseAddress 
                var login = new Login() { Email = "admin@example.com", Password = "admin123" };
                var response = await client.PostAsJsonAsync("account/login", login);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content is not null)
                    {
                        _loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                        if (_loginResponse != null)
                        {
                            button2.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            Cursor = Cursors.Default;
        }

        private async void GetData()
        {
            try
            {
                if (_loginResponse == null) return;
                HttpClient client = new HttpClient();

                // Put the following code where you want to initialize the class
                // It can be the static constructor or a one-time initializer
                client.BaseAddress = new Uri("http://localhost:5254/");
                client.DefaultRequestHeaders.Accept.Clear();
                //  client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _loginResponse.Token);
                //var result = await client.GetFromJsonAsync<WeatherForecast[]>("api/weatherforecast/weatherf");
                //var res = await client.GetStringAsync("api/Account/test");
                // StringContent httpContent = new StringContent(@"{ ""userName"": ""Sam"", ""password"": """" }", Encoding.UTF8, "application/json");
                StringContent httpContent = new StringContent(@"{ ""username"" : ""data"" }", Encoding.UTF8, "application/json");
                //var response = await client.GetStringAsync("api/weatherforecast/stringdata"); // ok
                var response = await client.PostAsync("api/weatherforecast/stringdata", httpContent);
                var data = await response.Content.ReadAsStringAsync();
                //if (result != null)
                //{
                //foreach (var weather in result)
                //{

                //}

                //}
            }
            catch (Exception ex)
            {

            }
        }
        private async void GetStringData()
        {
            try
            {
                //if (_loginResponse == null) return;
                HttpClient client = new HttpClient();

                // Put the following code where you want to initialize the class
                // It can be the static constructor or a one-time initializer
                client.BaseAddress = new Uri("http://localhost:5254/");
                client.DefaultRequestHeaders.Accept.Clear();
                StringContent httpContent = new StringContent(JsonConvert.SerializeObject("this is the test"), Encoding.UTF8, "application/json");
                var response = await client.GetStringAsync("api/weatherforecast/stringdata"); // ok
                                                                                              // var response = await client.PostAsync("api/weatherforecast/stringdata", httpContent);
                                                                                              // var data = await response.Content.ReadAsStringAsync();
                if (response != null)
                {
                    //foreach (var weather in result)
                    //{

                    //}
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void PostData()
        {
            try
            {
                if (_loginResponse == null) return;
                HttpClient client = new HttpClient();

                // Put the following code where you want to initialize the class
                // It can be the static constructor or a one-time initializer
                client.BaseAddress = new Uri("http://localhost:5254/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _loginResponse.Token);
                //var result = await client.GetFromJsonAsync<WeatherForecast[]>("api/weatherforecast/weatherf");
                //var res = await client.GetStringAsync("api/Account/test");
                //StringContent httpContent = new StringContent(JsonConvert.SerializeObject("this is test message"), Encoding.UTF8, "application/json");
                //var response = await client.PostAsync("api/weatherforecast/stringdata", httpContent);
                //var data = await response.Content.ReadAsStringAsync();
                // var dd=JsonConvert.DeserializeObject<string>(data);
                var res2 = await client.PostAsJsonAsync("api/weatherforecast/complexmethod", new Jwt() { Key = "sgm" });
                if (res2 != null && res2.IsSuccessStatusCode)
                {

                }

            }
            catch (Exception ex)
            {

            }
        }
        private async void PostStringData()
        {
            try
            {
                if (_loginResponse == null) return;
                HttpClient client = new HttpClient();

                // Put the following code where you want to initialize the class
                // It can be the static constructor or a one-time initializer
                client.BaseAddress = new Uri("http://localhost:5254/");
                client.DefaultRequestHeaders.Accept.Clear();
                // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _loginResponse.Token);
                //var result = await client.GetFromJsonAsync<WeatherForecast[]>("api/weatherforecast/weatherf");
                //var res = await client.GetStringAsync("api/Account/test");
                StringContent httpContent = new StringContent(JsonConvert.SerializeObject("this is test message"), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("api/weatherforecast/stringdata", httpContent);
                var data = await response.Content.ReadAsStringAsync();
                // var dd=JsonConvert.DeserializeObject<string>(data);
                var res2 = string.Empty;// await client.PostAsJsonAsync("api/weatherforecast/complexmethod", new Jwt() { Key = "sgm" });
                                        // if (res2 != null && res2.IsSuccessStatusCode)
                {

                }

            }
            catch (Exception ex)
            {

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //SendPost("sgm");
            //GetData();
            //PostData();
            //PostStringData();
            GetStringData();
        }

        private async void SendPost(string data)
        {
            try
            {
                string url = "http://localhost:5254/api/weatherforecast/stringdata/";
                HttpClient client = new HttpClient();

                var jsonMessage = JsonConvert.SerializeObject(data);

                //Console.WriteLine(jsonMessage);

                // Serialize it again so that the server will treat it as a string:
                //jsonMessage = JsonConvert.SerializeObject(jsonMessage);
                //Console.WriteLine(jsonMessage);

                var response = await client.PostAsync(url, new StringContent(jsonMessage, Encoding.UTF8, "application/json"));
                var rd = await response.Content.ReadAsStringAsync();
                Console.WriteLine(response);
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }

    public class Login
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
    public record LoginResponse(bool Flag, string Message = null!, string Token = null!, string RefreshToken = null!);

    public class Jwt
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }

}
