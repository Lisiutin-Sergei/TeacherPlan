using Dapper.FluentMap.Dommel.Mapping;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Data.Mapping
{
    /// <summary>
    /// Маппинг сущности "Тип учебной работы" на БД.
    /// </summary>
    public class EducationalWorkTypeMap : DommelEntityMap<EducationalWorkType>
    {
        public EducationalWorkTypeMap()
        {
            ToTable("educational_work_type");

            Map(e => e.EducationalWorkTypeId)
                .IsKey()
                .IsIdentity()
                .ToColumn("educational_work_type_id");
            Map(e => e.Name)
                .ToColumn("name");
        }
    }
}
