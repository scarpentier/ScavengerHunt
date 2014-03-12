using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScavengerHunt.Web.Models
{
    public class ScavengerHuntInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ScavengerHuntContext>
    {
        protected override void Seed(ScavengerHuntContext context)
        {
            var teams = new List<Team>
                            {
                                new Team() { Name = "Yves Corbeil" },
                                new Team() { Name = "Sloth" },
                                new Team() { Name = "CDP" }
                            };
            teams.ForEach(t => context.Teams.Add(t));
            context.SaveChanges();

            var stunts = new List<Stunt>
                             {
                                 new Stunt() { Title = "Ducktape", Description = "Ducktapez quelqu'un à sa chaise" },
                                 new Stunt() { Title = "LQJR is not dead", Description = "Envoyez une photo de Veers" }
                             };
            stunts.ForEach(s => context.Stunts.Add(s));
            context.SaveChanges();
        }
    }
}