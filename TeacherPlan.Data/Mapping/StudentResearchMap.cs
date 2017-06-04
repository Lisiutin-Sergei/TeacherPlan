using Dapper.FluentMap.Dommel.Mapping;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Data.Mapping
{
    /// <summary>
    /// Маппинг сущности "Руководство научной исследовательской работой студентов" на БД.
    /// </summary>
    public class StudentResearchMap : DommelEntityMap<StudentResearch>
    {
        public StudentResearchMap()
        {
            ToTable("students_research");

            Map(e => e.StudentResearchId)
                .IsKey()
                .IsIdentity()
                .ToColumn("students_research_id");
            Map(e => e.PlanId)
                .ToColumn("plan_id");
            Map(e => e.StudentName)
                .ToColumn("student_name");
            Map(e => e.StudentGroup)
                .ToColumn("student_group");
            Map(e => e.OopCode)
                .ToColumn("oop_code");
            Map(e => e.Research)
                .ToColumn("research");
            Map(e => e.Execution)
                .ToColumn("execution");
        }
    }
}
