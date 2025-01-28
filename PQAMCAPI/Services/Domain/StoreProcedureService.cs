using PQAMCAPI.Models;
using Helper;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using AutoMapper;
using PQAMCAPI.Interfaces.Services;
using API.Classes;

namespace PQAMCAPI.Services.Domain
{
    public class StoreProcedureService : IStoreProcedureService
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper _mapper;
        private readonly OracleConnection conn;

        public StoreProcedureService(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            string connStr = DBSettingsHelper.GetConnectionString(Configuration);
            conn = new OracleConnection(connStr);
            _mapper = mapper;
        }

        public async Task<T> InsertSP<T>(string spName, T movie)
        {
            /*await conn.OpenAsync();
            DataTable dt = new DataTable();
            OracleCommand objCmd = new OracleCommand(spName, conn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("Title", OracleDbType.NVarchar2).Value = movie.Title;
            objCmd.Parameters.Add("Genre", OracleDbType.NVarchar2).Value = movie.Genre;
            objCmd.Parameters.Add("ReleaseDate", OracleDbType.NVarchar2).Value = movie.ReleaseDate.ToString("yyyy-MMM-dd HH:mm:ss") ;
            OracleParameter returnCursor = new OracleParameter("Movie", OracleDbType.RefCursor, ParameterDirection.Output);
            objCmd.Parameters.Add(returnCursor);

            OracleDataReader dr = objCmd.ExecuteReader();
            */
            List<T> movies = new List<T>();
            /*while(dr.Read())
            {
                movies.Add(new Movie
                {
                    Id = dr.GetInt32(0),
                    Title = dr.GetString(1),
                    Genre = dr.GetString(2),
                    ReleaseDate = dr.GetDateTime(3),
                });
            }*/

            return movies[0];
        }

        public async Task<T> GetSP<T>(string spName, int Id)
        {
            await conn.OpenAsync();
            OracleCommand objCmd = new OracleCommand(spName, conn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("object_id", OracleDbType.Int32).Value = Id;
            OracleParameter returnCursor = new OracleParameter("recordset", OracleDbType.RefCursor,
                                                               ParameterDirection.Output);
            objCmd.Parameters.Add(returnCursor);

            OracleDataReader dr = objCmd.ExecuteReader();

            List<T> objectList = new List<T>();
            while (dr.Read())
            {
                objectList.Add(_mapper.Map<IDataReader, T>(dr));
            }
            if (objectList.Count > 0)
            {
                await conn.CloseAsync();
                return objectList[0];
            }
            else
            {
                throw new MyAPIException("Object Not Found");
            }
        }

        public async Task<T> GetSP<T>(string spName, string Key)
        {
            await conn.OpenAsync();
            OracleCommand objCmd = new OracleCommand(spName, conn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("object_key", OracleDbType.Varchar2).Value = Key;
            OracleParameter returnCursor = new OracleParameter("recordset", OracleDbType.RefCursor,
                                                               ParameterDirection.Output);
            objCmd.Parameters.Add(returnCursor);

            OracleDataReader dr = objCmd.ExecuteReader();

            List<T> objectList = new List<T>();
            while (dr.Read())
            {
                objectList.Add(_mapper.Map<IDataReader, T>(dr));
            }
            if (objectList.Count > 0)
            {
                await conn.CloseAsync();
                return objectList[0];
            }
            else
            {
                throw new MyAPIException("Object Not Found");
            }
        }

        public async Task<List<T>> GetAllSP<T>(string spName, string Id = "")
        {
            await conn.OpenAsync();
            OracleCommand objCmd = new OracleCommand(spName, conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            
            objCmd.Parameters.Add("object_id", OracleDbType.Varchar2).Value = Id;

            OracleParameter returnCursor = new OracleParameter("recordset", OracleDbType.RefCursor,
                                                               ParameterDirection.Output);
            objCmd.Parameters.Add(returnCursor);

            OracleDataReader dr = objCmd.ExecuteReader();

            List<T> objectList = new List<T>();
            while (dr.Read())
            {
                objectList.Add(_mapper.Map<IDataReader, T>(dr));
            }

            await conn.CloseAsync();
            return objectList;
        }

        public async Task<List<T>> GetAllSP<T>(string spName, int Id = -1)
        {
            await conn.OpenAsync();
            OracleCommand objCmd = new OracleCommand(spName, conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            if (Id > -1)
                objCmd.Parameters.Add("object_id", OracleDbType.Int32).Value = Id;

            OracleParameter returnCursor = new OracleParameter("recordset", OracleDbType.RefCursor,
                                                               ParameterDirection.Output);
            objCmd.Parameters.Add(returnCursor);

            OracleDataReader dr = objCmd.ExecuteReader();

            List<T> objectList = new List<T>();
            while (dr.Read())
            {
                objectList.Add(_mapper.Map<IDataReader, T>(dr));
            }

            await conn.CloseAsync();
            return objectList;
        }

        public async Task<int> DeleteSP<T>(string spName, int Id)
        {
            await conn.OpenAsync();
            OracleCommand objCmd = new OracleCommand(spName, conn);
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.Add("object_id", OracleDbType.Int32).Value = Id;
            OracleParameter recordCount = new OracleParameter("recordCount", OracleDbType.Int32,
                                                               ParameterDirection.Output);
            objCmd.Parameters.Add(recordCount);

            objCmd.ExecuteNonQuery();
            Console.WriteLine(objCmd.Parameters["recordCount"].Value.ToString());
            int count = int.Parse(objCmd.Parameters["recordCount"].Value.ToString());
            await conn.CloseAsync();
            return count;
        }
    }
}
