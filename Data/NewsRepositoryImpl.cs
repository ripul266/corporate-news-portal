using CorporateNewsPortal.Models;
using CorporateNewsPortal.Profiles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateNewsPortal.Data
{
    public class NewsRepositoryImpl : INewsRepository
    {
        SqlConnection conn = null;
        public DbHandler dbHandler = new DbHandler();
        List<News> newsList = new List<News>
        {
            new News{NewsId=1,Title="News1",Description="this is description1"},
            new News{NewsId=2,Title="News2",Description="this is description2"},
            new News{NewsId=3,Title="News3",Description="this is description3"},
            new News{NewsId=4,Title="News4",Description="this is description4"},
            new News{NewsId=5,Title="News5",Description="this is description5"},

        };
        public Task<bool> ApproveNews(News edt)
        {
            int result = 0;
            string query = "update News set approval=@approval where newsId=@id";
            try
            {
                conn = dbHandler.OpenConnection();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", edt.NewsId);
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
                if (edt.Approval)
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
        public Task<bool> CreateNews(News eNews)
        {
            return Task.Run(() => {
                if (eNews != null)
                {
                    try
                    {
                        conn = dbHandler.OpenConnection();
                        // 3.Create the command object and pass the query
                        // IN c# query is string but with ADO.net it is responsibilty of ADO to execute the query
                        // productName in varchar so in single commas ''
                        string query = $"insert into News values({eNews.NewsId},'{eNews.Title}','{eNews.Description}','false','{eNews.Date}')";
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
                    newsList.Add(eNews);
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }
  
        public Task<bool> CreateNewsAdmin(News eNews)
        {
            return Task.Run(() => {
                if (eNews != null)
                {
                    try
                    {
                        conn = dbHandler.OpenConnection();
                        // 3.Create the command object and pass the query
                        // IN c# query is string but with ADO.net it is responsibilty of ADO to execute the query
                        // productName in varchar so in single commas ''
                        string query = $"insert into News values({eNews.NewsId},'{eNews.Title}','{eNews.Description}','true','{eNews.Date}')";
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
                    newsList.Add(eNews);
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }

        public Task<bool> DeleteNews(News eNews)
        {
            try
            {
                int id = eNews.NewsId;
                conn = dbHandler.OpenConnection();
                string query = "delete from News where NewsId=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", eNews.NewsId);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                    return Task.Run(() => true);
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
            bool isRemoved = newsList.Remove(eNews);
            return Task.Run(() => isRemoved);
        }

       

        public Task<News> FindEmployeeNewsById(int NewsId)
        {

            News pdt = null;
            try
            {

                conn = dbHandler.OpenConnection();
                // for parameter we use '@'
                string query = "select * from News where newsId=@id";
                using (conn)
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    // One method to attach the parameter to the query
                    // SqlParameter p1= new SqlParameter()
                    //Other method
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", NewsId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        //reader.Read() return bool if it can read return true enter while 
                        pdt = new News();
                        while (reader.Read())
                        {
                            pdt.NewsId = (int)reader["newsId"];
                            pdt.Title = (string)(reader["title"]);
                            pdt.Description = (string)(reader["description"]);
                            pdt.Approval = Convert.ToBoolean(reader["approval"]);
                            pdt.Date = Convert.ToDateTime(reader["date"]);
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


            return Task.Run(() => newsList.SingleOrDefault(x => x.NewsId == NewsId));
        }

        public Task<IEnumerable<News>> GetAllNews()
        {
            List<News> employeeNewsList = new List<News>();

            try
            {

                conn = dbHandler.OpenConnection();
                // for parameter we use '@'
                string query = "select * from News";
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
                            News pdt = new News();
                            pdt.NewsId = (int)reader["newsId"];
                            pdt.Title = (string)(reader["title"]);
                            pdt.Description = (string)(reader["description"]);
                            pdt.Approval = Convert.ToBoolean(reader["approval"]);
                            employeeNewsList.Add(pdt);
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
            return Task.FromResult<IEnumerable<News>>(employeeNewsList);
        }
        public Task<IEnumerable<News>> GetApprovedNews()
        {
            List<News> employeeNewsList = new List<News>();

            try
            {

                conn = dbHandler.OpenConnection();
                // for parameter we use '@'
                string query = "select * from News where approval=@approval";
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
                            News pdt = new News();
                            pdt.NewsId = (int)reader["newsId"];
                            pdt.Title = (string)(reader["title"]);
                            pdt.Description = (string)(reader["description"]);
                            pdt.Approval = Convert.ToBoolean(reader["approval"]);
                            employeeNewsList.Add(pdt);
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
            return Task.FromResult<IEnumerable<News>>(employeeNewsList);
        }
        public Task<IEnumerable<News>> GetNotApprovedNews()
        {
            List<News> employeeNewsList = new List<News>();

            try
            {

                conn = dbHandler.OpenConnection();
                // for parameter we use '@'
                string query = "select * from News where approval=@approval";
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
                            News pdt = new News();
                            pdt.NewsId = (int)reader["newsId"];
                            pdt.Title = (string)(reader["title"]);
                            pdt.Description = (string)(reader["description"]);
                            pdt.Approval = Convert.ToBoolean(reader["approval"]);
                            employeeNewsList.Add(pdt);
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
            return Task.FromResult<IEnumerable<News>>(employeeNewsList);
        }
    }
}

