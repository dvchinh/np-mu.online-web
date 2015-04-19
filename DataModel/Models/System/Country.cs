using System;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.DataAnnotations;

namespace PhotoBookmart.DataLayer.Models.System
{
    [Schema("CMS")]
    public partial class Country : BasicModelBase
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string CurrencyCode { get; set; }

        /// <summary>
        /// To convert the exchange rate from the default curency base price
        /// </summary>
        [Default(1)]
        public double ExchangeRate { get; set; }

        /// <summary>
        /// The 3 letters currency for this country, from paypal
        /// </summary>
        public string Currency3Letter { get; set; }

        public bool Status { get; set; }

        /// <summary>
        /// Domain name that belongs to this country
        /// </summary>
        public List<string> Domains { get; set; }

        public string PhoneNumberPrefix { get; set; }

        public Country()
        {
            Name = ""; Code = "";
            Domains = new List<string>();
        }
    }
}
