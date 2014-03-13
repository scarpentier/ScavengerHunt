using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
                                 new Stunt() { Title = "Trouvez le secret caché dans le fichier", MaxScore = 15, Type = StuntTypeEnum.Flag },
                                 new Stunt() { Title = "Ducktape", Description = "Ducktapez quelqu'un à sa chaise", MaxScore = 20, Type = StuntTypeEnum.Live },
                                 new Stunt() { Title = "LQJR is not dead", Description = "Envoyez une photo de Veers", MaxScore = 10, Type = StuntTypeEnum.Url },
                                 new Stunt() { Title = "Aimer le jambon", Description = "Mettez une image de votre équipe sur jambon.ca affichant fièrement le jambon.", MaxScore = 10, Type = StuntTypeEnum.Url },
                                 new Stunt() { Title = "Risquer sa vie", Description = "Photo avec le légendaire Xzcute", MaxScore = 5, Type = StuntTypeEnum.Url },
                                 new Stunt() { Title = "Zombies", Description = "Faites un reportage sur les Gamers Zombies", MaxScore = 10, Type = StuntTypeEnum.Url }
                             };
            stunts.ForEach(s => context.Stunts.Add(s));

            context.SaveChanges();
        }
    }
}