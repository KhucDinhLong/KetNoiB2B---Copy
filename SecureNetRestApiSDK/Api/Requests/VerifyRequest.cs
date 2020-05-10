using SecureNetRestApiSDK.Api.Models;
using SNET.Core;

namespace SecureNetRestApiSDK.Api.Requests
{
    public class VerifyRequest : SecureNetRequest  //hungdd6374
    {
        #region Properties

        public decimal Amount { get; set; }
        public Card Card { get; set; }
        public DeveloperApplication DeveloperApplication { get; set; }

        public ExtendedInformation ExtendedInformation { get; set; }

        #endregion

        #region Methods

        public override string GetUri()
        {
            return "api/Payments/Verify";
        }

        public override HttpMethodEnum GetMethod()
        {
            return HttpMethodEnum.POST;
        }

        #endregion
    }
}
