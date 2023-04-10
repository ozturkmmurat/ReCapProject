using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;

namespace Core.Utilities.ExchangeRate.CurrencyGet
{
    public static class CurrencyGet
    {

        public static decimal GetUSD()
        {
            try
            {
                string today = "https://www.tcmb.gov.tr/kurlar/today.xml";
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(today);
                string USD = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/ForexBuying").InnerXml.Replace('.', ',');
                decimal USDd = Math.Round(decimal.Parse(USD), 2);
                return USDd;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static decimal GetEUR()
        {
            try
            {
                string today = "https://www.tcmb.gov.tr/kurlar/today.xml";
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(today);
                string EUR = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/ForexBuying").InnerXml.Replace('.', ',');
                decimal EURd = Math.Round(decimal.Parse(EUR), 2);
                return EURd;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}

