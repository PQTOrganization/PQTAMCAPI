using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PQAMCClasses.BlinQ
{
    public class BlinqPostPaymentDTO
    {
        //    < input type = "hidden" name = "client_id" id = "client_id" value = "0vnmf1T9DCJESiv" />
        //    < input type = "hidden" name = "payment_via" id = "payment_via" value = "BLINQ_VM" />
        //    < input type = "hidden" name = "order_id" id = "order_id" value = "170220211526-09" />
        //    < input type = "hidden" name = "customer_name" id = "customer_name" value = "Abid Zulfiqar" />
        //    < input type = "hidden" name = "customer_email" id = "customer_email" value = "abid.zulfi@domain.com" />
        //    < input type = "hidden" name = "customer_mobile" id = "customer_mobile" value = "03001234567" />
        //    < input type = "hidden" name = "order_amount" id = "order_amount" value = "100.00" />
        //    < input type = "hidden" name = "order_expiry_date_time" id = "order_expiry_date_time" value = "2021-01-
        //    01" />
        //    < input type = "hidden" name = "product_description" id = "product_description" value = "TestInvoice" />
        //    < input type = "hidden" name = "encrypted_form_data" id = "encrypted_form_data"
        //    value = "163155c38965e2c0f55bd76aca14e7cf6" />
        //    < input type = "hidden" name = "return_url" id = "return_url"
        //    value = "{https://www.yourdomain.com/order-confirmation}" />

        public string client_id { get; set; } = "";
        public string Paymentcode { get; set; } = "";
        public string acc_bank { get; set; } = "";
        public string payment_via { get; set; } = "";
        public string order_id { get; set; } = "";
        public string customer_name { get; set; } = "";
        public string customer_email { get; set; } = "";
        public string customer_mobile { get; set; } = "";
        public string order_amount { get; set; } = "";
        public string order_expiry_date_time { get; set; } = "";
        public string product_description { get; set; } = "";
        public string encrypted_form_data { get; set; } = "";
        public string return_url { get; set; } = "";
    }
}
