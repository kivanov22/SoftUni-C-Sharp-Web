namespace Git.Controllers
{
    using Git.Data;
    using Git.Data.Models;
    using Git.ModelViews.Repository;
    using Git.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    using static Data.DataConstants;

    public class RepositoriesController : Controller
    {
        private readonly IValidator validator;
        private readonly ApplicationDbContext data;

        public RepositoriesController(
            IValidator validator,
             ApplicationDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }

        public HttpResponse All()
        {
            var repositoriesAll = this.data
                .Repositories
                .AsQueryable();

            if (this.User.IsAuthenticated)
            {
                repositoriesAll = repositoriesAll.Where(x => x.IsPublic || x.OwnerId == this.User.Id);
            }
            else
            {
                repositoriesAll = repositoriesAll.Where(x => x.IsPublic);
            }

            var repositories = repositoriesAll
                .OrderByDescending(x => x.CreatedOn)
                .Select(r => new RepositoryListingViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Owner= r.Owner.Username,
                    CreatedOn= r.CreatedOn.ToLocalTime().ToString("F"),
                    Commits = r.Commits.Count(),

                })
                .ToList();

            return View(repositories);
        }

        [Authorize]
        public HttpResponse Create() => View();

        [Authorize]
        [HttpPost]
        public HttpResponse Create(CreateRepositoryFormModel model)
        {
            var modelErrors = this.validator.ValidateRepository(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var repository = new Repository
            {
                Name = model.Name,
                IsPublic = model.RepositoryType == RepositoryPublic,
                OwnerId = this.User.Id,
            };

            data.Repositories.Add(repository);
            data.SaveChanges();

            return Redirect("/Repositories/All");
        }
    }
}
