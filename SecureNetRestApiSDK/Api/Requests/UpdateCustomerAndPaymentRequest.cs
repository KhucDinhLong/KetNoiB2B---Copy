using System;
using System.Collections.Generic;
using SecureNetRestApiSDK.Api.Models;
using SNET.Core;

namespace SecureNetRestApiSDK.Api.Requests
{
    public class UpdateCustomerAndPaymentRequest : SecureNetRequest
    {
        #region Properties

        public string CustomerId { get; set; }
        public string PaymentMethodId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool SendEmailReceipts { get; set; }
        public string Notes { get; set; }
        public Address Address { get; set; }
        public string Company { get; set; }
        public List<UserDefinedField> UserDefinedFields { get; set; }
        public DeveloperApplication DeveloperApplication { get; set; }
        public int CustomerDuplicateCheckIndicator { get; set; }
        public Card Card { get; set; }
        public bool Primary { get; set; }
        public int AccountDuplicateCheckIndicator { get; set; }

        #endregion

        #region Methods

        public override string GetUri()
        {
            return String.Format("api/Customers/{0}/payments", CustomerId);
        }

        public override HttpMethodEnum GetMethod()
        {
            return HttpMethodEnum.POST;
        }

        #endregion
    }
}
