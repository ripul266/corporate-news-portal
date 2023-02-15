using CorporateNewsPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateNewsPortal.Data
{
    public class EmployeeRepositoryImpl : IEmployeeRepository
    {

        SqlConnection conn = null;
        public DbHandler dbHandler=new DbHandler();

     /*   EmployeeRepositoryImpl(DbHandler dbHandler)
        {
            this.dbHandler = dbHandler;
        }*/
        List<Employee> employeeList = new List<Employee>
        {
            new Employee{EmployeeId=1,EmployeeName="Name1",Email="Name1@Gep.com",PhoneNumber="9999999999",Gender="Male",Password="Pass@123"},
            new Employee{EmployeeId=2,EmployeeName="Name2",Email="Name2@Gep.com",PhoneNumber="8888888888",Gender="Male",Password="Pass@1234"},
            new Employee{EmployeeId=3,EmployeeName="Name3",Email="Name3@Gep.com",PhoneNumber="7777777777",Gender="Female",Password="Pass@1235"},
            new Employee{EmployeeId=4,EmployeeName="Name4",Email="Name4@Gep.com",PhoneNumber="9898989898",Gender="Female",Password="Pass@12356"},
            new Employee{EmployeeId=5,EmployeeName="Name5",Email="Name5@Gep.com",PhoneNumber="9769769768",Gender="Female",Password="Pass@123321"},



        };








        public Task<bool> ApproveEmployee(Employee edt)
        {
            int result = 0;
            string query = "update Employee set approval=@approval where employeeId=@id";
            try
            {
                conn = dbHandler.OpenConnection();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", edt.EmployeeId);
                cmd.Parameters.AddWithValue("@approval", "true");
                result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Data Updated ");
                    return Task.Run(() => true);
                }
                else
                {
                    Console.WriteLine("Updation Failed");
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error While Updating  record ...." + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }


            return Task.Run(() => {
                if (edt.Approval )
                {
                    // Already Approved
                    return false;
                }
                else
                {
                    // Approval Done
                    edt.Approval = true;
                    return true;
                }
            });
        }

        public Task<bool> CreateEmployee(Employee edt)
        {
            return Task.Run(() => {

                if (edt != null)
                {
                    try
                    {
                        conn = dbHandler.OpenConnection();
                        // 3.Create the command object and pass the query
                        // IN c# query is string but with ADO.net it is responsibilty of ADO to execute the query
                        // productName in varchar so in single commas ''
                        string query = $"insert into Employee values({edt.EmployeeId},'{edt.EmployeeName}','{edt.PhoneNumber}','{edt.Gender}','{edt.Email}','{edt.Password}','false','employee')";
                        SqlCommand command = new SqlCommand(query, conn);
                        //4. Execute the Query
                        // if query is **insert,select,update** we use "Execute Non Query"
                        // res returns the number of rows modified
                        int res = command.ExecuteNonQuery();
                        //5.Check if it is successful
                        // Dont write WriteLine statements in execution
                        if (res > 0) Console.WriteLine("Record Inserted Succesfully");
                        else Console.WriteLine("Insertion Failed");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error While Inserting a record ...." + ex.Message);
                    }
                    finally
                    {
                        dbHandler.CloseConnection();
                    }
                    employeeList.Add(edt);
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }

        public Task<bool> CreateAdmin(Employee edt)
        {
            return Task.Run(() => {

                if (edt != null)
                {
                    try
                    {
                        conn = dbHandler.OpenConnection();
                        // 3.Create the command object and pass the query
                        // IN c# query is string but with ADO.net it is responsibilty of ADO to execute the query
                        // productName in varchar so in single commas ''
                        string query = $"insert into Employee values({edt.EmployeeId},'{edt.EmployeeName}','{edt.PhoneNumber}','{edt.Gender}','{edt.Email}','{edt.Password}','true','admin')";
                        SqlCommand command = new SqlCommand(query, conn);
                        //4. Execute the Query
                        // if query is **insert,select,update** we use "Execute Non Query"
                        // res returns the number of rows modified
                        int res = command.ExecuteNonQuery();
                        //5.Check if it is successful
                        // Dont write WriteLine statements in execution
                        if (res > 0) Console.WriteLine("Record Inserted Succesfully");
                        else Console.WriteLine("Insertion Failed");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error While Inserting a record ...." + ex.Message);
                    }
                    finally
                    {
                        dbHandler.CloseConnection();
                    }
                    employeeList.Add(edt);
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }


        public Task<bool> DeleteEmployee(Employee edt)
        {
            try
            {
                int id = edt.EmployeeId;
                conn = dbHandler.OpenConnection();
                string query = "delete from Employee where employeeId=@id and Role!=@role";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@role", "admin");
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                    return Task.Run(()=>true);
                Console.WriteLine("Deleted File");
            }
            catch (SqlException ex)
            {

                Console.WriteLine("Error While deleting record ...." + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }
            bool isRemoved = employeeList.Remove(edt);
            return Task.Run(() => isRemoved);
        }

        public Task<Employee> FindEmployeeById(int employeId)
        {
            Employee pdt = null;
            try
            {

                conn = dbHandler.OpenConnection();
                // for parameter we use '@'
                string query = "select * from Employee where employeeId=@id";
                using (conn)
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    // One method to attach the parameter to the query
                    // SqlParameter p1= new SqlParameter()
                    //Other method
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", employeId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        //reader.Read() return bool if it can read return true enter while 
                        pdt = new Employee();
                        while (reader.Read())
                        {
                            pdt.EmployeeId = (int)reader["employeeId"];
                            pdt.EmployeeName = (string)(reader["employeeName"]);
                            pdt.PhoneNumber = (string)(reader["phoneNumber"]);
                            pdt.Gender = (string)(reader["gender"]);
                            pdt.Email= (string)(reader["email"]);
                            pdt.Approval = Convert.ToBoolean(reader["approval"]);
                        }
                        
                        reader.Close();
                        return Task.Run(() => pdt);



                    }


                }
                //Execute the select query

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error While Finding record ...." + ex.Message);
            }
            return Task.Run(() => employeeList.SingleOrDefault(x => x.EmployeeId == employeId));
        }

        public Task<Employee> FindEmployeeByName(string employeeName)
        {
            Employee pdt = null;
            try
            {

                conn = dbHandler.OpenConnection();
                // for parameter we use '@'
                string query = "select * from Employee where employeeName=@name";
                using (conn)
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    // One method to attach the parameter to the query
                    // SqlParameter p1= new SqlParameter()
                    //Other method
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@name", employeeName);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        //reader.Read() return bool if it can read return true enter while 
                        pdt = new Employee();
                        while (reader.Read())
                        {
                            pdt.EmployeeId = (int)reader["employeeId"];
                            pdt.EmployeeName = (string)(reader["employeeName"]);
                            pdt.PhoneNumber = (string)(reader["phoneNumber"]);
                            pdt.Gender = (string)(reader["gender"]);
                            pdt.Email = (string)(reader["email"]);
                            pdt.Approval = Convert.ToBoolean(reader["approval"]);
                        }

                        reader.Close();
                        return Task.Run(() => pdt);



                    }


                }
                //Execute the select query

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error While Finding record ...." + ex.Message);
            }

            return Task.Run(() => employeeList.SingleOrDefault(x => x.EmployeeName == employeeName));
        }

        public Task<IEnumerable<Employee>> GetAllEmployees()
        {
            List<Employee> employeeList = new List<Employee>();

            try
            {

                conn = dbHandler.OpenConnection();
                // for parameter we use '@'
                string query = "select * from Employee ";
                using (conn)
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    // One method to attach the parameter to the query
                    // SqlParameter p1= new SqlParameter()
                    //Other method
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        //reader.Read() return bool if it can read return true enter while 

                        while (reader.Read())
                        {
                            Employee pdt = new Employee();
                            pdt.EmployeeId = (int)reader["employeeId"];
                            pdt.EmployeeName = (string)(reader["employeeName"]);
                            pdt.PhoneNumber = (string)(reader["phoneNumber"]);
                            pdt.Gender = (string)(reader["gender"]);
                            pdt.Email = (string)(reader["email"]);
                            pdt.Approval = Convert.ToBoolean(reader["approval"]);
                            employeeList.Add(pdt);
                        }

                        reader.Close();
                       /* return Task.FromResult<IEnumerable<Employee>>(employeeList);
*/

                    }


                }
                //Execute the select query

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error While Finding record ...." + ex.Message);
            }
            return Task.FromResult<IEnumerable<Employee>>(employeeList);
        }

        public Task<IEnumerable<Employee>> GetAllNotApprovedEmployees()
        {
            List<Employee> employeeList = new List<Employee>();

            try
            {

                conn = dbHandler.OpenConnection();
                // for parameter we use '@'
                string query = "select * from Employee where approval=@approval ";
                using (conn)
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    // One method to attach the parameter to the query
                    // SqlParameter p1= new SqlParameter()
                    //Other method
                    cmd.Parameters.AddWithValue("@approval", "false");
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        //reader.Read() return bool if it can read return true enter while 

                        while (reader.Read())
                        {
                            Employee pdt = new Employee();
                            pdt.EmployeeId = (int)reader["employeeId"];
                            pdt.EmployeeName = (string)(reader["employeeName"]);
                            pdt.PhoneNumber = (string)(reader["phoneNumber"]);
                            pdt.Gender = (string)(reader["gender"]);
                            pdt.Email = (string)(reader["email"]);
                            pdt.Approval = Convert.ToBoolean(reader["approval"]);
                            employeeList.Add(pdt);
                        }

                        reader.Close();
                        /* return Task.FromResult<IEnumerable<Employee>>(employeeList);
 */

                    }


                }
                //Execute the select query

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error While Finding record ...." + ex.Message);
            }
            return Task.FromResult<IEnumerable<Employee>>(employeeList);
        }
        public Task<IEnumerable<Employee>> GetAllApprovedEmployees()
        {
            List<Employee> employeeList = new List<Employee>();

            try
            {

                conn = dbHandler.OpenConnection();
                // for parameter we use '@'
                string query = "select * from Employee where approval=@approval ";
                using (conn)
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    // One method to attach the parameter to the query
                    // SqlParameter p1= new SqlParameter()
                    //Other method
                    cmd.Parameters.AddWithValue("@approval", "true");
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        //reader.Read() return bool if it can read return true enter while 

                        while (reader.Read())
                        {
                            Employee pdt = new Employee();
                            pdt.EmployeeId = (int)reader["employeeId"];
                            pdt.EmployeeName = (string)(reader["employeeName"]);
                            pdt.PhoneNumber = (string)(reader["phoneNumber"]);
                            pdt.Gender = (string)(reader["gender"]);
                            pdt.Email = (string)(reader["email"]);
                            pdt.Approval = Convert.ToBoolean(reader["approval"]);
                            employeeList.Add(pdt);
                        }

                        reader.Close();
                        /* return Task.FromResult<IEnumerable<Employee>>(employeeList);
 */

                    }


                }
                //Execute the select query

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error While Finding record ...." + ex.Message);
            }
            return Task.FromResult<IEnumerable<Employee>>(employeeList);
        }

        public Task<bool> UpdateEmployee(Employee edt)
        {
            int result = 0;
            string query = "update Employee set phoneNumber=@phoneNumber,gender=@gender,password=@password where employeeId=@id and approval=@approval";
            try
            {
                conn = dbHandler.OpenConnection();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", edt.EmployeeId);
                cmd.Parameters.AddWithValue("@phoneNumber", edt.PhoneNumber);
                cmd.Parameters.AddWithValue("@gender", edt.Gender);
                cmd.Parameters.AddWithValue("@approval", true);
                cmd.Parameters.AddWithValue("@password", edt.Password);
                result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    Console.WriteLine("Data Updated ");
                    return Task.Run(() => true);
                }
                else
                {
                    Console.WriteLine("Updation Failed");
                    return Task.Run(() => false);
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error While Updating  record ...." + ex.Message);
            }
            finally
            {
                dbHandler.CloseConnection();
            }
            return Task.Run(() => false);

        }
    }
}
