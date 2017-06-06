using Dapper.FluentMap.Dommel.Mapping;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Data.Mapping
{
	/// <summary>
	/// Маппинг сущности "Работа над диссертацией" на БД.
	/// </summary>
	public class DissertationWorkMap : DommelEntityMap<DissertationWork>
    {
        public DissertationWorkMap()
        {
            ToTable("dissertation_work");

            Map(e => e.DissertationWorkId)
                .IsKey()
                .IsIdentity()
                .ToColumn("dissertation_work_id");
            Map(e => e.PlanId)
                .ToColumn("plan_id");
            Map(e => e.Name)
                .ToColumn("name");
            Map(e => e.Date)
                .ToColumn("date");
            Map(e => e.Execution)
                .ToColumn("execution");
            Map(e => e.Hours)
                .ToColumn("hours");
        }
    }
}
