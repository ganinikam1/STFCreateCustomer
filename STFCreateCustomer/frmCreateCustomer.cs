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
                try
                {

                    DataTable dtCustDetails = SqlDBHelper.ExecuteSelectCommand(@"exec [sp_FA_CreateCustomer] 'Create'", CommandType.Text);

                    for (int i = 0; i < dtCustDetails.Rows.Count; i++)
                    {
                        List<clsCreateCustomer> customers = new List<clsCreateCustomer>();
                        clsMapRouteOutlet RouteCustMap = new clsMapRouteOutlet();
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

                        customers.Add(customer);


                        RouteCustMap.Outlets.Add(dtCustDetails.Rows[i]["OutletErpId"].ToString());
                        RouteCustMap.RouteErpId = dtCustDetails.Rows[i]["RouteErpId"].ToString();



                        var jsonSerialiser = new JavaScriptSerializer();
                        var json1 = jsonSerialiser.Serialize(customers);
                        var json2 = jsonSerialiser.Serialize(RouteCustMap);
                        JsonString = json1;
                        ASCIIEncoding encoder = new ASCIIEncoding();
                        byte[] data = encoder.GetBytes(json1);
                        byte[] data1 = encoder.GetBytes(json2);

                        File.AppendAllText(Application.StartupPath + @"\" + DateTime.Now.ToString("ddMMyy_hhmm") + ".txt", json1);

                        HttpWebRequest request = WebRequest.Create("http://api.fieldassist.in/api/V3/Outlet/CreateMultiple") as HttpWebRequest;
                        request.Credentials = new System.Net.NetworkCredential("Raw10550", "W8)ke4eeXtmrllfehe1l");
                        // HttpWebRequest request = WebRequest.Create("http://api-debug.fieldassist.in/api/V3/Outlet/CreateMultiple") as HttpWebRequest;
                        //  request.Credentials = new System.Net.NetworkCredential("RAW_Chitransh", "9vf_1nue^T&z2Avb_rUe");


                        HttpWebRequest request1 = WebRequest.Create("http://api.fieldassist.in/api/V3/EmployeeJourney/UpdateRouteOutlet") as HttpWebRequest;
                        request1.Credentials = new System.Net.NetworkCredential("Raw10550", "W8)ke4eeXtmrllfehe1l");

                        request.Method = "POST";
                        request.ContentType = "application/json";
                        request.ContentLength = data.Length;
                        request.GetRequestStream().Write(data, 0, data.Length);

                        request1.Method = "POST";
                        request1.ContentType = "application/json";
                        request1.ContentLength = data1.Length;
                        request1.GetRequestStream().Write(data1, 0, data1.Length);

                    }
                }

                catch (Exception e1)
                {
                    WriteErrorLog(e1.Message + Environment.NewLine + JsonString);
                    String Query = @"USE msdb exec sp_send_DBMail 
@profile_name = 'MISMails',
@recipients = 'ganesh.nikam@rawpressery.com;deepa@rawpressery.com',
@subject = 'FA Customer Creation Failed',
@body = ' Dear Team," + Environment.NewLine + " Please find below Error msg " + Environment.NewLine + e1.Message + Environment.NewLine + JsonString + "'";
                    SqlDBHelper.ExecuteNonQuery(Query, CommandType.Text);
                }


                try
                {
                    DataTable dtUpdateCustDetails = SqlDBHelper.ExecuteSelectCommand(@"exec [sp_FA_CreateCustomer] 'Update'", CommandType.Text);

                    for (int i = 0; i < dtUpdateCustDetails.Rows.Count; i++)
                    {
                        List<clsUpdateCustomer> customers = new List<clsUpdateCustomer>();
                        clsMapRouteOutlet RouteCustMap = new clsMapRouteOutlet();
                        clsUpdateCustomer customer = new clsUpdateCustomer();


                        customer.OutletErpId = dtUpdateCustDetails.Rows[i]["OutletErpId"].ToString();
                        customer.Beat = dtUpdateCustDetails.Rows[i]["Beat"].ToString();
                        customer.OutletName = dtUpdateCustDetails.Rows[i]["OutletName"].ToString();
                        customer.Address = dtUpdateCustDetails.Rows[i]["Address"].ToString();

                        customer.GSTIN = dtUpdateCustDetails.Rows[i]["GSTIN"].ToString();
                        customer.Email = dtUpdateCustDetails.Rows[i]["Email"].ToString();
                        customer.PinCode = dtUpdateCustDetails.Rows[i]["PinCode"].ToString();
                        customer.MarketName = dtUpdateCustDetails.Rows[i]["MarketName"].ToString();
                        customer.City = dtUpdateCustDetails.Rows[i]["City"].ToString();
                        customer.GSTRegistered = Convert.ToBoolean(dtUpdateCustDetails.Rows[i]["GSTRegistered"]);

                        customers.Add(customer);


                        RouteCustMap.Outlets.Add(dtUpdateCustDetails.Rows[i]["OutletErpId"].ToString());
                        RouteCustMap.RouteErpId = dtUpdateCustDetails.Rows[i]["RouteErpId"].ToString();



                        var jsonSerialiser = new JavaScriptSerializer();
                        var json1 = jsonSerialiser.Serialize(customers);
                        var json2 = jsonSerialiser.Serialize(RouteCustMap);
                        JsonString = json1;
                        ASCIIEncoding encoder = new ASCIIEncoding();
                        byte[] data = encoder.GetBytes(json1);
                        byte[] data1 = encoder.GetBytes(json2);

                        File.AppendAllText(Application.StartupPath + @"\" + DateTime.Now.ToString("ddMMyy_hhmm") + ".txt", json1);

                        HttpWebRequest request = WebRequest.Create("http://api.fieldassist.in/api/V3/Outlet/Update") as HttpWebRequest;
                        request.Credentials = new System.Net.NetworkCredential("Raw10550", "W8)ke4eeXtmrllfehe1l");
                        // HttpWebRequest request = WebRequest.Create("http://api-debug.fieldassist.in/api/V3/Outlet/CreateMultiple") as HttpWebRequest;
                        //  request.Credentials = new System.Net.NetworkCredential("RAW_Chitransh", "9vf_1nue^T&z2Avb_rUe");


                        HttpWebRequest request1 = WebRequest.Create("http://api.fieldassist.in/api/V3/EmployeeJourney/UpdateRouteOutlet") as HttpWebRequest;
                        request1.Credentials = new System.Net.NetworkCredential("Raw10550", "W8)ke4eeXtmrllfehe1l");



                        request.Method = "POST";
                        request.ContentType = "application/json";
                        request.ContentLength = data.Length;
                        request.GetRequestStream().Write(data, 0, data.Length);

                        request1.Method = "POST";
                        request1.ContentType = "application/json";
                        request1.ContentLength = data1.Length;
                        request1.GetRequestStream().Write(data1, 0, data1.Length);

                    }

                }

                catch (Exception e2)
                {
                    WriteErrorLog(e2.Message + Environment.NewLine + JsonString);
                    String Query = @"USE msdb  exec sp_send_DBMail 
@profile_name = 'MISMails',
@recipients = 'ganesh.nikam@rawpressery.com;deepa@rawpressery.com',
@subject = 'FA Customer Updation Failed',
@body = ' Dear Team," + Environment.NewLine + " Please find below Error msg " + Environment.NewLine + e2.Message + Environment.NewLine + JsonString + "'";
                    SqlDBHelper.ExecuteNonQuery(Query, CommandType.Text);

                }









            }
            catch (Exception ex)
            {
                WriteErrorLog(ex.Message + Environment.NewLine + JsonString);
            }
            Application.Exit();

        }
    }
}
