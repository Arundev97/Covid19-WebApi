using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using arun.Model;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using System.Text;
using System.Security.Cryptography;

namespace arun.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class NewController : ControllerBase
    {
        private readonly ILogger<NewController> logger;
        public NewController(ILogger<NewController> logger)
        {
            this.logger = logger;
        }

        // GET: api/New
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/New/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("UserTestFun")]
        public string UserTestFun([FromBody]Data input)
        {

            return "Success : " + input.Name;
        }

        static string computeSha256(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for(int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // POST: api/New
        [HttpPost("UserData")]
        public string UserData([FromBody] Data data)
        {
            try
            {
                logger.LogInformation("Entering in User Data");
                string ConnectionString = @"server=localhost;userid=arun;password=Arun@1234;database=covid19";
                var Password = data.Password;
                string HashedData = computeSha256(Password);

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("InsertUserData", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new MySqlParameter("@Name", data.Name));
                    command.Parameters.Add(new MySqlParameter("@Age", data.Age));
                    command.Parameters.Add(new MySqlParameter("@Gender", data.Gender));
                    command.Parameters.Add(new MySqlParameter("@MartialStatus", data.MartialStatus));
                    command.Parameters.Add(new MySqlParameter("@Occupation", data.Occupation));
                    command.Parameters.Add(new MySqlParameter("@NoOfChildrens", data.NoofChildrens));
                    command.Parameters.Add(new MySqlParameter("@Education", data.Education));
                    command.Parameters.Add(new MySqlParameter("@LivingStatus", data.LivingStatus));
                    command.Parameters.Add(new MySqlParameter("@Nationality", data.Nationality));
                    command.Parameters.Add(new MySqlParameter("@UserName", data.UserName));                    
                    command.Parameters.Add(new MySqlParameter("@Address", data.Address));
                    command.Parameters.Add(new MySqlParameter("@BloodGroup", data.BloodGroup));
                    command.Parameters.Add(new MySqlParameter("@FatherName", data.FatherName));
                    command.Parameters.Add(new MySqlParameter("@DateOfBirth", data.DateOfBirth));
                    command.Parameters.Add(new MySqlParameter("@MobileNumber", data.MobileNumber));
                    command.Parameters.Add(new MySqlParameter("@Email", MySqlDbType.VarChar));
                    command.Parameters["@Email"].Value = data.Email;
                    command.Parameters.Add(new MySqlParameter("@PassWord", MySqlDbType.VarChar));
                    command.Parameters["@PassWord"].Value = HashedData;
                    command.Parameters.Add(new MySqlParameter("@IsSystem", MySqlDbType.Int32));
                    command.Parameters["@IsSystem"].Value = data.IsSystem;
                    command.Prepare();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        command.Connection.Close();

                        return "Record Inserted Successfully";
                    }
                    else
                    {
                        command.Connection.Close();

                        return "Unable to Insert";
                    }

                }
            }
            catch (Exception ex)
            {
                logger.LogError($"The Path {ex.StackTrace} threw an exception {ex.Message}");

                return "Error in UserData Function : " + ex.Message;
            }
        }

        // PUT: api/New/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }

        [HttpPost("PositionData")]
        public string PositionData([FromBody] Location location)
        {
            try
            {

                logger.LogDebug("Entering in Location Data");
                string ConnectionString = @"server=localhost;userid=arun;password=Arun@1234;database=covid19";
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("InsertLocationData", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new MySqlParameter("@Latitude", MySqlDbType.VarChar));
                    command.Parameters["@Latitude"].Value = location.Latitude;
                    command.Parameters.Add(new MySqlParameter("@Longitude", MySqlDbType.VarChar));
                    command.Parameters["@Longitude"].Value = location.Longitude;
                    command.Parameters.Add(new MySqlParameter("@SlotId1", MySqlDbType.VarChar));
                    command.Parameters["@SlotId1"].Value = location.SlotId;
                    command.Prepare();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        command.Connection.Close();

                        return "Record Updated Successfully";
                    }
                    else
                    {
                        command.Connection.Close();

                        return "Unable to Update";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"The path {ex.StackTrace} threw an exception{ex.Message}");
                return "Error in position Function : " + ex.Message;
            }

        }

        [HttpPost("AuthenticateUser")]
        public List<Resutdata> AuthenticateUser([FromBody]Data data)
        {
            try
            {
                string ConnectionString = @"server=localhost;userid=arun;password=Arun@1234;database=covid19";
                var Password = data.Password;
                string HashedData = computeSha256(Password);
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("AuthenticateUser", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new MySqlParameter("@UserName1", MySqlDbType.VarChar));
                    command.Parameters["@UserName1"].Value = data.UserName;
                    command.Parameters.Add(new MySqlParameter("@Password1", MySqlDbType.VarChar));
                    command.Parameters["@Password1"].Value = HashedData;
                    command.Parameters.Add(new MySqlParameter("SlotId2", MySqlDbType.VarChar));
                    command.Parameters["@SlotId2"].Direction = System.Data.ParameterDirection.Output;
                    MySqlDataReader reader = command.ExecuteReader();
                    var datalist = new List<Resutdata>();
                    while (reader.Read())
                    {
                        var Status = reader["Status"].ToString();
                        if (Status == "1")
                        {
                            var model = new Resutdata
                            {
                                Status = reader["Status"].ToString(),
                                SlotId = reader["SlotId"].ToString(),
                                Name = reader["Name"].ToString(),
                                IsSystem = Convert.ToInt16(reader["IsSystem"].ToString())
                            };
                            datalist.Add(model);
                        }
                        else
                        {
                            var model = new Resutdata
                            {
                                Status = reader["Status"].ToString()
                            };
                            datalist.Add(model);

                        }
                    }

                    return datalist;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetUserData")]
        public List<Data> GetUserData()
        {
            try
            {
                logger.LogInformation("Entering in GetUserData");
                string ConnectionString = @"server=localhost;userid=arun;password=Arun@1234;database=covid19";
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("GetUserData", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    MySqlDataReader reader = command.ExecuteReader();
                    var datalist = new List<Data>();
                    while (reader.Read())
                    {
                        var model = new Data
                        {
                            Name = reader["Name"].ToString(),
                            Age = reader["Age"].ToString(),
                            PersonId = reader["PersonId"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            SlotID = reader["SlotID"].ToString(),
                            BloodGroup = reader["BloodGroup"].ToString(),
                            FatherName = reader["FatherName"].ToString(),
                            DateOfBirth = reader["DateOfBirth"].ToString(),
                            MobileNumber = reader["MobileNumber"].ToString(),
                            Email = reader["Email"].ToString(),
                            UserName = reader["UserName"].ToString()
                        };
                        datalist.Add(model);
                    }
                    return datalist;
                }
            }
            catch (Exception e)
            {
                logger.LogError($"The Path {e.StackTrace} threw an exception {e.Message}");
                throw e;
            }
            finally
            {
                logger.LogInformation("Existing in GetUserData");
            }
        }
        [HttpPost("GetUserDataByUserId")]
        public List<Data> GetUserDataByUserId([FromBody] Data data)
        {
            try
            {
                logger.LogInformation("Entering in GetUserData");
                string ConnectionString = @"server=localhost;userid=arun;password=Arun@1234;database=covid19";
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("GetUserDataByUserId", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new MySqlParameter("@slotid", MySqlDbType.VarChar));
                    command.Parameters["@slotid"].Value = data.SlotID;
                    MySqlDataReader reader = command.ExecuteReader();
                    var datalist = new List<Data>();
                    while (reader.Read())
                    {
                        var model = new Data
                        {
                            Name = reader["Name"].ToString(),
                            PersonId=reader["PersonId"].ToString(),
                            Latitude=reader["Latitude"].ToString(),
                            Longitude=reader["Longitude"].ToString(),
                            Email=reader["Email"].ToString(),
                            MobileNumber=reader["MobileNumber"].ToString(),
                            Address=reader["Address"].ToString(),
                            BloodGroup=reader["BloodGroup"].ToString(),
                            DateOfBirth=reader["DateOfBirth"].ToString()
                        };
                        datalist.Add(model);
                    }

                    return datalist;
                }

            }
            catch (Exception e)
            {
                logger.LogError($"The Path {e.StackTrace} threw an exception {e.Message}");
                throw e;

            }
            finally
            {
                logger.LogInformation("Existing in GetUserData");
            }
        }

        [HttpGet("GetLocationData")]
        public List<Data> GetLocationData()
        {
            try
            {
                logger.LogInformation("Entering in GetLocationData");
                string ConnectionString = @"server=localhost;userid=arun;password=Arun@1234;database=covid19";
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("GetLocationByUser", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    MySqlDataReader reader = command.ExecuteReader();
                    var datalist = new List<Data>();
                    while (reader.Read())
                    {
                        var model = new Data
                        {
                            Name = reader["Name"].ToString(),
                            Latitude=reader["Latitude"].ToString(),
                            Longitude=reader["Longitude"].ToString(),
                            Address=reader["Address"].ToString()
                        };
                        datalist.Add(model);
                    }
                    return datalist;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"The Path {ex.StackTrace} threw an exception {ex.Message}");
                throw ex;
            }
            finally
            {
                logger.LogInformation("Existing in GetLocationData");
            }
        }

        [HttpPost("ForgotPassword")]
        public List<Data>ForgotPassword(Data data)
        {
            try
            {
                logger.LogInformation("Entering in ForgotPassword");
                string ConnectionString = @"server=localhost;userid=arun;password=Arun@1234;database=covid19";
                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("Forgotpassword", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new MySqlParameter("@Email1", MySqlDbType.VarBinary));
                    command.Parameters["@Email1"].Value = data.Email;
                    MySqlDataReader dataReader = command.ExecuteReader();
                    var datalist = new List<Data>();
                    while (dataReader.Read())
                    {
                        var Status = dataReader["Status"].ToString();
                        if (Status == "1")
                        {
                            var model = new Data
                            {
                                Name = dataReader["Name"].ToString(),
                                PersonId = dataReader["PersonId"].ToString(),
                                Email = dataReader["Email"].ToString(),
                                Status=dataReader["Status"].ToString()
                            };
                            datalist.Add(model);
                        }
                        else
                        {
                            var model = new Data
                            {
                                Status = dataReader["Status"].ToString()
                            };
                            datalist.Add(model);
                        }                        
                    }
                    return datalist;
                }
            }
            catch(Exception ex)
            {
                logger.LogError($"The Path {ex.StackTrace} threw an exception {ex.Message}");
                throw ex;
            }
            finally
            {
                logger.LogInformation("Existing in ForgotPassword");
            }
        }
        [HttpPost("UpdatePassword")]
        public string UpdatePassword(Data data)
        {
            try
            {
                logger.LogInformation("Entering in UpdatePassword");
                string ConnectionString = @"server=localhost;userid=arun;password=Arun@1234;database=covid19";
                var Password = data.ChangePassword;
                string HashedData = computeSha256(Password);
                var Personid = data.PersonId;

                using (var connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("UpdatePassword", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new MySqlParameter("@PersonId1", MySqlDbType.Int32));
                    command.Parameters["@PersonId1"].Value = int.Parse(data.PersonId);
                    command.Parameters.Add(new MySqlParameter("@NewPassword", MySqlDbType.VarChar));
                    command.Parameters["@NewPassword"].Value = HashedData;
                    command.Prepare();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        command.Connection.Close();

                        return "Record Updated Successfully";
                    }
                    else
                    {
                        command.Connection.Close();

                        return "Unable to Update";
                    }

                }
            }
            catch (Exception ex)
            {
                logger.LogError($"The Path {ex.StackTrace} threw an exception {ex.Message}");
                return "Error in UpdatePassword Function : " + ex.Message;
            }
            finally
            {
                logger.LogInformation("Existing in UpdatePassword");
            }
        }
    }
}
