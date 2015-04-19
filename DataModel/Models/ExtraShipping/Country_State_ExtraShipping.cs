using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.OrmLite;
using ServiceStack.DataAnnotations;
using PhotoBookmart.DataLayer.Models.System;

namespace PhotoBookmart.DataLayer.Models.ExtraShipping
{
    [Alias("Country_State_ExtraShipping")]
    [Schema("Products")]
    public class Country_State_ExtraShipping : BasicModelBase
    {
        [Default(0)]
        [ForeignKey(typeof(Country), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public long CountryId { get; set; }

        [Ignore]
        public string CountryName { get; set; }

        public string State { get; set; }

        public double Amount { get; set; }

        public Country_State_ExtraShipping() { }
    }
}