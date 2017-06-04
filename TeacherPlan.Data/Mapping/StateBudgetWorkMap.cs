using Dapper.FluentMap.Dommel.Mapping;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Data.Mapping
{
    /// <summary>
    /// Маппинг сущности "Госбюджетная работа" на БД.
    /// </summary>
    public class StateBudgetWorkMap : DommelEntityMap<StateBudgetWork>
    {
        public StateBudgetWorkMap()
        {
            ToTable("state_budget_work");

            Map(e => e.StateBudgetWorkId)
                .IsKey()
                .IsIdentity()
                .ToColumn("state_budget_work_id");
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
            Map(e => e.Execution)
                .ToColumn("execution");
        }
    }
}
