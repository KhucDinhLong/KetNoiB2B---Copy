using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.ElasticTranscoder;
using Amazon.ElasticTranscoder.Model;
using SETA.Common.Constants;

namespace SETA.Common.Helper
{
    public class TranscoderHelper
    {
        public static void AmazonTrancoder(string tokenFile, string pathUpload)
        {
            var stransCode = new AmazonElasticTranscoderClient(RegionEndpoint.USWest1);

            var fileName = pathUpload.Split('/').Last();
            var rootPath = Common.Utility.Utils.GetSetting(AppKeys.FOLDER_UPLOAD_S3_SITE, "");

            var jobInput = new JobInput
            {
                AspectRatio = "auto",
                Container = "mp4",
                FrameRate = "auto",
                Interlaced = "auto",
                Resolution = "auto",
                Key = pathUpload
            };
            var outPut = new CreateJobOutput
            {
                ThumbnailPattern = rootPath + "Thumnails/" + fileName + "_{count}",
                Rotate = "auto",
                PresetId = ConvertFileAmazon.PRESET_ID,
                Key = rootPath + "encodes/" + fileName
            };

            Dictionary<string, string> FileInfor = new Dictionary<string, string>();

            FileInfor.Add("TokenCourseFile", tokenFile);

            var PipeLine_ID = ConvertFileAmazon.PIPELINE_ID_QA;
            var environment = Common.Utility.Utils.GetSetting(AppKeys.CURRENT_ENVIRONMENT, EnvironmentData.Dev);
            if (string.CompareOrdinal(environment, EnvironmentData.Live) == 0)
            {
                PipeLine_ID = ConvertFileAmazon.PIPELINE_ID_LIVE;
            }
            else if (string.CompareOrdinal(environment, EnvironmentData.Staging) == 0)
            {
                PipeLine_ID = ConvertFileAmazon.PIPELINE_ID_STAGING;
            }

            var createJob = new CreateJobRequest
            {
                Input = jobInput,
                Output = outPut,
                PipelineId = PipeLine_ID,
                UserMetadata = FileInfor
            };

            stransCode.CreateJob(createJob);
        }
    }

    public class TrancodeError
    {
        public static dynamic ErrorTrancoder(int code)
        {
            dynamic Error = new ExpandoObject();
            switch (code)
            {
                case 1000:
                    Error.messageDetails = "Validation Error";
                    Error.Cause = "While processing the job, Elastic Transcoder determined that one or more values in the request were invalid.";
                    break;
                case 1001:
                    Error.messageDetails = "Dependency Error";
                    Error.Cause = "Elastic Transcoder could not generate the playlist because it encountered an error with one or more of the playlists dependencies.";
                    break;
                case 2000:
                    Error.messageDetails = "Cannot Assume Role";
                    Error.Cause = "Elastic Transcoder cannot assume the AWS Identity and Access Management role that is specified in the Role object in the pipeline for this job.";
                    break;
                case 3000:
                    Error.messageDetails = "Unclassified Storage Error";
                    Error.Cause = "";
                    break;
                case 3001:
                    Error.messageDetails = "Input Does Not Exist";
                    Error.Cause = "No file exists with the name that you specified in the Input:Key object for this job. The file must exist in the Amazon S3 bucket that is specified in the InputBucket object in the pipeline for this job.";
                    break;
                case 3002:
                    Error.messageDetails = "Output Already Exists";
                    Error.Cause = "A file already exists with the name that you specified in the Outputs:Key (or Output:Key) object for this job. The file cannot exist in the Amazon S3 bucket that is specified in the OutputBucket object in the pipeline for this job.";
                    break;
                case 3003:
                    Error.messageDetails = "Cannot Assume Role";
                    Error.Cause = "Elastic Transcoder cannot assume the AWS Identity and Access Management role that is specified in the Role object in the pipeline for this job.";
                    break;
                case 3004:
                    Error.messageDetails = "Does Not Have Write Permission";
                    Error.Cause = "The IAM role specified in the Role object in the pipeline that you used for this job doesn't have permission to write to the Amazon S3 bucket in which you want to save either transcoded files or thumbnail files.";
                    break;
                case 3005:
                    Error.messageDetails = "Bucket Does Not Exist";
                    Error.Cause = "The specified S3 bucket does not exist: bucket={1}.";
                    break;
                case 3006:
                    Error.messageDetails = "Does Not Have Write Permission";
                    Error.Cause = "Elastic Transcoder was unable to write the key={1} to bucket={2}, as the key is not in the same region as the bucket";
                    break;
                case 4000:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "The file that you specified in the Input:Key object for this job is in a format that is currently not supported by Elastic Transcoder.";
                    break;
                case 4001:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "The width x height of the file that you specified in the Input:Key object for this job exceeds the maximum allowed width x height.";
                    break;
                case 4002:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "The file size of the file that you specified in the Input:Key object for this job exceeds the maximum allowed size.";
                    break;
                case 4003:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Elastic Transcoder couldn't interpret the file that you specified in one of the Outputs:Watermarks:InputKey objects for this job.";
                    break;
                case 4004:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "The width x height of a file that you specified in one of the Outputs:Watermarks:InputKey objects for this job exceeds the maximum allowed width x height.";
                    break;
                case 4005:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "The size of a file that you specified for one of the {1} objects exceeds the maximum allowed size: bucket={2}, key={3}, size{4}, max size={5}.";
                    break;
                case 4006:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Elastic Transcoder could not transcode the input file because the format is not supported.";
                    break;
                case 4007:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Elastic Transcoder encountered a file type that is generally supported, but was unable to process the file correctly. This error automatically opened a support case, and we have started to research the cause of the problem.";
                    break;
                case 4008:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "The underlying cause of this is a mismatch between the preset and the input file. Examples include: The preset includes audio settings, but the input file lacks audio. The preset includes video settings, but the input file lacks video.";
                    break;
                case 4009:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Elastic Transcoder was unable to insert all of your album art into the output file because you exceeded the maximum number of artwork streams.";
                    break;
                case 4010:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Elastic Transcoder could not interpret the graphic file you specified for AlbumArt:Artwork:InputKey.";
                    break;
                case 4011:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Elastic Transcoder detected an embedded artwork stream, but could not interpret it.";
                    break;
                case 4012:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "The image that you specified for AlbumArt:Artwork exceeds the maximum allowed width x height: 4096 x 3072.";
                    break;
                case 4013:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "The width x height of the embedded artwork exceeds the maximum allowed width x height: 4096 x 3072.";
                    break;
                case 4014:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "The value that you specified for starting time of a clip is after the end of the input file. Elastic Transcoder could not create an output file.";
                    break;
                case 4015:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Elastic Transcoder could not generate a manifest file because the generated segments did not match.";
                    break;
                case 4016:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Elastic Transcoder could not decrypt the input file from {1} using {2}.";
                    break;
                case 4017:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "The AES key was encrypted with a {2}-bit encryption key. AES supports only 128-, 192-, and 256-bit encryption keys. MD5={1}.";
                    break;
                case 4018:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Elastic Transcoder was unable to decrypt the ciphered key with MD5={1}";
                    break;
                case 4019:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Elastic Transcoder was unable to generate a data key using the KMS key ARN {0}.";
                    break;
                case 4020:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Your key must be 128 bits for AES-128 encryption. MD5={1}, {2} bits.";
                    break;
                case 4021:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Your key must be 128 bits for PlayReady DRM. MD5={1}, strength={2} bits.";
                    break;
                case 4100:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Elastic Transcoder detected an embedded caption track but could not interpret it.";
                    break;
                case 4101:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Elastic Transcoder could not interpret the specified caption file for Amazon S3 bucket={1}, key={2}.";
                    break;
                case 4102:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Elastic Transcoder could not interpret the specified caption file since it was not UTF-8 encoded: Amazon S3 bucket={1}, key={2}.";
                    break;
                case 4103:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Elastic Transcoder was unable to process all of your caption tracks because you exceeded {1}, the maximum number of caption tracks.";
                    break;
                case 4104:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Elastic Transcoder could not generate a master playlist because the desired output contains {1} embedded captions, when the maximum is 4.";
                    break;
                case 4105:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Elastic Transcoder cannot embed your caption tracks because frame rate {1} is not supported for CEA-708 - only frame rates [29.97, 30] are supported.";
                    break;
                case 4106:
                    Error.messageDetails = "Bad Input File";
                    Error.Cause = "Elastic Transcoder cannot embed your caption tracks because format {1} supports only {2} caption track(s).";
                    break;
                case 9000:
                    Error.messageDetails = "Internal Service Error";
                    Error.Cause = "";
                    break;
                case 9001:
                    Error.messageDetails = "Internal Service Error";
                    Error.Cause = "";
                    break;
                case 9999:
                    Error.messageDetails = "Internal Service Error";
                    Error.Cause = "";
                    break;
            }
            return Error;
        }
    }
}
