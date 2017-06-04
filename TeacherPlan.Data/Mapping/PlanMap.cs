using Dapper.FluentMap.Dommel.Mapping;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Data.Mapping
{
    /// <summary>
    /// Маппинг сущности "План преподавателя" на БД.
    /// </summary>
    public class PlanMap : DommelEntityMap<Plan>
    {
        public PlanMap()
        {
            ToTable("plan");

            Map(e => e.PlanId)
                .IsKey()
                .IsIdentity()
                .ToColumn("plan_id");
            Map(e => e.Name)
                .ToColumn("name");
            Map(e => e.UserId)
                .ToColumn("user_id");
            Map(e => e.DateFrom)
                .ToColumn("date_from");
            Map(e => e.DateTo)
                .ToColumn("date_to");
            Map(e => e.PlanYear)
                .ToColumn("plan_year");
        }
    }
}
