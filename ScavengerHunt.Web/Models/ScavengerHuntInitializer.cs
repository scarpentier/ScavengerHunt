using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                                 new Stunt() { MaxScore = 15, Type = StuntTypeEnum.Flag, Translations = new Collection<StuntTranslation>()
                                                                                                            {
                                                                                                                new StuntTranslation() { Language = "fr", Title = "Trouvez le secret caché dans le fichier" },
                                                                                                                new StuntTranslation() { Title = "Find the hidden secret in the file" }
                                                                                                            }},
                                 new Stunt() { MaxScore = 20, Type = StuntTypeEnum.Live, Translations = new Collection<StuntTranslation>()
                                                                                                            {
                                                                                                                new StuntTranslation() { Language = "fr", Title = "Ducktape", Description = "Ducktapez quelqu'un à sa chaise" },
                                                                                                                new StuntTranslation() { Language = "en", Title = "Ducktape", Description = "Ducktape someone to its chair" }
                                                                                                            }},
                                 new Stunt() { MaxScore = 10, Type = StuntTypeEnum.Photo, Translations = new Collection<StuntTranslation>()
                                                                                                             {
                                                                                                                 new StuntTranslation() { Language = "fr", Title = "LQJR n'est pas mort", Description = "Envoyez une photo de Veers" },
                                                                                                                 new StuntTranslation() { Language = "en", Title = "LQJR is not dead", Description = "Send us a picture of you with Veers" }
                                                                                                             }},
                                 new Stunt() { MaxScore = 10, Type = StuntTypeEnum.Url, Translations = new Collection<StuntTranslation>()
                                                                                                           {
                                                                                                               new StuntTranslation() { Language = "fr", Title = "Aimer le jambon", Description = "Mettez une image de votre équipe sur jambon.ca affichant fièrement le jambon." },
                                                                                                               new StuntTranslation() { Language = "en", Title = "Love ham", Description = "Send a picture of your team on jambon.ca"}
                                                                                                           }},
                                 new Stunt() { MaxScore = 5, Type = StuntTypeEnum.Photo, Translations = new Collection<StuntTranslation>()
                                                                                                            {
                                                                                                                new StuntTranslation() { Language = "fr", Title = "Risquer sa vie", Description = "Photo avec le légendaire Xzcute" },
                                                                                                                new StuntTranslation() { Language = "en", Title = "Risk your life", Description = "Picture with legendary Xzcute" }
                                                                                                            }},
                                 new Stunt() { MaxScore = 10, Type = StuntTypeEnum.Video, Translations = new Collection<StuntTranslation>()
                                                                                                             {
                                                                                                                 new StuntTranslation() { Language = "fr", Title = "Zombies", Description = "Faites un reportage sur les Gamers Zombies" },
                                                                                                                 new StuntTranslation() { Language = "en", Title = "Zombies", Description = "Do a news report on Zombie Gamers" }
                                                                                                             }}
                             };

            stunts.ForEach(s => context.Stunts.Add(s));

            context.SaveChanges();
        }
    }
}