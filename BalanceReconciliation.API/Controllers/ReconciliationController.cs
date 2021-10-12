using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BalanceReconciliation.Service.Reconcile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BalanceReconciliation.API.Helpers;
using BalanceReconciliation.Service.Communication;

namespace BalanceReconciliation.API.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ReconciliationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IReconcile _reconcile;
        Validators check;

        public ReconciliationController(ILogger<ReconciliationController> logger, IReconcile reconcile)
        {
            _logger = logger;
            _reconcile = reconcile;
            check = new Validators();
        }

        [HttpPost]
        public async Task<IActionResult> Reconcile(IFormFile json)
        {
            try
            {
                if(!check.CheckIfJSonFile(json))
                {
                    var message = string.Join(" | ", ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage));
                    return StatusCode(StatusCodes.Status400BadRequest, new Service.Communication.ServiceResponse(message, ErrorCodes.Error));
                }
                string _json;
    
                using (var reader = new StreamReader(json.OpenReadStream()))
                {
                    _json = await reader.ReadToEndAsync();
                }

                var result = await _reconcile.performReconciliation(_json);
                if(result.Code == ErrorCodes.Success){
                    return Ok(result);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(
                    StatusCodes.Status500InternalServerError
                    , new ServiceResponse("Error occurred", ErrorCodes.Exception)
                );
            }
            
            
        }
    }
}
