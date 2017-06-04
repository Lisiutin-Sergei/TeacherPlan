using Dapper.FluentMap.Dommel.Mapping;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Data.Mapping
{
    /// <summary>
    /// Маппинг сущности "Издано учебников" на БД.
    /// </summary>
    public class BooksPublishingMap : DommelEntityMap<BooksPublishing>
    {
        public BooksPublishingMap()
        {
            ToTable("books_publishing");

            Map(e => e.BooksPublishingId)
                .IsKey()
                .IsIdentity()
                .ToColumn("books_publishing_id");
            Map(e => e.PlanId)
                .ToColumn("plan_id");
            Map(e => e.Output)
                .ToColumn("output");
            Map(e => e.Purpose)
                .ToColumn("purpose");
            Map(e => e.Coauthors)
                .ToColumn("coauthors");
            Map(e => e.Volume)
                .ToColumn("volume");
        }
    }
}
