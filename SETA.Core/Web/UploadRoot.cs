using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SETA.Common.Constants;

namespace SETA.Core.Web
{
    public class UploadRoot
    {
        public static string GetUploadPath(string teacher, string course, string lesson)
        {
            if (lesson == null || lesson.Equals("") || lesson.Equals("0"))
            {
                return GetUploadPath(teacher, course);
            }
            else
                return string.Format(FolderUpload.LessonUpload,  teacher,  course,   lesson); 
        }
        public static string GetUploadPath(string teacher, string course)
        {
            return string.Format(FolderUpload.CourseNoLesson, teacher,  course==null? "0": course);
        }

        public static string GetUploadPath(string teacher, string course, string lesson, string homework)
        {
            if (homework == null || homework.Equals("") || homework.Equals("0"))
            {
                return GetUploadPath(teacher, course, lesson);
            }
            else
                return string.Format(FolderUpload.HomeWorkUpload, teacher, course == null ? "0" : course, lesson, homework); 
        }

    }
}
