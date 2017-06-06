using Dapper.FluentMap.Dommel.Mapping;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Data.Mapping
{
	/// <summary>
	/// Маппинг сущности "Повышение квалификации" на БД.
	/// </summary>
	public class QualificationWorkMap : DommelEntityMap<QualificationWork>
    {
        public QualificationWorkMap()
        {
            ToTable("qualification_work");

            Map(e => e.QualificationWorkId)
                .IsKey()
                .IsIdentity()
                .ToColumn("qualification_work_id");
            Map(e => e.PlanId)
                .ToColumn("plan_id");
			Map(e => e.CourseName)
				.ToColumn("course_name");
			Map(e => e.CourseType)
                .ToColumn("course_type");
			Map(e => e.CourseVolume)
				.ToColumn("course_volume");
			Map(e => e.Place)
				.ToColumn("place");
			Map(e => e.Date)
                .ToColumn("date");
            Map(e => e.Execution)
                .ToColumn("execution");
            Map(e => e.Hours)
                .ToColumn("hours");
        }
    }
}
