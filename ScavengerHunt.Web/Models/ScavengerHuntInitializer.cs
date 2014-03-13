using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ScavengerHunt.Web.Models
{
    public class ScavengerHuntInitializer : System.Data.Entity.DropCreateDatabaseAlways<ScavengerHuntContext>
    {
        protected override void Seed(ScavengerHuntContext context)
        {
            // Create roles
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            RoleManager.Create(new IdentityRole("Admin"));
            RoleManager.Create(new IdentityRole("Judge"));

            // Create users
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var userAdmin = new ApplicationUser() { UserName = "admin" };
            UserManager.Create(userAdmin, "admin123");
            UserManager.AddToRole(userAdmin.Id, "Admin");
            var userJudge = new ApplicationUser() { UserName = "judge" };
            UserManager.Create(userJudge, "judge123");
            UserManager.AddToRole(userJudge.Id, "Judge");

            // Create mock data
            var teams = new List<Team>
                            {
                                new Team() { Name = "Yves Corbeil" },
                                new Team() { Name = "Sloth" },
                                new Team() { Name = "CDP" },
                                new Team() { Name = "Les Casseurs Flotteurs" }
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