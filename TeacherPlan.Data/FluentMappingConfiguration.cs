using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using TeacherPlan.Data.Mapping;

namespace TeacherPlan.Data
{
    /// <summary>
    /// Конфигурация Fluent Mapping для всех сущностей проекта.
    /// </summary>
    public static class FluentMappingConfiguration
    {
        public static void ConfigureMapping()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new UserMap());
                config.AddMap(new PlanMap());
                config.AddMap(new EducationalWorkMap());
                config.AddMap(new EducationalWorkTypeMap());
                config.AddMap(new EduMethodWorkMap());
                config.AddMap(new BooksWritingMap());
                config.AddMap(new BooksPublishingMap());
                config.AddMap(new StateBudgetWorkMap());
                config.AddMap(new ScienceGroupMap());
                config.AddMap(new StudentResearchMap());
                config.AddMap(new PublicationMap());
                config.AddMap(new TrainingWorkMap());
                config.AddMap(new ProfessionalWorkMap());
                config.AddMap(new PlannedWorkMap());

                config.ForDommel();
            });
        }
    }
}
