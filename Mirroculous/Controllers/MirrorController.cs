using Microsoft.AspNetCore.Mvc;
using Mirroculous.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mirroculous.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MirrorController : ControllerBase
    {
        private const string ConnectionString = "Server=tcp:mirroculous.database.windows.net,1433;Initial Catalog=Mirroculous;Persist Security Info=False;User ID=mirroradmin;Password=Mirrorpassword1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        /// <summary>
        /// Connection String to Data Base
        /// </summary>
        ///

        //const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Mirroculous;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private List<Mirror> _dbList;

        /// <summary>
        /// GET: api/<ValuesController>
        /// </summary>
        /// <returns>return DB as list </returns>

        [HttpGet]
        public IEnumerable<Mirror> Get()
        {
            List<Mirror> DBList = new List<Mirror>();

            String selectAllDB = "Select * from Mirror";

            using (SqlConnection dataBaseConnection = new SqlConnection(ConnectionString))
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

        /// <summary>
        /// GET api/<ValuesController>/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "Routing does not exist!";
        }

        /// <summary>
        /// POST api/<ValuesController>
        /// </summary>
        /// <param name="mirror"></param>
        [HttpPost]
        public void Post([FromBody] Mirror value)
        {
            string insertSql =
                "insert into Mirror(temperature, humidity, dateTime) values( @temperature, @humidity, @dateTime)";

            using (SqlConnection dataBaseConnection = new SqlConnection(ConnectionString))
            {
                dataBaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertSql, dataBaseConnection))
                {
                   // insertCommand.Parameters.AddWithValue("@id", value.ID);
                    insertCommand.Parameters.AddWithValue("@temperature", value.Temperature);
                    insertCommand.Parameters.AddWithValue("@humidity", value.Humidity);
                    insertCommand.Parameters.AddWithValue("@dateTime", value.DateTime);

                    var rowsAffected = insertCommand.ExecuteNonQuery();
                    Console.WriteLine($"Rows affected: {rowsAffected}");
                }
            }
        }

        //// PUT api/<ValuesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        /// <summary>
        /// GET - By Date from DB
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns> selected date </returns>
        [HttpGet("bydate/{dateTime}", Name = "GetByDate")]
        public IActionResult GetDateTime(DateTime dateTime)
        {
            string sql = $"Select id, temperature, humidity, dateTime from Mirror Where dateTime = '{dateTime}'";

            var bydate1 = GetMirrorFromDb(sql);
            if (bydate1 != null)
            {
                return Ok(bydate1);
            }
            return NotFound(new { message = "Date not found!" });
        }

        /// <summary>
        /// Helper Method - GetMirrorFromDB(string sql)
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>

        public List<Mirror> GetMirrorFromDb(string sql)
        {
            List<Mirror> DBList = new List<Mirror>();
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
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


        /// <summary>
        /// getting from DB. this method is mostly for testing purpose 
        /// </summary>
        /// <returns></returns>
        public List<Mirror>  GetMirrorFromDB2()
        {
            String selectAllDB = "Select id, temperature, humidity, dateTime from Mirror";

            List<Mirror> DBList = new List<Mirror>();
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(selectAllDB, databaseConnection))
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

        private bool MirrorExists(long id)
        {
            return _dbList.Any(e => e.ID == id);
        }


        public MirrorController()
        {
        }
        //List<Mirror> productMirror = new List<Mirror>();

        //public MirrorController(List<Mirror> productMirror)
        //{
        //    this.productMirror = productMirror;
        //}

    }
}
