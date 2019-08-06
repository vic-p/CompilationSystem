namespace PMY.CompilationSystem.Model.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PMY.CompilationSystem.Model.CompilationSystemEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PMY.CompilationSystem.Model.CompilationSystemEntities";
        }

        protected override void Seed(PMY.CompilationSystem.Model.CompilationSystemEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
