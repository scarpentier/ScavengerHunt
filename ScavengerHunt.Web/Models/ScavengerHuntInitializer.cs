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
            // Base configuration
            var configuration = new List<Setting>
                                    {
                                        new Setting() {
                                            Key = "ShowKeyword",
                                            Value = "true",
                                            Description = "Determines wether to show the Stunt Keyword to the users"
                                        },
                                        new Setting() {
                                            Key = "ShowTitle",
                                            Value = "true",
                                            Description = "Determines wether to show the Stunt Title to the users"
                                        },
                                        new Setting() {
                                            Key = "AllowStuntRetry",
                                            Value = "true",
                                            Description = "Allows teams to retry a stunt already done to increase points if they are unhappy with the judging"
                                        },
                                        new Setting() {
                                            Key = "EnableUserRegistration",
                                            Value = "true",
                                            Description = "Allows user registration"
                                        },
                                        new Setting() {
                                            Key = "EnableTeamRegistration",
                                            Value = "true",
                                            Description = "Allows team registration"
                                        },
                                        new Setting() {
                                            Key = "EnabledTeamJoining",
                                            Value = "true",
                                            Description = "Allows team joining"
                                        },
                                        new Setting() {
                                            Key = "GuestStuntsVisible",
                                            Value = "true",
                                            Description = "Allows guests to see stunts"
                                        },
                                        new Setting() {
                                            Key = "GuestTeamsVisible",
                                            Value = "true",
                                            Description = "Allows guests to see teams"
                                        },
                                        new Setting() {
                                            Key = "GuestSummaryVisible",
                                            Value = "false",
                                            Description = "Makes the TeamStunts Summary visible to guests: useful to display all the stunts done by all the teams at the end of the event. Accessible though /TeamStunt/Summary"
                                        },
                                        new Setting()
                                        {
                                            Key = "GuideUrl",
                                            Value = "https://gist.github.com/scarpentier/acaa5c0502ebb7761d27",
                                            Description = "URL for helpful guide and other instructions"
                                        }
                                    };

            context.Settings.AddRange(configuration);

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
                                 new Stunt() { Keyword = "Markdown", MaxScore = 5, Type = StuntTypeEnum.Text, Translations = new Collection<StuntTranslation>()
                                                                                                            {
                                                                                                                new StuntTranslation() { Language = "en", Title = "Support for Markdown!", Description = "You red that **right**! You can use [Markdown syntax](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet) right here!"}
                                                                                                            }},
                                 new Stunt() { Keyword = "Flag", MaxScore = 10, Type = StuntTypeEnum.Flag, JudgeNotes = "flag1234", Translations = new Collection<StuntTranslation>()
                                                                                                            {
                                                                                                                new StuntTranslation() { Language = "en", Title = "Support for flags!", Description = "Flags is a special kind of stunt that's judged automatically. Just set the answer in the Judge Notes! This one is \"flag1324\"" }
                                                                                                            }},
                                 new Stunt() { Keyword = "Zelda", MaxScore = 15, Type = StuntTypeEnum.Flag, JudgeNotes = "Potato", Translations = new Collection<StuntTranslation>()
                                                                                                            {
                                                                                                                new StuntTranslation() { Language = "fr", Title = "Trouvez le secret caché dans le fichier" },
                                                                                                                new StuntTranslation() { Title = "Find the hidden secret in the file" }
                                                                                                            }},
                                 new Stunt() { Keyword = "Ducktape", MaxScore = 20, Type = StuntTypeEnum.Live, Translations = new Collection<StuntTranslation>()
                                                                                                            {
                                                                                                                new StuntTranslation() { Language = "fr", Title = "Ducktape", Description = "Ducktapez quelqu'un à sa chaise" },
                                                                                                                new StuntTranslation() { Language = "en", Title = "Ducktape", Description = "Ducktape someone to its chair" }
                                                                                                            }},
                                 new Stunt() { Keyword = "Weird", MaxScore = 10, Type = StuntTypeEnum.Url, Translations = new Collection<StuntTranslation>()
                                                                                                           {
                                                                                                               new StuntTranslation() { Language = "fr", Title = "Endroit étrange", Description = "Partagez-nous l'URL de votre site web louche favori." },
                                                                                                               new StuntTranslation() { Language = "en", Title = "Weird place", Description = "Share us the URL for your favorite wierd web site."}
                                                                                                           }},
                                 new Stunt() { Keyword = "SPACEBAR", MaxScore = 5, Type = StuntTypeEnum.Picture, Translations = new Collection<StuntTranslation>()
                                                                                                            {
                                                                                                                new StuntTranslation() { Language = "fr", Title = "Temple de la renommée", Description = "Prenez une photo avec le légendaire SPACEBAR" },
                                                                                                                new StuntTranslation() { Language = "en", Title = "Hall of fame", Description = "Take a picture with the legendary SPACEBAR" }
                                                                                                            }},
                                 new Stunt() { Keyword = "Zombies", MaxScore = 10, Type = StuntTypeEnum.Video, Translations = new Collection<StuntTranslation>()
                                                                                                             {
                                                                                                                 new StuntTranslation() { Language = "fr", Title = "Zombies", Description = "Faites un reportage sur les Gamers Zombies" },
                                                                                                                 new StuntTranslation() { Language = "en", Title = "Zombies", Description = "Do a news report on Zombie Gamers" }
                                                                                                             }}
                             };

            stunts.ForEach(s => context.Stunts.Add(s));

            context.SaveChanges();

            base.Seed(context);
        }
    }
}