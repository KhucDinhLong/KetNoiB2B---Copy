namespace SETA.Common.Constants
{
    public class FolderUpload
    {
        public const string Image = "/Image";
        public const string ImagePreupload = "/Image/PreUpload";
        public const string HomeWorkUpload = "Uploads/TZ/Courses/{0}/{1}/{2}/{3}";
        public const string LessonUpload = "Uploads/TZ/Courses/{0}/{1}/{2}";
        public const string CourseNoLesson = "Uploads/TZ/Courses/{0}/{1}/NoLesson";
        public const string Thumbnails = "/Uploads/Thumbnails";
        public const string PDFFile = "/Uploads/PDFFile";
        public const string PDFFileExport = "/Uploads/PDFFileExport";
        public const string FilePayment = "/FilePayment";
    }

    public class ConvertFileAmazon
    {
        public const string PIPELINE_ID_QA = "1445505143830-z8e10m"; //QA Site
        public const string PIPELINE_ID_STAGING = "1461693910103-28kmbw"; //Staging Site
        public const string PIPELINE_ID_LIVE = "1447330585164-mv384w"; //Production Site
        public const string PRESET_ID = "1445505625158-y6ot1h";
        public const string SNS_TOPIC = "arn:aws:sns:us-west-1:304605616652:Elastic_Transcoder";
        public const string AMAZONS3_BUCKET = "teacherzones-media-files";
        public const int PROCESS = 1;
        public const int READY = 2;
        public const int TRANSCODE_FAILED = 4;
        public const int WAITING_UPLOADER = 5;
        public const int FAIL_UPLOADER = 6;
    }
    
}
