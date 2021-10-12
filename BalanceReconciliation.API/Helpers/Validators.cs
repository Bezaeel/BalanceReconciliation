using Microsoft.AspNetCore.Http;

namespace BalanceReconciliation.API.Helpers
{
    public class Validators
    {
        public bool CheckIfJSonFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".json");
        }
    }
}