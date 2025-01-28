namespace PQAMCClasses.CloudDTOs
{
    public class SubmitConversionRequestDTO
    {
        public string? schemeOption { get; set; }
        public string? folioNo { get; set; }
        public string? fromPlanFundId { get; set; }
        public string? fromUnitTypeClass { get; set; }
        public string? conversionInTermsOf { get; set; }
        public string? allUnits { get; set; }
        public string? conversionTermValue { get; set; }
        public string? toPlanFundId { get; set; }
        public string? formReceivingDateTime { get; set; }
    }    
}
