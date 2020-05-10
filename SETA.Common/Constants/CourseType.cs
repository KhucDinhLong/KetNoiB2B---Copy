using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETA.Common.Constants
{
    /// <summary>
    /// Link or File
    /// <para>1: Link</para>
    /// <para>2: File</para>
    /// <para>3: CourseFile Trial</para>
    /// </summary>
    public class CourseFileType
    {
        public const byte Link = 1;
        public const byte File = 2;
        public const byte CourseFileTrial = 3;
        public const byte LessonFileTrial = 4;
        public const byte CoursePracticeFile = 5;
        public const byte LessonYoutubeLink = 6;
        public const byte HomeworkVideoFile = 7;
        public const byte HomeworkLink = 8;
    }
    public class CourseDetailType
    {
        public const byte LessonContent = 1;
        public const byte LeftContent = 2;
        public const byte CourseContent = 3;
        public const byte CourseJson = 4;
    }

    public class CourseTypeID
    {
        public const byte Song = 3;
        public const byte Skill = 4;        
    }

}
