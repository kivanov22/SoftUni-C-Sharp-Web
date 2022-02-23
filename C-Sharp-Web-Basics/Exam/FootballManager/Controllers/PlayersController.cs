using FootballManager.Data;
using FootballManager.Data.Models;
using FootballManager.Services;
using FootballManager.ViewModels.Players;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace FootballManager.Controllers
{
    using static Data.DataConstants;
    public class PlayersController:Controller
    {
        private readonly IValidator validator;
        private readonly FootballManagerDbContext data;

        public PlayersController(
             IValidator validator,
             FootballManagerDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }



        [Authorize]
        public HttpResponse All()
        {
           

            if (!User.IsAuthenticated)
            {
                return Unauthorized();
            }

            var players = this.data.Players
                .Select(p => new PlayerListingViewModel
                {
                    Id = p.Id,
                    FullName =p.FullName,
                    ImageUrl = p.ImageUrl,
                    Position =p.Position,
                    Speed =p.Speed.ToString(),
                    Endurance = p.Endurance.ToString(),
                    Description =p.Description
                })
                .ToList();


            return View(players);
        }

        public HttpResponse Add() => View();
            
        [Authorize]
        [HttpPost]
        public HttpResponse Add(PlayerFormModel model)
        {
            var modelErrors = this.validator.ValidatePlayer(model);


            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var player = new Player
            {
                FullName = model.FullName,
                ImageUrl =model.ImageUrl,
                Position = model.Position,
                Speed = model.Speed,
                Endurance = model.Endurance,
                Description = model.Description
            };

            this.data.Players.Add(player);
            this.data.SaveChanges();

            return Redirect("/Players/All");
        }

        [Authorize]
        public HttpResponse Collection()
        {

            //string userId) => this.Data.UserPlayers.Where(x => x.UserId == userId).Select(x => x.Player);
            //var user = this.data.Players
            //    .Where(u => u.Id == this.User.Id)
            //    .FirstOrDefault();

            //var userCollection = this.data.UserPlayers.Where(x => x.UserId == this.User.Id).Select(x => x.Player).ToList();

            var myCollection = this.data.Players
                //.Where(p => p.Id == playerId)
                .Select(c => new PlayerListingViewModel
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    ImageUrl = c.ImageUrl,
                    Endurance = c.Endurance.ToString(),
                    Position = c.Position,
                    Speed = c.Speed.ToString(),
                    Description = c.Description,
                }).ToList();

            if (myCollection == null)
            {
                return NotFound();
            }

            return View(myCollection);
        }

        public HttpResponse AddPlayerToCollection(int playerId, string userId)
        {
            var user = this.data.Users
                .Where(p => p.Id == this.User.Id)
                .FirstOrDefault();

            var player = this.data.Players.Where(p => p.Id == playerId)
                .FirstOrDefault();

            var playersOfUser = new UserPlayer
            {
                UserId = userId,
                PlayerId = playerId,
                User = user,
                Player = player
            };

            this.data.UserPlayers.Add(playersOfUser);
            this.data.SaveChanges();

            return View(playersOfUser);
        }



        [Authorize]
        public HttpResponse Delete(int playerId)
        {
            if (!this.User.IsAuthenticated)
            {
                return Unauthorized();
            }


            //var userCard = this.Data.UserPlayers.FirstOrDefault(x => x.UserId == userId && x.PlayerId == playerId);

            //this.Data.UserPlayers.Remove(userCard);

            //this.Data.SaveChanges();

            //  var player = this.data.Players.Find(playerId);
            var player = this.data.UserPlayers.Where(p => p.PlayerId == playerId && p.UserId==this.User.Id).FirstOrDefault();
            this.data.UserPlayers.Remove(player);
            this.data.SaveChanges();


            return Redirect("/Players/Collection");
        }


    }
}
