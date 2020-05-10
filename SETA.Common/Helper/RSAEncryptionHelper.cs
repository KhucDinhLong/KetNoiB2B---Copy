using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SETA.Common.Helper
{
    public class RSAEncryptionHelper
    {
        static string publicPrivateKeyXML = "<RSAKeyValue><Modulus>woGOpcZ5RcoC5QYg6RLhnfiWd4fcQeiDMCqYtZQzrjkDko7jXxIeik8L2hpigxsNlfQkg3ONNyWsO+dRizP16OFv+B/CZQb6JAVK4NFBC+sr24qRzXsywOaNNQiaZ6sLm3JdaI2PSfx4GSeQjAAkYeKVwf0tssCGtd048xlR5is=</Modulus><Exponent>AQAB</Exponent><P>7oaTL1148ev9x6ZjiYKP0cbFeEb89WWdGTJkpZ3On5AVgv7LW4M18Q8tvnx4eEO+DxlsTG/v1Ai8WOdEwolGaQ==</P><Q>0MFq5lOt7VMWQ4JHmzKFS4xWUBMt0s+BdLMDcP/wDI8qp4gh1J1NByw9rhTsF+3k7KJioOWIKQGdezhR71Z9cw==</Q><DP>whcfjVsyHyk4yEzMkgh7nudvp/+bttOIkgg/fcR7bbuAxacvMrgCHLWNM4N1Q+dbsE2vokMzlAXHvU/y05mAgQ==</DP><DQ>kmCtlqfic2EpYVj+4OJB+UGEXE22efWq7qt/pEKyjfvtit+36SALnRX2ghSFoBndxdfvlKaeHTLWvEo3mb1p5Q==</DQ><InverseQ>MzwQ4+wUIGN2iNHA09yhQbdygCEoPEMGw2x53smyLS4SjWc/KctmUXaI4Lhy2+oimoCpWPFt9YswWORewBMAXw==</InverseQ><D>Q/UDE11sSpthdoY1ImnD5S8Q1zNjG63yg/YmA68DfXgTDPYab8GDZRxoDixQxfDCRuWVik3phV6GtilEPsgJPteaswwoaRh1jYf9wtYF0tWpyJnBWXmDD2E9Q2g6uArVJrFLOS/0P9l1n9dpRWyiGdvLQ1aCXEmnnfpZ34SPBnk=</D></RSAKeyValue>";
        static string publicOnlyKeyXML = "<RSAKeyValue><Modulus>woGOpcZ5RcoC5QYg6RLhnfiWd4fcQeiDMCqYtZQzrjkDko7jXxIeik8L2hpigxsNlfQkg3ONNyWsO+dRizP16OFv+B/CZQb6JAVK4NFBC+sr24qRzXsywOaNNQiaZ6sLm3JdaI2PSfx4GSeQjAAkYeKVwf0tssCGtd048xlR5is=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        public string TZEncrypt(string dataToDycript)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicOnlyKeyXML);
            var encryptedBytes = rsa.Encrypt(ASCIIEncoding.ASCII.GetBytes(dataToDycript), true);
            return Convert.ToBase64String(encryptedBytes);
        }

        public string TZDecrypt(string encryptedData)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicPrivateKeyXML);
            byte[] toBytes = Convert.FromBase64String(encryptedData);
            return ASCIIEncoding.ASCII.GetString(rsa.Decrypt(toBytes, true));
        }
    }
}

