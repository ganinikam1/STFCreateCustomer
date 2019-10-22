using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STFCreateCustomer
{
    class clsCreateCustomer

    {

        public Decimal Latitude { get; set; }
        public Decimal Longitude { get; set; }
        public String OutletGuid { get; set; }
        public String RouteErpId { get; set; }
        public String Country { get; set; }
        public String OutletErpId { get; set; }
        public Decimal TotalPotential { get; set; }
        public Decimal TotalCompanyPotential { get; set; }
        public Decimal TotalCompanyPotentialAlternate { get; set; }
        // public String SegmentationScope { get; set; }
        // public String Segmentation { get; set; }
        public Boolean Focused { get; set; }
        public String RecommendationTag { get; set; }
        public String Beat { get; set; }
        //  public String SegmentationDisplayName { get; set; }
        //  public String ChannelName { get; set; }
        public String OutletName { get; set; }
        //  public String ShopType { get; set; }
        //  public String ShopTypeErpId { get; set; }
        // public String OutletChannel { get; set; }
        //   public String ChannelErpId { get; set; }
        public String Address { get; set; }
        public String OwnersName { get; set; }
        public String OwnersNo { get; set; }
        public String TIN { get; set; }
        public String GSTIN { get; set; }
        public String PAN { get; set; }
        public String Aadhar { get; set; }
        public String Email { get; set; }
        public String PinCode { get; set; }
        public String MarketName { get; set; }
        public String City { get; set; }
        public String SubCity { get; set; }
        public String State { get; set; }
        public Boolean GSTRegistered { get; set; }
        public String BankAccountNumber { get; set; }
        public String AccountHoldersName { get; set; }
        public String IFSCCode { get; set; }
        public String AttributeText1 { get; set; }
        public String AttributeText2 { get; set; }
        public String AttributeText3 { get; set; }

        //  public String SegmentationErpId { get; set; }



    }
    class clsMapRouteOutlet
    {
        public List<String> Outlets = new List<String>();
         public String RouteErpId { get; set; }

    }

}
