using Dapper.FluentMap.Dommel.Mapping;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Data.Mapping
{
	/// <summary>
	/// Маппинг сущности "Дополнительная образовательная деятельность" на БД.
	/// </summary>
	public class AdditionalWorkMap : DommelEntityMap<AdditionalWork>
    {
        public AdditionalWorkMap()
        {
            ToTable("additional_work");

            Map(e => e.AdditionalWorkId)
                .IsKey()
                .IsIdentity()
                .ToColumn("additional_work_id");
            Map(e => e.PlanId)
                .ToColumn("plan_id");
			Map(e => e.Name)
				.ToColumn("name");
			Map(e => e.Students)
                .ToColumn("students");
			Map(e => e.Place)
				.ToColumn("place");
			Map(e => e.Program)
                .ToColumn("program");
            Map(e => e.EducationType)
                .ToColumn("education_type");
			Map(e => e.Volume)
				.ToColumn("volume");
		}
    }
}
