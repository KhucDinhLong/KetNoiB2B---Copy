using System;
using Newtonsoft.Json;

namespace SecureNetRestApiSDK.Api.Models
{
    /// <summary>
    /// Data from a Vault payment account.
    /// </summary>
    public class PaymentVaultToken
    {
        #region Properties

        /// <summary>
        /// Customer identifier.
        /// </summary>
        [JsonProperty("customerId")]
        public String CustomerId { get; set; }

        /// <summary>
        /// Payment method to be used for billing.
        /// </summary>
        [JsonProperty("paymentMethodId")]
        public String PaymentMethodId { get; set; }

        /// <summary>
        /// Public Key used to identify the mechant.
        /// </summary>
        [JsonProperty("publicKey")]
        public String PublicKey { get; set; }

        /// <summary>
        /// Payment type that is stored or about to be stored in the Vault.
        /// </summary>
        [JsonProperty("paymentType")]
        public String PaymentType { get; set; }

        #endregion
    }

    public class VaultCredentials //hungdd6374
    {
        #region Properties

        /// <summary>
        /// secureNetId
        /// </summary>
        [JsonProperty("secureNetId")]
        public String SecureNetId { get; set; }

        /// <summary>
        /// secureNetKey
        /// </summary>
        [JsonProperty("secureNetKey")]
        public String SecureNetKey { get; set; }
         
        #endregion
    }
}
