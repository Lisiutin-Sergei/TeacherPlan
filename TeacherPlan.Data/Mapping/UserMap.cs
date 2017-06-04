using Dapper.FluentMap.Dommel.Mapping;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Data.Mapping
{
    /// <summary>
    /// Маппинг сущности "Пользователь" на БД.
    /// </summary>
    public class UserMap : DommelEntityMap<User>
    {
        public UserMap()
        {
            ToTable("user_");

            Map(e => e.UserId)
                .IsKey()
                .IsIdentity()
                .ToColumn("user_id");
            Map(e => e.Name)
                .ToColumn("name");
            Map(e => e.Position)
                .ToColumn("position");
            Map(e => e.PasswordHash)
                .ToColumn("password");
            Map(e => e.Login)
                .ToColumn("login");
            Map(e => e.NameGenitive)
                .ToColumn("name_genitive");
            Map(e => e.AcademicDegree)
                .ToColumn("academic_degree");
            Map(e => e.AcademicRank)
                .ToColumn("academic_rank");
            Map(e => e.PositionVolume)
                .ToColumn("position_volume");
            Map(e => e.Department)
                .ToColumn("department");
            Map(e => e.PositionType)
                .ToColumn("position_type");
        }
    }
}
