using Core.Utilities.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logigcs)  // Params verdiğimiz zaman Iresult türünde istediğimiz kadar parametre verebiliyoruz
        {
            foreach (var logic in logigcs)
            {
                if (!logic.Success)
                {
                    return logic; //Hata oluşursa onu döndürür
                }
            }
            return null;
        }
    }
}
