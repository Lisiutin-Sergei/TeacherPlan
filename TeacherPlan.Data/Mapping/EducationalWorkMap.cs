using Dapper.FluentMap.Dommel.Mapping;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Data.Mapping
{
    /// <summary>
    /// Маппинг сущности "Учебная работа" на БД.
    /// </summary>
    public class EducationalWorkMap : DommelEntityMap<EducationalWork>
    {
        public EducationalWorkMap()
        {
            ToTable("educational_work");

            Map(e => e.EducationalWorkId)
                .IsKey()
                .IsIdentity()
                .ToColumn("educational_work_id");
            Map(e => e.EducationalWorkTypeId)
                .ToColumn("educational_work_type_id");
            Map(e => e.PlanId)
                .ToColumn("plan_id");
            Map(e => e.Name)
                .ToColumn("name");
            Map(e => e.FirstSemesterPlan)
                .ToColumn("first_semester_plan");
            Map(e => e.FirstSemesterFact)
                .ToColumn("first_semester_fact");
            Map(e => e.SecondSemesterPlan)
                .ToColumn("second_semester_plan");
            Map(e => e.SecondSemesterFact)
                .ToColumn("second_semester_fact");
        }
    }
}
