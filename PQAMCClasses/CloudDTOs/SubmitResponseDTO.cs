namespace PQAMCClasses.CloudDTOs
{
    public class SubmitResponseDTO
    {       
        public string ResponseMessage { get; set; } = "";
        public string ResponseCode { get; set; } = "";
        public string ErrorDescription { get; set; }
        public string Response { get; set; }

    }
}
