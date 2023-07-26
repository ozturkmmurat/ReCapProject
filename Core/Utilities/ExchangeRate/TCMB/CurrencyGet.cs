using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;

namespace Core.Utilities.ExchangeRate.CurrencyGet
{
    public static class CurrencyGet
    {

        public static decimal ForexBuyingCurrencyGet(string currencyName)
        {
            try
            {
                string today = "https://www.tcmb.gov.tr/kurlar/today.xml";
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(today);
                string currency = xmlDoc.SelectSingleNode($"Tarih_Date/Currency[@Kod='{currencyName.ToUpper()}']/ForexBuying").InnerXml.Replace('.', ',');
                decimal currencyd = Math.Round(decimal.Parse(currency), 2);
                return currencyd;
            }
            catch (Exception e)
            {
                 throw new Exception("Kur bilgisini çekerken bir sorun oluştu. " + e.Message);
            }
        }
    }
}

