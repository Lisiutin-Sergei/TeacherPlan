using Dapper.FluentMap.Dommel.Mapping;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Data.Mapping
{
	/// <summary>
	/// Маппинг сущности "Хоздоговорная работа" на БД.
	/// </summary>
	public class ContractWorkMap : DommelEntityMap<ContractWork>
    {
        public ContractWorkMap()
        {
            ToTable("contract_work");

            Map(e => e.ContractWorkId)
                .IsKey()
                .IsIdentity()
                .ToColumn("contract_work_id");
            Map(e => e.PlanId)
                .ToColumn("plan_id");
			Map(e => e.Name)
				.ToColumn("name");
			Map(e => e.Type)
                .ToColumn("type");
			Map(e => e.Volume)
				.ToColumn("volume");
			Map(e => e.Duty)
				.ToColumn("duty");
			Map(e => e.Execution)
                .ToColumn("execution");
            Map(e => e.Comment)
                .ToColumn("comment");
        }
    }
}
