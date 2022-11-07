using Microsoft.AspNetCore.Mvc;


namespace Mock_Api_Endpoint.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ThreadingTestController
    {
        [HttpGet(Name = "TTest")]
        public string TTest(string pathParam)
        {
            Console.WriteLine("Log: " + pathParam);
            return "Nice :-) " + pathParam;
        }
    }
}
