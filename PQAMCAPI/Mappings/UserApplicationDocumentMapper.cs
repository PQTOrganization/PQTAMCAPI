using AutoMapper;
using Oracle.ManagedDataAccess.Client;
using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace Mappings
{
    public class UserApplicationDocumentMapper : Profile
    {
        public UserApplicationDocumentMapper()
        {
            CreateMap<UserApplicationDocumentDTO, UserApplicationDocument>();
            CreateMap<UserApplicationDocument, UserApplicationDocumentDTO>();

            CreateMap<OracleDataReader, UserApplicationDocument>()
               .ForMember(dest => dest.UserApplicationDocumentId, 
                          src => src.MapFrom(x => x["USER_APPLICATION_DOCUMENT_ID"])
                )
               .ForMember(dest => dest.UserApplicationId, src =>
                          src.MapFrom(x => x["USER_APPLICATION_ID"])
               )
               .ForMember(dest => dest.Document, src => src.MapFrom(x => x["DOCUMENT"]))
               .ForMember(dest => dest.DocType, src => src.MapFrom(x => x["DOC_TYPE"]))
               .ForMember(dest => dest.Filename, src => src.MapFrom(x => x["FILENAME"]))
               .ForMember(dest => dest.CreatedDate, src => src.MapFrom(x => x["CREATED_DATE"]))
               .ForMember(dest => dest.ModifiedDate, src =>
               {
                   src.PreCondition(x => x["MODIFIED_DATE"] != DBNull.Value);
                   src.MapFrom(x => x["MODIFIED_DATE"]);
               });

            CreateMap<OracleDataReader, UserApplicationDocumentDTO>()
               .ForMember(dest => dest.DocumentId, src => src.MapFrom(x => x["DOCUMENT_ID"]))
               .ForMember(dest => dest.DocumentCode, src => src.MapFrom(x => x["DOCUMENT_CODE"]))
               .ForMember(dest => dest.ShortName, src => src.MapFrom(x => x["SHORT_NAME"]))
               .ForMember(dest => dest.LongName, src => src.MapFrom(x => x["LONG_NAME"]))
               .ForMember(dest => dest.IsMandatory, src => src.MapFrom(x => x["IS_MANDATORY"]))

               .ForMember(dest => dest.UserApplicationDocumentId, src =>
               {
                    src.PreCondition(x => x["USER_APPLICATION_DOCUMENT_ID"] != DBNull.Value);
                    src.MapFrom(x => x["USER_APPLICATION_DOCUMENT_ID"]);
               })
               .ForMember(dest => dest.UserApplicationId, src =>
               {
                   src.PreCondition(x => x["USER_APPLICATION_ID"] != DBNull.Value);
                   src.MapFrom(x => x["USER_APPLICATION_ID"]);
               })
               .ForMember(dest => dest.Document, src =>
               {
                   src.PreCondition(x => x["DOCUMENT"] != DBNull.Value);
                   src.MapFrom(x => x["DOCUMENT"]);
               })
               .ForMember(dest => dest.DocType, src =>
               {
                   src.PreCondition(x => x["DOC_TYPE"] != DBNull.Value);
                   src.MapFrom(x => x["DOC_TYPE"]);
               })
               .ForMember(dest => dest.Filename, src =>
               {
                   src.PreCondition(x => x["FILENAME"] != DBNull.Value);
                   src.MapFrom(x => x["FILENAME"]);
               })
               .ForMember(dest => dest.CreatedDate, src =>
               {
                   src.PreCondition(x => x["CREATED_DATE"] != DBNull.Value);
                   src.MapFrom(x => x["CREATED_DATE"]);
               })                      
               .ForMember(dest => dest.ModifiedDate, src =>
               {
                   src.PreCondition(x => x["MODIFIED_DATE"] != DBNull.Value);
                   src.MapFrom(x => x["MODIFIED_DATE"]);
               });
        }
    }
}