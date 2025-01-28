//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace KhaabachiAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SMSController : ControllerBase
//    {
//        private readonly ISMSService _service;

//        public SMSController(ISMSService Service)
//        {
//            _service = Service;
//        }

//        [HttpPost]
//        public async Task<IActionResult> SendTestMessage()
//        {
//            await _service.SendTestSMS();
//            return Ok("Message sent successfully");
//        }
//    }
//}
