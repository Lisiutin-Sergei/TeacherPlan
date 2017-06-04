﻿using Dapper.FluentMap.Dommel.Mapping;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Data.Mapping
{
    /// <summary>
    /// Маппинг сущности "Воспитательная и внеаудиторная и работа со студентами" на БД.
    /// </summary>
    public class ProfessionalWorkMap : DommelEntityMap<ProfessionalWork>
    {
        public ProfessionalWorkMap()
        {
            ToTable("professional_work");

            Map(e => e.ProfessionalWorkId)
                .IsKey()
                .IsIdentity()
                .ToColumn("professional_work_id");
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
