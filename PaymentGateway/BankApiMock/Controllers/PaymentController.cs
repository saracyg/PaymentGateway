using Microsoft.AspNetCore.Mvc;

namespace BankApiMock.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        [HttpPost]
        public int Post()
        {
            return 1;
        }
    }
}
