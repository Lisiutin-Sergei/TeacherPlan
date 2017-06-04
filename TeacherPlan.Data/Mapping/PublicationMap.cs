using Dapper.FluentMap.Dommel.Mapping;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Data.Mapping
{
    /// <summary>
    /// Маппинг сущности "Печатные (научные) труды за год" на БД.
    /// </summary>
    public class PublicationMap : DommelEntityMap<Publication>
    {
        public PublicationMap()
        {
            ToTable("publication");

            Map(e => e.PublicationId)
                .IsKey()
                .IsIdentity()
                .ToColumn("publication_id");
            Map(e => e.PlanId)
                .ToColumn("plan_id");
            Map(e => e.IsPublished)
                .ToColumn("is_published");
            Map(e => e.Coauthors)
                .ToColumn("coauthors");
            Map(e => e.Volume)
                .ToColumn("volume");
            Map(e => e.Name)
                .ToColumn("name");
        }
    }
}
