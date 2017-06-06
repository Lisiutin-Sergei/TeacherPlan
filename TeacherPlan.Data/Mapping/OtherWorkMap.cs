using Dapper.FluentMap.Dommel.Mapping;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Data.Mapping
{
	/// <summary>
	/// Маппинг сущности "Прочие виды работ" на БД.
	/// </summary>
	public class OtherWorkMap : DommelEntityMap<OtherWork>
    {
        public OtherWorkMap()
        {
            ToTable("other_work");

            Map(e => e.OtherWorkId)
                .IsKey()
                .IsIdentity()
                .ToColumn("other_work_id");
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
