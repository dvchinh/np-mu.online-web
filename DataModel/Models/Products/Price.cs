using System;
using System.Linq;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.DataAnnotations;

namespace PhotoBookmart.DataLayer.Models.Products
{
    public enum Enum_Price_MasterType
    {
        ProductOption,
        ProductShippingPrice,
        Product,
    }

    /// <summary>
    /// Remember to delete the Price when you delete object for we dont know exactly which object connect to the price
    /// </summary>
    [Schema("Products")]
    public partial class Price : ModelBase
    {
        ///// <summary>
        ///// Price we display according to the country
        ///// </summary>
        //public double DisplayPrice { get; set; }
        ///// <summary>
        ///// Price we use to calculate, in RM
        ///// </summary>
        //public double RealPrice { get; set; }

        /// <summary>
        /// Price in this currency
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Vietnam, Malaysia,...
        /// https://developer.paypal.com/docs/classic/api/country_codes/
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// VND, đ, RM, USD, to show to customer
        /// </summary>
        public string CurrencyCode { get; set; }
        ///// <summary>
        ///// This is paypal code
        ///// https://developer.paypal.com/docs/classic/api/currency_codes/
        ///// </summary>
        //public string CurrencyCode_ThereLetters { get; set; }

        /// <summary>
        /// Id of the object which hold the link to this price
        /// </summary>
        public long MasterId { get; set; }

        /// <summary>
        /// Type of the object hold this price
        /// </summary>
        public Enum_Price_MasterType MasterType { get; set; }

        [Ignore]
        public string Master_Name { get; set; }

        public Price()
        {

        }
    }
}
