using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProvisionPay;
using ProvisionPay.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.demo.webapi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SDKDemoController : ControllerBase
    {
        private readonly ISoftposDeeplinkSDK _sdk;
        public SDKDemoController(ISoftposDeeplinkSDK sdk)
        {
            _sdk = sdk;
        }
        [HttpPost]
        public IActionResult EnrollUser()
        {
            var enrollUserRequest = new EnrollUserRequest()
            {
                PackageId = "com.provisionpay.softpos.yourtenant",
                UserId = "User1",
                WspTenantId = "yourcompanytenant"
            };

            var enrollUserResponse = _sdk.EnrollUser(enrollUserRequest);

            return Ok(enrollUserResponse);
        }

        [HttpPost]
        public IActionResult EnrollUserWithTerminal()
        {
            var enrolluserWithDetailRequest = new EnrollUserWithDetailRequest()
            {
                PackageId = "com.provisionpay.softpos.yourtenant",
                UserId = "User1",
                WspTenantId = "yourcompanytenant",

                TerminalId = "ASDA1234",//must be 8 character
                AcquirerId = "000001",//must be  6 character
                TerminalSerialNumber = "12312312",//must be  8 character
                EmvProfileId = "123",

                MerchantId = "000000000000001",//must be 15 character
                MerchantCategoryCode = "1234",//must be 4 character
                MerchantNameLocation = "Your Merchant Adress",
                MerchantNameToPrint = "Your Merchant Name"
            };

            var enrolluserWithDetailResponse = _sdk.EnrollUserWithDetail(enrolluserWithDetailRequest);

            return Ok(enrolluserWithDetailResponse);
        }
        [HttpPost]
        public IActionResult CancelUserEnrollment()
        {
            var cancelUserEnrollmentRequest = new CancelUserEnrollmentRequest()
            {
                UserId = "User1",
                WspTenantId = "yourcompanytenant",
            };

            var cancelUserEnrollmentResponse = _sdk.CancelUserEnrollment(cancelUserEnrollmentRequest);

            return Ok(cancelUserEnrollmentResponse);
        }

        [HttpPost]
        public IActionResult CancelUserActivationRequest()
        {
            var cancelUserActivationRequest = new CancelUserActivationRequest()
            {
                UserId = "User1",
                WspTenantId = "yourcompanytenant",
            };

            var cancelUserActivationResponse = _sdk.CancelUserActivation(cancelUserActivationRequest);

            return Ok(cancelUserActivationResponse);
        }

        [HttpGet]
        public IActionResult GetTransactionByPaymentSessionToken()
        {
            string token = "Your Payment Session Token";
            var transactionByPaymentSessionTokenRequest = new GetTransactionByPaymentSessionTokenRequest()
            {
                PaymentSessionToken = token
            };

            var transactionByPaymentSessionToken = _sdk.GetTransactionByPaymentSessionToken(transactionByPaymentSessionTokenRequest);

            return Ok(transactionByPaymentSessionToken);
        }

        [HttpGet]
        public IActionResult GetPaymentSessionStatus()
        {
            string token = "Your Payment Session Token";
            var paymentSessionStatusRequest = new GetPaymentSessionStatusRequest()
            {
                PaymentSessionToken = token
            };

            var getPaymentSessionStatus = _sdk.GetPaymentSessionStatus(paymentSessionStatusRequest);

            return Ok(getPaymentSessionStatus);
        }

        [HttpPost]
        public IActionResult CreatePaymentSessionToken()
        {
            var createPaymentSessionTokenRequest = new CreatePaymentSessionTokenRequest()
            {
                UserHash = "User1",
                Amount = 1,
                CallBackURL = "https://yourapplink",
                CurrencyCode = Constants.CurrencyCode.TRY,
                OrderID = "YourOrderId",
                TransactionType = Constants.TransactionType.Sale,
            };

            var createPaymentSession = _sdk.CreatePaymentSessionToken(createPaymentSessionTokenRequest);

            return Ok(createPaymentSession);
        }

        [HttpPost]
        public IActionResult HandleWebhook()
        {
            APICredentials apiCredentials = new APICredentials("secretkey", "accesskey");
            var privateKey = "Your private key 32 character";
            WebHookTransactionDetail webHookTransactionDetail = _sdk.HandleWebhook(apiCredentials, privateKey, Request);

            return Ok(webHookTransactionDetail);
        }

        [HttpGet]
        public IActionResult HandleCallbackData()
        {
            var privateKey = "Your private key 32 character";
            var handleCallbackData = _sdk.HandleCallBackData(Request, privateKey);
            return Ok(handleCallbackData);
        }
    }
}
