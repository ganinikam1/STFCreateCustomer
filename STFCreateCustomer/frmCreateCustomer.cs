using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Net;

namespace STFCreateCustomer
{
    public partial class frmCreateCustomer : Form
    {
        String JsonString = "";
        public frmCreateCustomer()
        {
            InitializeComponent();
        }
        public void WriteErrorLog(String Message)
        {
            String Msg = "";
            String LogFileName = Application.StartupPath + @"\" + "ERROR " + DateTime.Now.ToString("ddMMyyyy") + ".txt";
            Msg += Environment.NewLine + "-------------------------------------------------------------------------------------" + Environment.NewLine;
            Msg += DateTime.Now.ToString("hh:mm:ss tt") + "|Error Message: " + Message;
            Msg += "-------------------------------------------------------------------------------------";
            File.AppendAllText(LogFileName, Msg);

        }

        private void frmCreateCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCustDetails = SqlDBHelper.ExecuteSelectCommand(@"exec [sp_FA_CreateCustomer]", CommandType.Text);

                List<clsCreateCustomer> customers = new List<clsCreateCustomer>();

                for (int i = 0; i < dtCustDetails.Rows.Count; i++)
                {
                    clsCreateCustomer customer = new clsCreateCustomer();
                    customer.Latitude = Convert.ToDecimal(dtCustDetails.Rows[i]["Latitude"]);
                    customer.Longitude = Convert.ToDecimal(dtCustDetails.Rows[i]["Longitude"]);
                    customer.RouteErpId = dtCustDetails.Rows[i]["RouteErpId"].ToString();
                    customer.Country = dtCustDetails.Rows[i]["Country"].ToString();
                    customer.OutletErpId = dtCustDetails.Rows[i]["OutletErpId"].ToString();
                    customer.TotalPotential = Convert.ToDecimal(dtCustDetails.Rows[i]["TotalPotential"].ToString());
                    customer.TotalCompanyPotential = Convert.ToDecimal(dtCustDetails.Rows[i]["TotalCompanyPotential"].ToString());
                    customer.TotalCompanyPotentialAlternate = Convert.ToDecimal(dtCustDetails.Rows[i]["TotalCompanyPotentialAlternate"].ToString());
                    //  customer.SegmentationScope = dtCustDetails.Rows[i]["SegmentationScope"].ToString();
                    //  customer.Segmentation = dtCustDetails.Rows[i]["Segmentation"].ToString();
                    customer.Focused = Convert.ToBoolean(dtCustDetails.Rows[i]["Focused"].ToString());

                    customer.RecommendationTag = dtCustDetails.Rows[i]["RecommendationTag"].ToString();
                    customer.Beat = dtCustDetails.Rows[i]["Beat"].ToString();
                    //    customer.SegmentationDisplayName = dtCustDetails.Rows[i]["SegmentationDisplayName"].ToString();
                    //    customer.ChannelName = dtCustDetails.Rows[i]["ChannelName"].ToString();
                    customer.OutletName = dtCustDetails.Rows[i]["OutletName"].ToString();
                    //   customer.ShopType = dtCustDetails.Rows[i]["ShopType"].ToString();
                    //    customer.ShopTypeErpId = dtCustDetails.Rows[i]["ShopTypeErpId"].ToString();
                    //   customer.OutletChannel = dtCustDetails.Rows[i]["OutletChannel"].ToString();
                    //    customer.ChannelErpId = dtCustDetails.Rows[i]["ChannelErpId"].ToString();
                    customer.Address = dtCustDetails.Rows[i]["Address"].ToString();
                    customer.OwnersName = dtCustDetails.Rows[i]["OwnersName"].ToString();
                    customer.OwnersNo = dtCustDetails.Rows[i]["OwnersNo"].ToString();
                    customer.TIN = dtCustDetails.Rows[i]["TIN"].ToString();
                    customer.GSTIN = dtCustDetails.Rows[i]["GSTIN"].ToString();
                    customer.PAN = dtCustDetails.Rows[i]["PAN"].ToString();
                    customer.Aadhar = dtCustDetails.Rows[i]["Aadhar"].ToString();
                    customer.Email = dtCustDetails.Rows[i]["Email"].ToString();
                    customer.PinCode = dtCustDetails.Rows[i]["PinCode"].ToString();
                    customer.MarketName = dtCustDetails.Rows[i]["MarketName"].ToString();
                    customer.City = dtCustDetails.Rows[i]["City"].ToString();
                    customer.SubCity = dtCustDetails.Rows[i]["SubCity"].ToString();
                    customer.State = dtCustDetails.Rows[i]["State"].ToString();
                    customer.GSTRegistered = Convert.ToBoolean(dtCustDetails.Rows[i]["GSTRegistered"]);
                    customer.BankAccountNumber = dtCustDetails.Rows[i]["BankAccountNumber"].ToString();
                    customer.AccountHoldersName = dtCustDetails.Rows[i]["AccountHoldersName"].ToString();
                    customer.IFSCCode = dtCustDetails.Rows[i]["IFSCCode"].ToString();
                    customer.AttributeText1 = dtCustDetails.Rows[i]["AttributeText1"].ToString();
                    customer.AttributeText2 = dtCustDetails.Rows[i]["AttributeText2"].ToString();
                    customer.AttributeText3 = dtCustDetails.Rows[i]["AttributeText3"].ToString();
                    // customer.Segmentation = dtCustDetails.Rows[i]["Segmentation"].ToString();
                    //  customer.SegmentationErpId = dtCustDetails.Rows[i]["SegmentationErpId"].ToString();
                    customers.Add(customer);
                }


                var jsonSerialiser = new JavaScriptSerializer();
                var json1 = jsonSerialiser.Serialize(customers);
                JsonString = json1;
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] data = encoder.GetBytes(json1);

                File.AppendAllText(Application.StartupPath + @"\" + DateTime.Now.ToString("ddMMyy_hhmm") + ".txt", json1);

               // HttpWebRequest request = WebRequest.Create("http://api.fieldassist.in/api/V3/Outlet/CreateMultiple") as HttpWebRequest;
                //request.Credentials = new System.Net.NetworkCredential("Raw10550", "W8)ke4eeXtmrllfehe1l");
                HttpWebRequest request = WebRequest.Create("http://api-debug.fieldassist.in/api/V3/Outlet/CreateMultiple") as HttpWebRequest;
                 request.Credentials = new System.Net.NetworkCredential("RAW_Chitransh", "9vf_1nue^T&z2Avb_rUe");

                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                request.GetRequestStream().Write(data, 0, data.Length);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                response.Close();
                response.Dispose();




            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message + Environment.NewLine + JsonString);
            }
            Application.Exit();

        }
    }
}
