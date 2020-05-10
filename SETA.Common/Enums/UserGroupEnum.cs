namespace SETA.Common.Enums
{
    /// <summary>
    /// Security Levels:
    /// <para>100: Students</para>
    /// <para>200: Teachers</para>
    /// <para>300: School Administrators</para>
    /// <para>400: Admin - Super User</para>
    /// <para>500: Super Admin (God)</para>
    /// </summary>
    public enum UserGroupEnum
    {
        Student = 100,
        Teacher = 200,
        School = 300,
        AdminSuper = 400,
        Admin = 500
    }
}
