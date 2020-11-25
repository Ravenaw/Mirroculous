using Microsoft.AspNetCore.Mvc;
using Mirroculous.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mirroculous.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MirrorController : ControllerBase
    {
        const string localDBLink = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Mirroculous;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private const string connectionString = "Server=tcp:mirroculous.database.windows.net,1433;Initial Catalog=Mirroculous;Persist Security Info=False;User ID=mirroradmin;Password=Mirrorpassword1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Mirror> Get()
        {
            List<Mirror> DBList = new List<Mirror>();

            String selectAllDB = "Select id, temperature, humidity, dateTime from Mirror";

            using (SqlConnection dataBaseConnection = new SqlConnection(connectionString))
            {
                dataBaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectAllDB, dataBaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int temperature = reader.GetInt32(1);
                            int humidity = reader.GetInt32(2);
                            DateTime dateTime = reader.GetDateTime(3);

                            DBList.Add(new Mirror(id, temperature, humidity, dateTime));
                        }
                    }
                }
            }
            //return fakeDataList;
            return DBList;
        }


        // GET api/<ValuesController>/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "Not implemented! cuz we don't need it";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("bydate/{dateTime}", Name = "GetByDate")]
        public IEnumerable<Mirror> GetDateTime(DateTime dateTime)
        {
            string sql = $"Select id, temperature, humidity, dateTime from Mirror Where dateTime = '{dateTime}'";

            return GetMirrorFromDB(sql);
        }

        private List<Mirror> GetMirrorFromDB(string sql)
        {
            List<Mirror> DBList = new List<Mirror>();
            using (SqlConnection databaseConnection = new SqlConnection(connectionString))
            {
                databaseConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, databaseConnection))
                {
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id1 = reader.GetInt32(0);
                            int byTemperature = reader.GetInt32(1);
                            int byHumidity = reader.GetInt32(2);
                            DateTime byDateTime = reader.GetDateTime(3);

                            DBList.Add(new Mirror(id1, byTemperature, byHumidity, byDateTime));
                        }
                    }
                }
            }
            return DBList;
        }
    }
}
