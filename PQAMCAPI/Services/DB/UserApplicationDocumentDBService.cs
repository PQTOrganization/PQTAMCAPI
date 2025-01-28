using Oracle.ManagedDataAccess.Client;
using AutoMapper;

using Helper;
using PQAMCAPI.Models;
using PQAMCAPI.Interfaces.Services;
using PQAMCClasses.DTOs;
using System.Data;
using API.Classes;

namespace PQAMCAPI.Services.DB
{
    public class UserApplicationDocumentDBService : IUserApplicationDocumentDBService
    {
        const string PACKAGE_NAME = "AMC_USER_APPLICATION_DOCUMENT_PKG"; 

        private readonly OracleConnection conn;
        private readonly IStoreProcedureService _spService;

        public UserApplicationDocumentDBService(IStoreProcedureService spService, 
                                                IConfiguration configuration, IMapper mapper)
        {
            String connStr = DBSettingsHelper.GetConnectionString(configuration);
            conn = new OracleConnection(connStr);

            _spService = spService;
        }

        public async Task<List<UserApplicationDocumentDTO>> GetAllUserApplicationDocuments(
            int UserApplicationId)
        {
            return await _spService.GetAllSP<UserApplicationDocumentDTO>(
                                PACKAGE_NAME + ".GET_DOCUMENTS_FOR_USER_APPLICATION", UserApplicationId);
        }

        public async Task<UserApplicationDocument> GetApplicationDocument(int UserApplicationDocumentId)
        {
            return await _spService.GetSP<UserApplicationDocument>(
                                PACKAGE_NAME + ".GET_USER_APPLICATION_DOCUMENT", UserApplicationDocumentId);
        }

        public async Task<UserApplicationDocument> InsertDocument(UserApplicationDocument Document)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".INSERT_USER_APPLICATION_DOCUMENT", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.Add("P_USER_APPLICATION_ID", OracleDbType.Int64).Value = 
                    Document.UserApplicationId;
            objCmd.Parameters.Add("P_DOCUMENT_ID", OracleDbType.Int64).Value = Document.DocumentId;
            objCmd.Parameters.Add("P_DOCUMENT", OracleDbType.Clob).Value = Document.Document;
            objCmd.Parameters.Add("P_DOC_TYPE", OracleDbType.Varchar2).Value = Document.DocType;
            objCmd.Parameters.Add("P_FILENAME", OracleDbType.Varchar2).Value = Document.Filename;
            objCmd.Parameters.Add("P_NEW_USER_APPLICATION_DOCUMENT_ID", OracleDbType.Int64, 
                    ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            // Best way to convert Int64 to int
            Document.UserApplicationDocumentId = 
                int.Parse(objCmd.Parameters["P_NEW_USER_APPLICATION_DOCUMENT_ID"].Value.ToString());

            await conn.CloseAsync();

            return Document;
        }

        public async Task<UserApplicationDocument> DeleteDocument(int ApplicationDocumentId)
        {
            var Document = await GetApplicationDocument(ApplicationDocumentId);

            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".DELETE_USER_APPLICATION_DOCUMENT", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.Add("P_APPLICATION_DOCUMENT_ID", OracleDbType.Int64).Value = 
                ApplicationDocumentId;
            objCmd.Parameters.Add("P_RECORD_COUNT", OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            var DeletedRecords  = int.Parse(objCmd.Parameters["P_RECORD_COUNT"].Value.ToString());

            await conn.CloseAsync();

            if (DeletedRecords == 0)
                throw new MyAPIException("No document found.");

            return Document;
        }

        public async Task<UserApplicationDocument> DeleteDocument(int UserApplicationId, int DocumentId)
        {
            await conn.OpenAsync();

            OracleCommand objCmd = new OracleCommand(PACKAGE_NAME + ".DELETE_DOCUMENT_FROM_APPLICATION", conn);
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.Add("P_USER_APPLICATION_ID", OracleDbType.Int64).Value = UserApplicationId;
            objCmd.Parameters.Add("P_DOCUMENT_ID", OracleDbType.Int64).Value = DocumentId;
            objCmd.Parameters.Add("P_RECORD_COUNT", OracleDbType.Int64, ParameterDirection.Output);

            await objCmd.ExecuteNonQueryAsync();

            var DeletedRecords = int.Parse(objCmd.Parameters["P_RECORD_COUNT"].Value.ToString());

            await conn.CloseAsync();

            if (DeletedRecords == 0)
                throw new MyAPIException("No document found.");

            return new UserApplicationDocument();
        }
    }
}
