using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PSTDotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<T> Query<T>(string query, params AdoDotNetParameter[]? parameters) 
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();  

            
            SqlCommand cmd = new SqlCommand(query, connection);

            if(parameters is not null && parameters.Length > 0)
            {
                var parametersArray = parameters.Select(p => new SqlParameter(p.Name, p.Value)).ToArray();
                cmd.Parameters.AddRange(parametersArray);
            }

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();


            string json = JsonConvert.SerializeObject(dt);
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!;

            return lst;
        }
        public T QueryFirstOrDefault<T>(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();


            SqlCommand cmd = new SqlCommand(query, connection);

            if (parameters is not null && parameters.Length > 0)
            {
                var parametersArray = parameters.Select(p => new SqlParameter(p.Name, p.Value)).ToArray();
                cmd.Parameters.AddRange(parametersArray);
            }

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);


            if (dt.Rows.Count == 0)
            {
                return default;
            }

            connection.Close();


            string json = JsonConvert.SerializeObject(dt); //C# to json
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!;  //json to c#

            return lst[0];
        }

        public int Execute(string query, params AdoDotNetParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();


            SqlCommand cmd = new SqlCommand(query, connection);

            if (parameters is not null && parameters.Length > 0)
            {
                cmd.Parameters.AddRange(parameters.Select(p => new SqlParameter(p.Name, p.Value)).ToArray());
            }

            var result = cmd.ExecuteNonQuery();


            connection.Close();
            return result;
        }
    }
    public class AdoDotNetParameter
    {
        public AdoDotNetParameter()
        {

        }
        public AdoDotNetParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public object Value { get; set; }

        
    }
}
