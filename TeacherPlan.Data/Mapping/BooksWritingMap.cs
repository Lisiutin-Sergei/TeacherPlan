using Dapper.FluentMap.Dommel.Mapping;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Data.Mapping
{
    /// <summary>
    /// Маппинг сущности "Написание учебников" на БД.
    /// </summary>
    public class BooksWritingMap : DommelEntityMap<BookWriting>
    {
        public BooksWritingMap()
        {
            ToTable("books_writing");

            Map(e => e.BookWritingId)
                .IsKey()
                .IsIdentity()
                .ToColumn("books_writing_id");
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
