using Dapper.FluentMap.Dommel.Mapping;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Data.Mapping
{
    /// <summary>
    /// Маппинг сущности "Учебно-методическая работа" на БД.
    /// </summary>
    public class EduMethodWorkMap : DommelEntityMap<EduMethodWork>
    {
        public EduMethodWorkMap()
        {
            ToTable("edu_method_work");

            Map(e => e.EduMethodWorkId)
                .IsKey()
                .IsIdentity()
                .ToColumn("edu_method_work_id");
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
