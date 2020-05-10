using System;
using System.Configuration;
using System.Linq;
using SecureNetRestApiSDK.Api.Controllers;
using SecureNetRestApiSDK.Api.Models;
using SecureNetRestApiSDK.Api.Requests;
using SecureNetRestApiSDK.Api.Responses;
using SNET.Core;

namespace SETA.Common.Helper
{
    public class PaymentHelper
    {
        static string publicKey = ConfigurationManager.AppSettings["WPPublicKey"];
        public static MessagePayment VerifyCard(Card card)
        {
            MessagePayment messagePayment;
            try
            {
                var request = new VerifyRequest
                {
                    Amount = 0,
                    Card = card,
                    ExtendedInformation = new ExtendedInformation
                    {
                        TypeOfGoods = "PHYSICAL"
                    },
                    DeveloperApplication = new DeveloperApplication
                    {
                        DeveloperId = 12345678,
                        Version = "1.2"
                    }
                };

                var apiContext = new APIContext();
                var controller = new PaymentsController();

                // Act
                var response = controller.ProcessRequest<VerifyResponse>(apiContext, request);

                messagePayment = new MessagePayment
                {
                    Message = response.Message,
                    Result = response.Result
                };
            }
            catch (Exception ex)
            {
                messagePayment = new MessagePayment
                {
                    Message = "Connection to Worldpay failure. Please try again later!",
                    Result = "BAD_REQUEST"
                };
            }
            return messagePayment;
        }
        public static MessagePayment CreateTokenPayment(Card card)
        {
            MessagePayment messagePayment;
            try
            {
                var request = new TokenCardRequest
                {
                    PublicKey = PaymentHelper.publicKey,
                    AddToVault = true,
                    Card = card,
                    DeveloperApplication = new DeveloperApplication
                    {
                        DeveloperId = 12345678,
                        Version = "1.2"
                    }
                };
                var apiContext = new APIContext();
                var controller = new PreVaultController();

                // Act
                var response = controller.ProcessRequest<TokenCardResponse>(apiContext, request);
                messagePayment = new MessagePayment
                {
                    Message = response.Message,
                    Result = response.Result,
                    Token = response.Token
                };
            }
            catch (Exception ex)
            {
                messagePayment = new MessagePayment
                {
                    Message = ex.Message,
                    Result = "BAD_REQUEST"
                };
            }
            return messagePayment;
        }

        public static MessagePayment ChargeTokenization(decimal amount, string paymentToken)
        {
            MessagePayment messagePayment;
            try
            {
                var request = new ChargeRequest
                {
                    Amount = amount,
                    PaymentVaultToken = new PaymentVaultToken
                    {
                        PaymentMethodId = paymentToken,
                        PaymentType = "STORED_VALUE",
                        PublicKey = PaymentHelper.publicKey,
                    },
                    AddToVault = true,
                    ExtendedInformation = new ExtendedInformation
                    {
                        TypeOfGoods = "PHYSICAL"
                    },
                    DeveloperApplication = new DeveloperApplication
                    {
                        DeveloperId = 12345678,
                        Version = "1.2"
                    }
                };
                var apiContext = new APIContext();
                var controller = new PaymentsController();
                // Act
                var response = controller.ProcessRequest<ChargeResponse>(apiContext, request);                
                messagePayment = new MessagePayment
                {
                    Message = response.Message,
                    Result = response.Result,
                    CardType = response.Transaction.CreditCardType,
                    CardNumber = response.Transaction.CardNumber,
                    TransactionId = response.Transaction.TransactionId
                };
            }
            catch (Exception ex)
            {
                messagePayment = new MessagePayment
                {
                    Message = ex.Message,
                    Result = "BAD_REQUEST"
                };
            }
            return messagePayment;
        }

        public static MessagePayment Refund(decimal amount, int transactionId)
        {
            MessagePayment messagePayment;
            try
            {
                // Arrange
                var request = new RefundRequest
                {
                    Amount = amount,
                    TransactionId = transactionId,
                    DeveloperApplication = new DeveloperApplication
                    {
                        DeveloperId = 12345678,
                        Version = "1.2",                        
                    }                    
                };

                var apiContext = new APIContext();
                var controller = new PaymentsController();

                // Act
                var response = controller.ProcessRequest<RefundResponse>(apiContext, request);
                messagePayment = new MessagePayment
                {
                    Message = response.Message,
                    Result = response.Result
                    //CardType = response.Transaction.CreditCardType,
                    //CardNumber = response.Transaction.CardNumber,
                    //TransactionId = response.Transaction.TransactionId
                };
            }
            catch (Exception ex)
            {
                messagePayment = new MessagePayment
                {
                    Message = ex.Message,
                    Result = "BAD_REQUEST"
                };
            }
            
            return messagePayment;
        }

        public CardFullName GetFullName(string sFullName)
        {
            try
            {
                CardFullName fullName = new CardFullName();
                sFullName = sFullName.Trim();
                var name = sFullName.Split(' ');
                fullName.LastName = name.Length == 1 ? string.Empty : name.Last();
                var idx = sFullName.LastIndexOf(' ');
                fullName.FirstName = idx >= 0 ? sFullName.Remove(idx) : sFullName;
                return fullName;
            }
            catch (Exception ex)
            {
                return  new CardFullName();
            }
            
        }

        public string FormatCardNumber(string cardNumber)
        {
            var length = cardNumber.Length;
            var showCard = cardNumber.Substring(cardNumber.Length - 4);
            var prefix = "";
            //fill xxx
            for (var i = 0; i < length - 4; i++)
            {
                prefix = prefix + "X";
            }
            return prefix + " " + showCard;
        }
    }

    public class MessagePayment
    {
        public string Message { set; get; }
        public string Result { set; get; }
        public string Token { set; get; }
        public string CardType { set; get; }
        public string CardNumber { set; get; }
        public int TransactionId { get; set; }
    }
    public class CardFullName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

