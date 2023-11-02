
namespace FindMatchingColumns.Data.Entities
{
    public class Policy
    {
        public int PolicyDetailID { get; set; }
        public int PolicyID { get; set; }
        public int InsuredId { get; set; }
        public string? Insured { get; set; }
        public string? FullName { get; set; }
        public string? Plan { get; set; }
        public bool Deductible { get; set; }
        public bool SportsActivities { get; set; }
        public decimal DeductiblePrice { get; set; }
        public decimal SportsActivitiesPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal PlanPrice { get; set; }
        public decimal FinalPrice { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PassportNo { get; set; }
        public string? Gender { get; set; }
        public int Tariff { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
