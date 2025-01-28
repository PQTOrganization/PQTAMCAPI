using Microsoft.AspNetCore.Mvc;
using PQAMCAPI.Models;
using PQAMCClasses.DTOs;

namespace PQAMCAPI.Controller.Onboarding
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        public ListController()
        {            
        }


        [HttpGet("modesOfContribution")]
        public async Task<ActionResult<IEnumerable<ListDTO>>> GetModesOfContribution()
        {
            List<ListDTO> list = new List<ListDTO>
            {
                new ListDTO { Id = 1, Name = "Self" },
                new ListDTO { Id = 2, Name = "Employer/Third Party" }
            };
            return list;
        }

        [HttpGet("modesOfPayment")]
        public async Task<ActionResult<IEnumerable<ListDTO>>> GetModesOfPayment()
        {
            List<ListDTO> list = new List<ListDTO>
            {
                new ListDTO { Id = 1, Name = "Cheque" },
                new ListDTO { Id = 2, Name = "Demand Draft" },
                new ListDTO { Id = 3, Name = "Pay Order" },
                new ListDTO { Id = 5, Name = "Online" },
                new ListDTO { Id = 4, Name = "Other" }
            };
            return list;
        }

        [HttpGet("contributionFrequency")]
        public async Task<ActionResult<IEnumerable<ListDTO>>> GetContributionFrequency()
        {
            List<ListDTO> list = new List<ListDTO>
            {
                //new ListDTO { Id = 1, Name = "Monthly" },
                new ListDTO { Id = 2, Name = "Quarterly" },
                new ListDTO { Id = 3, Name = "Half-yearly" },
                //new ListDTO { Id = 4, Name = "Yearly" }
            };
            return list;
        }
    }
}
