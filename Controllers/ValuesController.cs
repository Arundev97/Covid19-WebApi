using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace arun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
        
            try
            {
                //string ConnectionString = @"server=localhost;userid=Arun;password=Arun@123;database=test";
                //using (var connection = new MySqlConnection(ConnectionString))
                //{
                //    connection.Open();
                //    var sql = "Insert into manual(name) values('Ram - Geek')";
                //    var cmd = new MySqlCommand(sql, connection);
                //    cmd.Prepare();
                //    cmd.ExecuteNonQuery();
                //    cmd.Connection.Close();
                //}

                return new string[] { "value1", "value2" };
            }
            catch (Exception e)
            {
                throw e;
            }
                   
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
