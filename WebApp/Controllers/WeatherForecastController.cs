using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
        {
            BasePath = "changewithyourbasepathurlfirebase"
          ,
            AuthSecret = "changewithyourauthsecretfirebase"
        };
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("getweather")]
        public IEnumerable<WeatherForecast> getweather()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("getpersonal")]
        public FirebaseForecast.About getpersonal()
        {
            IFirebaseClient client;
            var list = new FirebaseForecast.About();

            try
            {
                list.Sosmed = new List<FirebaseForecast.Socialmedia>();
                client = new FireSharp.FirebaseClient(config);
                FirebaseResponse responsedesc = client.Get("Desc/");
                dynamic datadesc = JsonConvert.DeserializeObject<dynamic>(responsedesc.Body);
                if (datadesc != null)
                {
                    foreach (var item in datadesc)
                    {
                        try
                        {
                            var convitem = JsonConvert.DeserializeObject<FirebaseForecast.Desc>(((JProperty)item).Value.ToString());
                            list.name = convitem.name;
                            list.moto = convitem.moto;
                            list.detail = convitem.detail;
                        }
                        catch
                        {

                        }
                    }
                }
                FirebaseResponse responserole = client.Get("Role/");
                dynamic datarole = JsonConvert.DeserializeObject<dynamic>(responserole.Body);
                if (datarole != null)
                {
                    string sroles = "";
                    foreach (var item in datarole)
                    {
                        try
                        {
                            var convitem = JsonConvert.DeserializeObject<FirebaseForecast.Role>(((JProperty)item).Value.ToString());
                            sroles += convitem.name + ", ";
                        }
                        catch
                        {

                        }

                    }
                    list.roles = sroles;
                }
                FirebaseResponse responseskill = client.Get("skill/");

                dynamic dataskill = JsonConvert.DeserializeObject<dynamic>(responseskill.Body);
                if (dataskill != null)
                {
                    var LSkil = new List<FirebaseForecast.Skill>();
                    foreach (var item in dataskill)
                    {
                        try
                        {
                            var convitem = JsonConvert.DeserializeObject<FirebaseForecast.Skill>(((JProperty)item).Value.ToString());
                            LSkil.Add(convitem);
                        }
                        catch
                        {

                        }

                    }
                    var groupbyskill = LSkil.GroupBy(pp => pp.type).ToList();
                    foreach (var item in groupbyskill)
                    {
                        foreach (var subitem in item)
                        {

                            if (item.Key == "Front End Programming")
                            {
                                list.skillfront += subitem.name + ", ";
                            }
                            else if (item.Key == "Back End Programming")
                            {
                                list.skillback += subitem.name + ", ";
                            }
                            else if (item.Key == "Mobile Programming")
                            {
                                list.skillmobile += subitem.name + ", ";
                            }
                            else if (item.Key == "Database SQL")
                            {
                                list.skillsql += subitem.name + ", ";

                            }
                            else if (item.Key == "Database NOSQL")
                            {
                                list.skillnosql += subitem.name + ", ";
                            }
                        }

                    }
                }
                FirebaseResponse responsesosmed = client.Get("Socialmedia/");
                dynamic datasosmed = JsonConvert.DeserializeObject<dynamic>(responsesosmed.Body);
                if (datasosmed != null)
                {
                    foreach (JProperty item in datasosmed)
                    {

                        try
                        {
                            JToken value = item.Value;
                            if (value.Type == JTokenType.Object)
                            {
                                var itemsosmed = new FirebaseForecast.Socialmedia();
                                itemsosmed.name = value.Value<String>("name");
                                itemsosmed.url = value.Value<String>("url");
                                itemsosmed.detail = value.Value<String>("detail");
                                list.Sosmed.Add(itemsosmed);
                            }

                        }
                        catch
                        {

                        }

                    }
                }

            }
            catch
            {

            }
            return list;
        }
        [HttpGet("getexperience")]
        public IEnumerable<FirebaseForecast.Project> getexperience()
        {
            IFirebaseClient client;
            var list = new List<FirebaseForecast.Project>();

            try
            {
                client = new FireSharp.FirebaseClient(config);
                FirebaseResponse responseexp = client.Get("experience/");
                dynamic dataexp = JsonConvert.DeserializeObject<dynamic>(responseexp.Body);
                if (dataexp != null)
                {
                    foreach (var item in dataexp)
                    {
                        try
                        {
                            var convitem = JsonConvert.DeserializeObject<FirebaseForecast.ProjectJson>(((JProperty)item).Value.ToString());
                            if (convitem != null)
                            {
                                string sdetail = "";

                                sdetail = convitem.Detail + Environment.NewLine;
                                sdetail += "Tech Stack : ";
                                if (convitem.FrontEnd != null)
                                {
                                    if (convitem.FrontEnd.Count > 0)
                                    {
                                        var listitem = new List<string>();

                                        foreach (string itemdata in convitem.FrontEnd)
                                        {
                                            string strdata = "";
                                            if (!string.IsNullOrEmpty(itemdata))
                                            {
                                                strdata = itemdata + ", ";
                                                sdetail += strdata;
                                            }
                                            if (!string.IsNullOrEmpty(strdata))
                                            {
                                                listitem.Add(strdata);
                                            }
                                        }
                                        convitem.FrontEnd.Clear();
                                        convitem.FrontEnd.AddRange(listitem);
                                    }
                                }
                                if (convitem.BackEnd != null)
                                {
                                    if (convitem.BackEnd.Count > 0)
                                    {
                                        var listitem = new List<string>();

                                        foreach (string itemdata in convitem.BackEnd)
                                        {
                                            string strdata = "";
                                            if (!string.IsNullOrEmpty(itemdata))
                                            {
                                                strdata = itemdata + ", ";
                                                sdetail += strdata;
                                            }
                                            if (!string.IsNullOrEmpty(strdata))
                                            {
                                                listitem.Add(strdata);
                                            }
                                        }
                                        convitem.BackEnd.Clear();
                                        convitem.BackEnd.AddRange(listitem);
                                    }
                                }
                                if (convitem.Database != null)
                                {
                                    if (convitem.Database.Count > 0)
                                    {
                                        var listitem = new List<string>();

                                        foreach (string itemdata in convitem.Database)
                                        {
                                            string strdata = "";
                                            if (!string.IsNullOrEmpty(itemdata))
                                            {
                                                strdata = itemdata + ", ";
                                                sdetail += strdata;
                                            }
                                            if (!string.IsNullOrEmpty(strdata))
                                            {
                                                listitem.Add(strdata);
                                            }
                                        }
                                        convitem.Database.Clear();
                                        convitem.Database.AddRange(listitem);
                                    }
                                }
                                var newdata = new FirebaseForecast.Project();
                                newdata.Name = convitem.Name;
                                newdata.Duration = convitem.Duration;
                                newdata.Type = convitem.Type;

                                newdata.Detail = sdetail;
                                newdata.Demo = new List<FirebaseForecast.Demo>();
                                newdata.Demo = convitem.Demo;
                                //Duration
                                //Type
                                //Detail
                                //Demo
                                list.Add(newdata);
                            }
                        }
                        catch
                        {

                        }

                    }
                }

            }
            catch
            {

            }
            return list.ToArray();
        }
        [HttpGet("getprojects")]
        public IEnumerable<FirebaseForecast.NewProject> getprojects()
        {
            IFirebaseClient client;
            var list = new List<FirebaseForecast.NewProject>();

            try
            {
                client = new FireSharp.FirebaseClient(config);
                FirebaseResponse responseexp = client.Get("project/");
                dynamic dataexp = JsonConvert.DeserializeObject<dynamic>(responseexp.Body);
                if (dataexp != null)
                {
                    foreach (var item in dataexp)
                    {
                        try
                        {
                            var convitem = JsonConvert.DeserializeObject<FirebaseForecast.NewProject>(((JProperty)item).Value.ToString());
                            if (convitem != null)
                            {

                                list.Add(convitem);
                            }
                        }
                        catch
                        {

                        }

                    }
                }

            }
            catch
            {

            }
            return list.ToArray();
        }
        [HttpGet("getprojectsbyname")]
        public FirebaseForecast.NewProject getprojectsbyname(string sname)
        {
            IFirebaseClient client;
            var list = new List<FirebaseForecast.NewProject>();
            var data = new FirebaseForecast.NewProject();
            try
            {
                client = new FireSharp.FirebaseClient(config);
                FirebaseResponse responseexp = client.Get("project/");
                dynamic dataexp = JsonConvert.DeserializeObject<dynamic>(responseexp.Body);
                if (dataexp != null)
                {
                    foreach (var item in dataexp)
                    {
                        try
                        {
                            var convitem = JsonConvert.DeserializeObject<FirebaseForecast.NewProject>(((JProperty)item).Value.ToString());
                            if (convitem != null)
                            {
                                if (convitem.SimpleName == sname)
                                {
                                    data = convitem;
                                }
                                //list.Add(convitem);
                            }
                        }
                        catch
                        {

                        }

                    }
                }

            }
            catch
            {

            }
            return data;
        }

    }
}