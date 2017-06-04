using Dapper.FluentMap.Dommel.Mapping;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Data.Mapping
{
    /// <summary>
    /// Маппинг сущности "Научные кружки" на БД.
    /// </summary>
    public class ScienceGroupMap : DommelEntityMap<ScienceGroup>
    {
        public ScienceGroupMap()
        {
            ToTable("science_group");

            Map(e => e.ScienceGroupId)
                .IsKey()
                .IsIdentity()
                .ToColumn("science_group_id");
            Map(e => e.PlanId)
                .ToColumn("plan_id");
            Map(e => e.Name)
                .ToColumn("name");
            Map(e => e.Place)
                .ToColumn("place");
            Map(e => e.StudentsCount)
                .ToColumn("students_count");
            Map(e => e.PublicationsCount)
                .ToColumn("publications_count");
            Map(e => e.ConferencesCount)
                .ToColumn("conferences_count");
            Map(e => e.DiplomasCount)
                .ToColumn("diplomas_count");
        }
    }
}
