using System.Collections.Generic;
using WebBanHang.Data_Access_Layer;
using static WebBanHang.Data_Access_Layer.CodeDataAccess;

namespace WebBanHang.Business_Layer
{
    public class CodeBusinessLogic
    {
        private readonly CodeDataAccess codeDataAccess;

        public CodeBusinessLogic()
        {
            var databaseConnection = new DatabaseConnection();
            codeDataAccess = new CodeDataAccess(databaseConnection);
        }

        public bool IsCodeExists(string codeName)
        {
            return codeDataAccess.IsCodeExists(codeName);
        }

        public bool AddCode(string codeName, string codeMa, int codePrice)
        {
            return codeDataAccess.AddCode(codeName, codeMa, codePrice);
        }

        public bool EditCode(string codeName, int codeId, string codeMa, int codePrice)
        {
            return codeDataAccess.editCode(codeName, codeId, codeMa, codePrice);
        }


        public List<Code> GetCodes()
        {
            return codeDataAccess.GetCodes();
        }
    }
}
