using System.IO;

namespace BalanceReconciliation.Test.Util
{
    public class Helper 
    {
        public Helper()
        {
            
        }
        public string loadJson() 
        {
            //BalanceReconciliation.Test/Resources/mock.json
            using (StreamReader r = new StreamReader("Resources/mock.json"))
            {
                return r.ReadToEnd();
            }
        }
    }
}