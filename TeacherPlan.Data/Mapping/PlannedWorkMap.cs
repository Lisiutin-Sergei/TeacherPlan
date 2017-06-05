using Dapper.FluentMap.Dommel.Mapping;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Data.Mapping
{
    /// <summary>
    /// Маппинг сущности "Воспитательная и внеаудиторная и работа со студентами" на БД.
    /// </summary>
    public class PlannedWorkMap : DommelEntityMap<PlannedWork>
    {
        public PlannedWorkMap()
        {
            ToTable("planned_work");

            Map(e => e.PlannedWorkId)
                .IsKey()
                .IsIdentity()
                .ToColumn("planned_work_id");
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
