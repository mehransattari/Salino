using System.ServiceModel;

namespace SalinoMvc5.SalinoUtilities
{
    public class PaymentOptions
    {
        public ZarinPalOptions ZarinPal { get; set; } = new();
        public ParsianOptions Parsian { get; set; } = new();

        /// <summary>Set once at startup from the "Payment" configuration section.</summary>
        public static PaymentOptions Current { get; set; } = new();
    }

    public class ZarinPalOptions
    {
        public string MerchantId { get; set; }
        public bool UseSandbox { get; set; } = true;
        public string ServiceUrl => UseSandbox
            ? "https://sandbox.zarinpal.com/pg/services/WebGate/service"
            : "https://www.zarinpal.com/pg/services/WebGate/service";
        public string StartPayUrl => UseSandbox
            ? "https://sandbox.zarinpal.com/pg/StartPay/"
            : "https://www.zarinpal.com/pg/StartPay/";

        public ServiceZarinPalTest.PaymentGatewayImplementationServicePortTypeClient CreateClient() =>
            new(new BasicHttpBinding(BasicHttpSecurityMode.Transport), new EndpointAddress(ServiceUrl));
    }

    public class ParsianOptions
    {
        public string LoginAccount { get; set; }
        /// <summary>Site root Parsian redirects back to, e.g. http://salinotbz.ir</summary>
        public string CallbackBaseUrl { get; set; }
        public string ServiceUrl { get; set; } = "https://pec.shaparak.ir/NewIPGServices/Sale/SaleService.asmx";
        public string StartPayUrl { get; set; } = "https://pec.shaparak.ir/NewIPG/?token=";

        public ParsianSale.SaleServiceSoapClient CreateClient() =>
            new(new BasicHttpBinding(BasicHttpSecurityMode.Transport), new EndpointAddress(ServiceUrl));
    }
}
