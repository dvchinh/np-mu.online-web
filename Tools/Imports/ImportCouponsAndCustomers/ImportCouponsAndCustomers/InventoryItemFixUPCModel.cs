using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace LifeSpringFixUPC
{
    class CouponModel
    {
        public long couponId { get; set; }

        public string couponname { get; set; }

        public string couponsecuritycode { get; set; }

        public double couponamount { get; set; }

        public string couponcurrency { get; set; }

        public double couponpercent { get; set; }

        public DateTime couponstartdate { get; set; }

        public DateTime couponenddate { get; set; }

        public int couponlimit { get; set; }

        public string couponcategories { get; set; }
        public string couponproducts { get; set; }

        public int couponusedcount { get; set; }

        public string couponlastuseddate { get; set; }

        public string couponcomment { get; set; }

        public string couponother { get; set; }

        public CouponModel()
        {

        }
    }

    [Alias("products")]
    public class AccessProducts
    {
        [AutoIncrement]
        [PrimaryKey]
        public long catalogid { get; set; }

        public int ccode { get; set; }
    }

    [Alias("customers")]
    public class AccessCustomers
    {
        [PrimaryKey]
        [AutoIncrement]
        public long contactid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string dob { get; set; }

        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }

        public string country { get; set; }
        public string phone { get; set; }
        public string email { get; set; }

        public string website { get; set; }
        public string password { get; set; }
    }

    #region Coupon Promo Model
    public enum Enum_CouponType
    {
        /// <summary>
        /// This type will only need the CouponCode, no need to check the Secuirty Code
        /// </summary>
        Monthly_PromoCode = 0,
        /// <summary>
        /// This time we need to check both Coupon Code and Secuirty code
        /// </summary>
        Groupon = 1
    }

    [Alias("Coupon_Promo_Code")]
    [Schema("Products")]
    public partial class CouponPromo 
    {

        [PrimaryKey]
        [AutoIncrement]
        public long Id { get; set; }

        /// <summary>
        /// Coupon Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// This field can be be add / update
        /// We only update this field incase this coupon has been used on the Front End. 
        /// Backend does not need to update
        /// Only groupon type need this field, for later we export this coupon code to Groupon to get money back
        /// </summary>
        public string SecurityCode { get; set; }

        public bool SecurityCodeRequired { get; set; }

        public DateTime IssueOn { get; set; }

        // To Groupon or to someone else
        public string IssueTo { get; set; }

        public int CouponType { get; set; }
        [Ignore]
        public Enum_CouponType CouponTypeEnum
        {
            get
            {
                return (Enum_CouponType)CouponType;
            }
            set
            {
                CouponType = (int)value;
            }
        }

        /// <summary>
        /// The begin date of the coupon
        /// </summary>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// the end date of the coupon
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// True: percent, false: fixed amount
        /// </summary>
        public bool isPercentDiscount { get; set; }

        /// <summary>
        /// True : to option
        /// False: to total product price except shipping
        /// </summary>
        public bool isApplyToOption { get; set; }

        /// <summary>
        /// Amount to discount (in RM or in percent)
        /// </summary>
        public double DiscountAmount { get; set; }

        /// <summary>
        /// Store the ID of all the options this coupon apply to
        /// </summary>
        public List<long> AppliedOptions { get; set; }

        /// <summary>
        /// Store the product id this coupon not apply to
        /// </summary>
        public List<long> ExceptProducts { get; set; }

        /// <summary>
        /// How many time maximum this coupon can be use
        /// </summary>
        public int MaxUse { get; set; }

        /// <summary>
        /// How many coupon has been used
        /// </summary>
        public int Used { get; set; }

        /// <summary>
        /// Which country this coupon will be apply to? 
        /// This field can be null, mean apply to all country.
        /// </summary>
        public string CountryCode { get; set; }

        public CouponPromo()
        {
            CountryCode = "";
        }
    }

    [Schema("CMS")]
    public partial class Country
    {
        public long Id { get; set; }

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

        public Country()
        {
            Name = ""; Code = "";
            Domains = new List<string>();
        }
    }

    [Alias("Products")]
    [Schema("Products")]
    public partial class Product
    {
        [PrimaryKey]
        [AutoIncrement]
        public long Id { get; set; }
        public bool Status { get; set; }

        public string Name { get; set; }

        public List<string> Tag { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public bool PriceDontShow { get; set; }

        public bool PriceCallForPrice { get; set; }

        public bool isFreeShip { get; set; }

        public bool ShowOnHomepage { get; set; }

        public string SeoName { get; set; }

        // thứ tự sắp xếp
        public int Order { get; set; }

        public string Size { get; set; }

        public int Pages { get; set; }

        public string Paper { get; set; }

        public string Orientation { get; set; }

        public long CatId { get; set; }
        [Ignore]
        public string Category_Name { get; set; }

        /// <summary>
        ///  This is the pID from the My Photo Creation. We follow them in order to capture the product 
        /// </summary>
        public int MyPhotoCreationId { get; set; }

        /// <summary>
        /// Weight of the product, in grams
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// If yes, then when customer submit order, they can select cover material
        /// </summary>
        public bool IsAllowCoverMaterialSelect { get; set; }


        public Product()
        {
            Id = 0;

            Order = 0;

            Tag = new List<string>();

            SeoName = "";

            Pages = 0;

        }
    }

    [Alias("Users")]
    [Schema("System")]
    public class ABUserAuth
    {
        public bool ActiveStatus { get; set; }

        public string Phone { get; set; }

        public string Addr { get; set; }

        public string States { get; set; }

        public string City { get; set; }

        public int GroupId { get; set; }

        public virtual DateTime? BirthDate { get; set; }
        public virtual string BirthDateRaw { get; set; }
        public virtual string Country { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual string Culture { get; set; }
        public virtual string DigestHa1Hash { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Email { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Gender { get; set; }
        [AutoIncrement]
        public virtual int Id { get; set; }
        public virtual string Language { get; set; }
        public virtual string LastName { get; set; }
        public virtual string MailAddress { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual string Nickname { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual List<string> Permissions { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string PrimaryEmail { get; set; }
        public virtual int? RefId { get; set; }
        public virtual string RefIdStr { get; set; }
        public virtual List<string> Roles { get; set; }
        public virtual string Salt { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual string UserName { get; set; }
    }
    #endregion
}
