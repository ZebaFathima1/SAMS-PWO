namespace StudentAcademicManagementSystem.Services;
public static class GradeService
{
    public static string Calculate(decimal total) => total switch
    {
        >= 90 => "A+", >= 80 => "A", >= 70 => "B", >= 60 => "C", >= 50 => "D", _ => "F"
    };
}
