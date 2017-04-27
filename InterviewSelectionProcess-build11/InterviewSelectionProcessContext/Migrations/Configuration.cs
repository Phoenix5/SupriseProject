namespace Interview.Domain.EF.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Interview.Domain.Model.InterviewSelectionProcessContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Interview.Domain.Model.InterviewSelectionProcessContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Posts.AddOrUpdate(
                p => p.PostName,
                new Model.Post { PostName = "Junior developer" },
                new Model.Post { PostName = "Developer" },
                new Model.Post { PostName = "Senior developer" }
              );
            context.Technologies.AddOrUpdate(
                  p => p.TechnologyName,
                  new Model.Technology { TechnologyName = "ASP.NET" },
                  new Model.Technology { TechnologyName = "Node.JS" },
                  new Model.Technology { TechnologyName = "MongoDB" }
                );
            context.InterviewStatuses.AddOrUpdate(
                  p => p.InterviewStatusName,
                  new Model.InterviewStatus { InterviewStatusName = "New" },
                  new Model.InterviewStatus { InterviewStatusName = "Scheduled" },
                  new Model.InterviewStatus { InterviewStatusName = "Round 1" },
                  new Model.InterviewStatus { InterviewStatusName = "Round 2" },
                  new Model.InterviewStatus { InterviewStatusName = "Round 3" }
                );
            context.SelectionStatuses.AddOrUpdate(
                  p => p.SelectionStatusName,
                  new Model.SelectionStatus { SelectionStatusName = "Not screened" },
                  new Model.SelectionStatus { SelectionStatusName = "Screening in progress" },
                  new Model.SelectionStatus { SelectionStatusName = "Shortlisted" },
                  new Model.SelectionStatus { SelectionStatusName = "Offer letter given" },
                  new Model.SelectionStatus { SelectionStatusName = "Offer letter accepted" }
                );
        }
    }
}
