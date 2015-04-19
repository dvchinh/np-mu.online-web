using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceStack.OrmLite;
using System.IO;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.ServiceInterface.Auth;

namespace LifeSpringFixUPC
{
    public partial class Form1 : Form
    {
        IDbConnection Db;
        List<CouponModel> data = new List<CouponModel>();
        List<AccessCustomers> customer = new List<AccessCustomers>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.openFileDialog1.InitialDirectory = Application.StartupPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!LoadData())
            {
                MessageBox.Show("Can load load data file");
                return;
            }

            if (!ConnectDB())
            {
                MessageBox.Show("Can not load db");
                return;
            }

            var countries = Db.Select<Country>();
            var products = Db.Select<Product>();
            tSQL.Text = "";
            var count = 0;
            tExpired.Text = "0";
            tDuplicated.Text = "0";
            tInserted.Text = "0";
            foreach (var coupon in data)
            {
                if (!(coupon.couponenddate >= DateTime.Now))
                {
                    tExpired.Text = (int.Parse(tExpired.Text) + 1).ToString();
                    continue;
                }

                // check existing
                if (Db.Select<CouponPromo>(x => x.Where(m => m.Code == coupon.couponname).Limit(1)).Count > 0)
                {
                    //UpdateInfo("Coupon " + coupon.couponname + " already existed");
                    tDuplicated.Text = (int.Parse(tDuplicated.Text) + 1).ToString();
                    tSQL.Text = string.Format("-------Duplication {0}     ||     {1}", coupon.couponname, coupon.couponsecuritycode) + "\r\n" + tSQL.Text;
                    continue;
                }

                // Check for Coupon Used Count & Coupon Limit
                if (coupon.couponlimit > 0 && coupon.couponusedcount == coupon.couponlimit)
                {
                    tExpired.Text = (int.Parse(tExpired.Text) + 1).ToString();
                    //UpdateInfo(string.Format("Cancel: Coupon Used Count >= Coupon Limit"));
                    continue;
                }

                var c = new CouponPromo();

                c.AppliedOptions = new List<long>();
                c.BeginDate = coupon.couponstartdate;
                c.Code = coupon.couponname;
                c.SecurityCode = coupon.couponsecuritycode;
                c.SecurityCodeRequired = cRequireSecurity.Checked;
                c.Used = coupon.couponusedcount;
                c.CouponTypeEnum = Enum_CouponType.Groupon;
                c.DiscountAmount = coupon.couponamount;
                c.EndDate = coupon.couponenddate;
                c.ExceptProducts = new List<long>();
                c.Id = 0;
                c.isApplyToOption = false;
                c.isPercentDiscount = false;
                c.IssueOn = DateTime.Now;
                c.IssueTo = "";
                c.MaxUse = coupon.couponlimit;
                c.CountryCode = "";
                c.IssueTo = coupon.couponcomment;
                // get the country
                var country = countries.Where(x => x.Currency3Letter == coupon.couponcurrency).FirstOrDefault();
                if (country != null)
                {
                    c.CountryCode = country.Code;
                }
                else
                {
                    continue;
                }

                if (coupon.couponlimit == 0 && coupon.couponusedcount == 0)
                {
                    c.MaxUse = 1;
                }

                // check for teh coupon type
                if (coupon.couponlimit == 0)
                {
                    // this is promo code
                    //c.CouponTypeEnum = Enum_CouponType.Monthly_PromoCode;
                    c.MaxUse = 1;
                }

                // insert into db
                Db.Insert<CouponPromo>(c);
                Application.DoEvents();

                count++;
               //tSQL.Text = string.Format("Inserted coupon {0} {1}  - {2:0.00}% ", coupon.couponname, coupon.couponsecuritycode, (double)count / (double)data.Count * 100) + "\r\n" + tSQL.Text;
                tInserted.Text = (int.Parse(tInserted.Text) + 1).ToString();

                Application.DoEvents();
            }

            UpdateInfo("Finish " + DateTime.Now.ToString());
        }

        void UpdateInfo(string st)
        {
            ListViewItem item = new ListViewItem(st);
            item.SubItems.Add(st);
            lResult.Items.Insert(0, item);
            Application.DoEvents();
        }

        bool ConnectDB()
        {
            SqlServerOrmLiteDialectProvider dialect = SqlServerOrmLiteDialectProvider.Instance;
            dialect.UseUnicode = true;
            dialect.UseDatetime2(true);
            dialect.StringColumnDefinition = "nvarchar(MAX)";
            dialect.StringLengthColumnDefinitionFormat = dialect.StringColumnDefinition;
            dialect.StringLengthNonUnicodeColumnDefinitionFormat = dialect.StringColumnDefinition;
            dialect.StringLengthUnicodeColumnDefinitionFormat = dialect.StringColumnDefinition;
            var ret = new OrmLiteConnectionFactory(this.tConnection.Text, dialect);
            ret.AutoDisposeConnection = true;
            try
            {
                this.Db = ret.OpenDbConnection();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //      public bool ConnectToAccess()
        //      {
        //          FileAccess = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;" +
        //"Data Source=" + Application.StartupPath + "\\db.mdb;Persist Security Info=False");
        //          try
        //          {
        //              FileAccess.Open();
        //          }
        //          catch
        //          {
        //              UpdateInfo("Can not open Access db");
        //              return false;
        //          }
        //          return true;
        //      }

        public bool LoadData()
        {
            bool ret = true;

            List<string> csv_title = new List<string>();
            bool first_line = true;

            try
            {
                data = new List<CouponModel>();
                lStatus.Text = "";
                int row_index = 0;
                using (CsvFileReader reader = new CsvFileReader(this.lSCVFile.Text))
                {
                    CsvRow row = new CsvRow();
                    while (reader.ReadRow(row))
                    {
                        row_index++;
                        lStatus.Text = "Read row " + row_index.ToString();
                        Application.DoEvents();
                        //Application.DoEvents();
                        if (first_line) // need to parse the title
                        {
                            // check if the title missing
                            if (row.ValidRow())
                            {
                                csv_title = row.ToList();
                                first_line = false;
                            }
                            else
                            {
                                ret = false;
                                break;
                            }
                        }
                        else
                        {
                            // parse data
                            if (row.ValidRow())
                            {
                                CouponModel model = new CouponModel();
                                try
                                {
                                    // determine which field we need to put into object
                                    for (int i = 0; i < csv_title.Count; i++)
                                        switch (csv_title[i])
                                        {

                                            case "couponname":
                                                model.couponname = row[i].Trim();
                                                break;
                                            case "couponId":
                                                model.couponId = long.Parse(row[i]);
                                                break;
                                            case "couponsecuritycode":
                                                model.couponsecuritycode = row[i].Trim();
                                                break;
                                            case "couponamount":
                                                model.couponamount = double.Parse(row[i]);
                                                break;
                                            case "couponcurrency":
                                                model.couponcurrency = row[i].Trim().ToUpper();
                                                break;

                                            case "couponstartdate":
                                                model.couponstartdate = DateTime.Parse(row[i]);
                                                break;
                                            case "couponenddate":
                                                model.couponenddate = DateTime.Parse(row[i]);
                                                break;

                                            case "couponlimit":
                                                model.couponlimit = int.Parse(row[i]);
                                                break;

                                            case "couponusedcount":
                                                model.couponusedcount = int.Parse(row[i]);
                                                break;

                                            case "couponproducts":
                                                model.couponproducts = row[i];
                                                break;

                                            case "couponlastuseddate":

                                                try
                                                {
                                                    model.couponlastuseddate = row[i];
                                                }
                                                catch
                                                {
                                                    model.couponlastuseddate = "";
                                                }
                                                break;

                                            case "couponcomment":

                                                try
                                                {
                                                    model.couponcomment = row[i];
                                                }
                                                catch
                                                {
                                                    model.couponcomment = "";
                                                }
                                                break;

                                            case "couponother":
                                                try
                                                {
                                                    model.couponother = row[i];
                                                }
                                                catch
                                                {
                                                    model.couponother = "";
                                                }
                                                break;
                                            default:
                                                break;

                                        }

                                    data.Add(model);
                                }
                                catch (Exception ex)
                                {
                                    // do not thing, forget this coupon
                                    UpdateInfo("COUPON IMPORT . Id = " + model.couponId + " row = " + row_index.ToString() + "; Error: " + ex.Message);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret = false;
            }
            return ret;
        }


        public bool LoadCustomer()
        {
            bool ret = true;

            List<string> csv_title = new List<string>();
            bool first_line = true;

            try
            {
                customer = new List<AccessCustomers>();
                lStatus.Text = "";
                int row_index = 0;
                using (CsvFileReader reader = new CsvFileReader(this.lCustomerFilename.Text))
                {
                    CsvRow row = new CsvRow();
                    while (reader.ReadRow(row))
                    {
                        row_index++;
                        lStatus.Text = "Read row " + row_index.ToString();
                        Application.DoEvents();
                        Application.DoEvents();
                        if (first_line) // need to parse the title
                        {
                            // check if the title missing
                            if (row.ValidRow())
                            {
                                csv_title = row.ToList();
                                first_line = false;
                            }
                            else
                            {
                                ret = false;
                                break;
                            }
                        }
                        else
                        {
                            // parse data
                            if (row.ValidRow())
                            {
                                AccessCustomers model = new AccessCustomers();
                                try
                                {
                                    // determine which field we need to put into object
                                    for (int i = 0; i < csv_title.Count; i++)
                                        switch (csv_title[i])
                                        {


                                            case "contactid":
                                                try
                                                {
                                                    model.contactid = long.Parse(row[i]);
                                                }
                                                catch
                                                {
                                                    model.contactid = 0;
                                                }
                                                break;

                                            case "firstname":
                                                try
                                                {
                                                    model.firstname = row[i];
                                                }
                                                catch
                                                {
                                                    model.firstname = "";
                                                }
                                                break;
                                            case "lastname":
                                                try
                                                {
                                                    model.lastname = row[i];
                                                }
                                                catch
                                                {
                                                    model.lastname = "";
                                                }
                                                break;

                                            case "dob":
                                                try
                                                {
                                                    model.dob = row[i];
                                                }
                                                catch
                                                {
                                                    model.dob = "";
                                                }
                                                break;

                                            case "address":
                                                try
                                                {
                                                    model.address = row[i];
                                                }
                                                catch
                                                {
                                                    model.address = "";
                                                }
                                                break;

                                            case "city":
                                                try
                                                {
                                                    model.city = row[i];
                                                }
                                                catch
                                                {
                                                    model.city = "";
                                                }
                                                break;

                                            case "state":
                                                try
                                                {
                                                    model.state = row[i];
                                                }
                                                catch
                                                {
                                                    model.state = "";
                                                }
                                                break;

                                            case "postcode":
                                                try
                                                {
                                                    model.postcode = row[i];
                                                }
                                                catch
                                                {
                                                    model.postcode = "";
                                                }
                                                break;

                                            case "country":
                                                try
                                                {
                                                    model.country = row[i];
                                                }
                                                catch
                                                {
                                                    model.country = "";
                                                }
                                                break;

                                            case "phone":
                                                try
                                                {
                                                    model.phone = row[i];
                                                }
                                                catch
                                                {
                                                    model.phone = "";
                                                }
                                                break;
                                            case "email":
                                                try
                                                {
                                                    model.email = row[i];
                                                }
                                                catch
                                                {
                                                    model.email = "";
                                                }
                                                break;

                                            case "website":
                                                try
                                                {
                                                    model.website = row[i];
                                                }
                                                catch
                                                {
                                                    model.website = "";
                                                }
                                                break;
                                            case "password":
                                                try
                                                {
                                                    model.password = row[i];
                                                }
                                                catch
                                                {
                                                    model.password = "";
                                                }
                                                break;
                                            default:
                                                break;

                                        }

                                    customer.Add(model);
                                }
                                catch (Exception ex)
                                {
                                    // do not thing, forget this coupon
                                    UpdateInfo("CUSTOMER PARSER . Id = " + model.contactid + " row = " + row_index.ToString() + "; Error: " + ex.Message);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                this.lSCVFile.Text = file;
            }
        }

        private void lResult_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                this.lCustomerFilename.Text = file;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!LoadCustomer())
            {
                MessageBox.Show("Can load load data file");
                return;
            }

            if (!ConnectDB())
            {
                MessageBox.Show("Can not load db");
                return;
            }

            //if (!ConnectToAccess())
            //{
            //    MessageBox.Show("Can not load access file");
            //    return;
            //}

            var countries = Db.Select<Country>();

            var count = 0;
            tSQL.Text = "";
            tInserted.Text = "0";
            tDuplicated.Text = "0";
            foreach (var cus in customer)
            {
                // check existing
                if (Db.Select<ABUserAuth>(x => x.Where(m => m.Email == cus.email).Limit(1)).Count > 0)
                {
                    UpdateInfo("User " + cus.email + " already existed");
                    tDuplicated.Text = (int.Parse(tDuplicated.Text) + 1).ToString();
                    continue;
                }

                var c = new ABUserAuth();
                c.ActiveStatus = true;
                c.Addr = cus.address;

                // date
                try
                {
                    c.BirthDate = DateTime.Parse(cus.dob);
                }
                catch
                {
                    c.BirthDate = DateTime.MinValue;
                }

                c.City = cus.city;

                var country = countries.Where(x => x.Code == cus.country).FirstOrDefault();
                if (country != null)
                {
                    c.Country = country.Code;
                }
                else
                {
                    c.Country = "MY";
                }

                c.CreatedDate = DateTime.Now;

                c.DisplayName = cus.firstname + " " + cus.lastname;
                c.Email = cus.email;
                c.FirstName = cus.firstname;
                c.FullName = c.DisplayName;
                c.Gender = "male";
                c.GroupId = 0;
                c.Id = 0;
                c.LastName = cus.lastname;
                c.MailAddress = cus.email;
                c.Permissions = new List<string>();
                c.Roles = new List<string>();
                c.Roles.Add("Customer");
                c.Phone = cus.phone;
                c.PostalCode = cus.postcode;
                c.PrimaryEmail = cus.email;
                c.States = cus.state;
                c.UserName = cus.email;

                // password
                var PasswordHasher = new SaltedHash();
                string salt;
                string hash;
                PasswordHasher.GetHashAndSaltString(cus.password, out hash, out salt);
                c.PasswordHash = hash;
                c.Salt = salt;

                // then insert
                Db.Insert<ABUserAuth>(c);

                Application.DoEvents();
                count++;
                tInserted.Text = (int.Parse(tInserted.Text) + 1).ToString();
                tSQL.Text = string.Format("Inserted customer {0} {1} {2}  - {3:0.00}% ", cus.firstname, cus.lastname, cus.email, (double)count / (double)customer.Count * 100) + "\r\n" + tSQL.Text;
                Application.DoEvents();
                Application.DoEvents();
            }


            UpdateInfo("Finish " + DateTime.Now.ToString());
        }
    }
}
