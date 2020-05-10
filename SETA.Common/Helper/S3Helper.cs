using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace SETA.DJMixesWeb.Business
{
    public class S3Helper
    {
        private static string[] subResourcesToConsider = new string[] { "acl", "lifecycle", "location", "logging", "notification", "partNumber", "policy", "requestPayment", "torrent", "uploadId", "uploads", "versionId", "versioning", "versions", "website", };
        private static string[] overrideResponseHeadersToConsider = new string[] { "response-content-type", "response-content-language", "response-expires", "response-cache-control", "response-content-disposition", "response-content-encoding" };

        //public static void UploadFile()
        //{
        //    string accessKey = "AKIAJSLVX2T5ZKNBICQA";
        //    string secretKey = "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";
        //    var requestUri = new Uri("https://djmixess3.s3.amazonaws.com/Do-Dua-Quan-Ho.mp3");

        //    var filePath = @"e:\Do-Dua-Quan-Ho.mp3";
        //    var expiryDate = DateTime.UtcNow.AddHours(12);
        //    var uploadId = InitiateMultipartUpload(accessKey, secretKey, requestUri, DateTime.UtcNow, "audio/MPA", null);
        //    var partNumberETags = UploadParts(accessKey, secretKey, requestUri, uploadId, filePath, expiryDate);
        //    FinishMultipartUpload(accessKey, secretKey, requestUri, uploadId, partNumberETags, expiryDate);
        //}

        public static string GetStringToSign(Uri requestUri, string httpVerb, string contentMD5, string contentType, DateTime date, NameValueCollection requestHeaders)
        {
            var canonicalizedResourceString = GetCanonicalizedResourceString(requestUri);
            var canonicalizedAmzHeadersString = GetCanonicalizedAmzHeadersString(requestHeaders);
            var dateInStringFormat = date.ToString("R");
            if (requestHeaders != null && requestHeaders.AllKeys.Contains("x-amz-date"))
            {
                dateInStringFormat = string.Empty;
            }
            var stringToSign = string.Format("{0}\n{1}\n{2}\n{3}\n{4}{5}", httpVerb, contentMD5, contentType, dateInStringFormat, canonicalizedAmzHeadersString, canonicalizedResourceString);
            return stringToSign;
        }

        public static string GetStringToSign(Uri requestUri, string httpVerb, string contentMD5, string contentType, long secondsSince1stJan1970, NameValueCollection requestHeaders)
        {
            var canonicalizedResourceString = GetCanonicalizedResourceString(requestUri);
            var canonicalizedAmzHeadersString = GetCanonicalizedAmzHeadersString(requestHeaders);
            var stringToSign = string.Format("{0}\n{1}\n{2}\n{3}\n{4}{5}", httpVerb, contentMD5, contentType, secondsSince1stJan1970, canonicalizedAmzHeadersString, canonicalizedResourceString);
            return stringToSign;
        }

        public static string GetCanonicalizedResourceString(Uri requestUri)
        {
            var host = requestUri.DnsSafeHost;
            var hostElementsArray = host.Split('.');
            var bucketName = "";
            if (hostElementsArray.Length > 3)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hostElementsArray.Length - 3; i++)
                {
                    sb.AppendFormat("{0}.", hostElementsArray[i]);
                }
                bucketName = sb.ToString();
                if (bucketName.Length > 0)
                {
                    if (bucketName.EndsWith("."))
                    {
                        bucketName = bucketName.Substring(0, bucketName.Length - 1);
                    }
                    bucketName = string.Format("/{0}", bucketName);
                }
            }
            var subResourcesList = subResourcesToConsider.ToList();
            var overrideResponseHeadersList = overrideResponseHeadersToConsider.ToList();
            StringBuilder canonicalizedResourceStringBuilder = new StringBuilder();
            canonicalizedResourceStringBuilder.Append(bucketName);
            canonicalizedResourceStringBuilder.Append(requestUri.AbsolutePath);
            NameValueCollection queryVariables = HttpUtility.ParseQueryString(requestUri.Query);
            SortedDictionary<string, string> queryVariablesToConsider = new SortedDictionary<string, string>();
            SortedDictionary<string, string> overrideResponseHeaders = new SortedDictionary<string, string>();
            if (queryVariables != null && queryVariables.Count > 0)
            {
                var numQueryItems = queryVariables.Count;
                for (int i = 0; i < numQueryItems; i++)
                {
                    var key = queryVariables.GetKey(i);
                    var value = queryVariables[key];
                    if (subResourcesList.Contains(key))
                    {
                        if (queryVariablesToConsider.ContainsKey(key))
                        {
                            var val = queryVariablesToConsider[key];
                            queryVariablesToConsider[key] = string.Format("{0},{1}", value, val);
                        }
                        else
                        {
                            queryVariablesToConsider.Add(key, value);
                        }
                    }
                    if (overrideResponseHeadersList.Contains(key))
                    {
                        overrideResponseHeaders.Add(key, HttpUtility.UrlDecode(value));
                    }
                }
            }
            if (queryVariablesToConsider.Count > 0 || overrideResponseHeaders.Count > 0)
            {
                StringBuilder queryStringInCanonicalizedResourceString = new StringBuilder();
                queryStringInCanonicalizedResourceString.Append("?");
                for (int i = 0; i < queryVariablesToConsider.Count; i++)
                {
                    var key = queryVariablesToConsider.Keys.ElementAt(i);
                    var value = queryVariablesToConsider.Values.ElementAt(i);
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        queryStringInCanonicalizedResourceString.AppendFormat("{0}={1}&", key, value);
                    }
                    else
                    {
                        queryStringInCanonicalizedResourceString.AppendFormat("{0}&", key);
                    }
                }
                for (int i = 0; i < overrideResponseHeaders.Count; i++)
                {
                    var key = overrideResponseHeaders.Keys.ElementAt(i);
                    var value = overrideResponseHeaders.Values.ElementAt(i);
                    queryStringInCanonicalizedResourceString.AppendFormat("{0}={1}&", key, value);
                }
                var str = queryStringInCanonicalizedResourceString.ToString();
                if (str.EndsWith("&"))
                {
                    str = str.Substring(0, str.Length - 1);
                }
                canonicalizedResourceStringBuilder.Append(str);
            }
            return canonicalizedResourceStringBuilder.ToString();
        }

        public static string GetCanonicalizedAmzHeadersString(NameValueCollection requestHeaders)
        {
            var canonicalizedAmzHeadersString = string.Empty;
            if (requestHeaders != null && requestHeaders.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                SortedDictionary<string, string> sortedRequestHeaders = new SortedDictionary<string, string>();
                var requestHeadersCount = requestHeaders.Count;
                for (int i = 0; i < requestHeadersCount; i++)
                {
                    var key = requestHeaders.Keys.Get(i);
                    var value = requestHeaders[key].Trim();
                    key = key.ToLowerInvariant();
                    if (key.StartsWith("x-amz-", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (sortedRequestHeaders.ContainsKey(key))
                        {
                            var val = sortedRequestHeaders[key];
                            sortedRequestHeaders[key] = string.Format("{0},{1}", val, value);
                        }
                        else
                        {
                            sortedRequestHeaders.Add(key, value);
                        }
                    }
                }
                if (sortedRequestHeaders.Count > 0)
                {
                    foreach (var item in sortedRequestHeaders)
                    {
                        sb.AppendFormat("{0}:{1}\n", item.Key, item.Value);
                    }
                    canonicalizedAmzHeadersString = sb.ToString();
                }
            }
            return canonicalizedAmzHeadersString;
        }

        public static string CreateSignature(string secretKey, string stringToSign)
        {
            byte[] dataToSign = Encoding.UTF8.GetBytes(stringToSign);
            using (HMACSHA1 hmacsha1 = new HMACSHA1(Encoding.UTF8.GetBytes(secretKey)))
            {
                return Convert.ToBase64String(hmacsha1.ComputeHash(dataToSign));
            }
        }

        public static string InitiateMultipartUpload(string accessKey, string secretKey, Uri requestUri, DateTime requestDate, string contentType, NameValueCollection requestHeaders)
        {
            var uploadId = string.Empty;
            var uploadIdRequestUrl = new Uri(string.Format("{0}?uploads=", requestUri.AbsoluteUri));
            var uploadIdRequestUrlRequestHeaders = new NameValueCollection();
            if (requestHeaders != null)
            {
                for (int i = 0; i < requestHeaders.Count; i++)
                {
                    var key = requestHeaders.Keys[i];
                    var value = requestHeaders[key];
                    if (key.StartsWith("x-amz-", StringComparison.InvariantCultureIgnoreCase))
                    {
                        uploadIdRequestUrlRequestHeaders.Add(key, value);
                    }
                }
            }
            var stringToSign = GetStringToSign(uploadIdRequestUrl, "POST", string.Empty, contentType, requestDate, requestHeaders);
            var signatureForUploadId = CreateSignature(secretKey, stringToSign);
            uploadIdRequestUrlRequestHeaders.Add("Authorization", string.Format("AWS {0}:{1}", accessKey, signatureForUploadId));
            var request = (HttpWebRequest)WebRequest.Create(uploadIdRequestUrl);
            request.Method = "POST";
            request.ContentLength = 0;
            request.Date = requestDate;
            request.ContentType = contentType;
            request.Headers.Add(uploadIdRequestUrlRequestHeaders);
            using (var resp = (HttpWebResponse)request.GetResponse())
            {
                using (var s = new StreamReader(resp.GetResponseStream()))
                {
                    var response = s.ReadToEnd();
                    XElement xe = XElement.Parse(response);
                    uploadId = xe.Element(XName.Get("UploadId", "http://s3.amazonaws.com/doc/2006-03-01/")).Value;
                }
            }
            return uploadId;
        }

        public static Dictionary<int, string> UploadParts(string accessKey, string secretKey, Uri requestUri, string uploadId, string filePath, DateTime expiryDate)
        {
            Dictionary<int, string> partNumberETags = new Dictionary<int, string>();
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan ts = new TimeSpan(expiryDate.Ticks - Jan1st1970.Ticks);
            var expiry = Convert.ToInt64(ts.TotalSeconds);
            var fileContents = File.ReadAllBytes(filePath);
            int fiveMB = 1 * 1 * 1024;
            int partNumber = 1;
            var startPosition = 0;
            var bytesToBeUploaded = fileContents.Length;
            do
            {
                var bytesToUpload = Math.Min(fiveMB, bytesToBeUploaded);
                var partUploadUrl = new Uri(string.Format("{0}?uploadId={1}&partNumber={2}", requestUri.AbsoluteUri, HttpUtility.UrlEncode(uploadId), partNumber));
                var partUploadSignature = CreateSignature(secretKey, GetStringToSign(partUploadUrl, "PUT", string.Empty, string.Empty, expiry, null));
                var partUploadPreSignedUrl = new Uri(string.Format("{0}?uploadId={1}&partNumber={2}&AWSAccessKeyId={3}&Signature={4}&Expires={5}", requestUri.AbsoluteUri,
                    HttpUtility.UrlEncode(uploadId), partNumber, accessKey, HttpUtility.UrlEncode(partUploadSignature), expiry));
                var request = (HttpWebRequest)WebRequest.Create(partUploadPreSignedUrl);
                request.Method = "PUT";
                request.Timeout = 1000 * 600;
                request.ContentLength = bytesToUpload;
                
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(fileContents, startPosition, bytesToUpload);
                }
                using (var resp = (HttpWebResponse)request.GetResponse())
                {
                    using (var s = new StreamReader(resp.GetResponseStream()))
                    {
                        partNumberETags.Add(partNumber, resp.Headers["ETag"]);
                    }
                }
                bytesToBeUploaded = bytesToBeUploaded - bytesToUpload;
                startPosition = bytesToUpload;
                partNumber = partNumber + 1;

            }
            while (bytesToBeUploaded > 0);
            return partNumberETags;
        }

        public static void FinishMultipartUpload(string accessKey, string secretKey, Uri requestUri, string uploadId, Dictionary<int, string> partNumberETags, DateTime expiryDate)
        {
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan ts = new TimeSpan(expiryDate.Ticks - Jan1st1970.Ticks);
            var expiry = Convert.ToInt64(ts.TotalSeconds);
            var finishOrCancelMultipartUploadUri = new Uri(string.Format("{0}?uploadId={1}", requestUri.AbsoluteUri, uploadId));
            var signatureForFinishMultipartUpload = CreateSignature(secretKey, GetStringToSign(finishOrCancelMultipartUploadUri, "POST", string.Empty, "text/plain", expiry, null));
            var finishMultipartUploadUrl = new Uri(string.Format("{0}?uploadId={1}&AWSAccessKeyId={2}&Signature={3}&Expires={4}", requestUri.AbsoluteUri, HttpUtility.UrlEncode(uploadId), accessKey, HttpUtility.UrlEncode(signatureForFinishMultipartUpload), expiry));
            StringBuilder payload = new StringBuilder();
            payload.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?><CompleteMultipartUpload>");
            foreach (var item in partNumberETags)
            {
                payload.AppendFormat("<Part><PartNumber>{0}</PartNumber><ETag>{1}</ETag></Part>", item.Key, item.Value);
            }
            payload.Append("</CompleteMultipartUpload>");
            var requestPayload = Encoding.UTF8.GetBytes(payload.ToString());
            var request = (HttpWebRequest)WebRequest.Create(finishMultipartUploadUrl);
            request.Method = "POST";
            request.ContentType = "text/plain";
            request.ContentLength = requestPayload.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(requestPayload, 0, requestPayload.Length);
            }
            using (var resp = (HttpWebResponse)request.GetResponse())
            {
            }
        }
    }
}