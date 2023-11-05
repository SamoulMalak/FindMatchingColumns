using FindMatchingColumns.BL.IServices;
using FindMatchingColumns.Data.Entities;
using FindMatchingColumns.Data.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindMatchingColumns.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchingColumnsController : ControllerBase
    {
        private readonly IPolicyReopsitory repo;
        private readonly IMatchingServices services;
        
        public MatchingColumnsController(IPolicyReopsitory repo,
                                         IMatchingServices services)
        {
            this.repo = repo;
            this.services = services;
        }
        [HttpGet("{Id:int}")]
        public IActionResult GetDtoFromDataBase(int Id)
        {
            var result = repo.GetPolicyById(Id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult GetDocumentForDTO([FromBody]Policy dto,string Filepath)
        {
            List<string> tables = new List<string>();
            services.FilePath = Filepath;
            tables.AddRange(new string[] { "TR_Beneficiary", "TR_BenefitTitle", "TR_Currency", "TR_CurrencyRat", "TR_Users", "TR_Product", "TR_Tariff", "TR_Zone", "TR_UsersProduct", "TR_Plan" });
            services.GetPropertyValueInTable<Policy>(dto, tables);
            return Ok();
        }
    }
}
