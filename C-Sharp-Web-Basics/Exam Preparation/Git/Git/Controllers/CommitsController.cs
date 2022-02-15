namespace Git.Controllers
{
    using Git.Data;
    using Git.Data.Models;
    using Git.ModelViews.Commit;
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    using static Data.DataConstants;
    public class CommitsController : Controller
    {
        private readonly ApplicationDbContext data;

        public CommitsController(ApplicationDbContext data)
        {
            this.data = data;
        }


        [Authorize]
        public HttpResponse Create(string id)
        {
            var repositoryCommit = this.data.Repositories
                .Where(c => c.Id == id)
                .Select(x => new CommitToRepositoryModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .FirstOrDefault();

            if (repositoryCommit == null)
            {
                return BadRequest();
            }

            return View(repositoryCommit);
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Create(CreateCommitFormModel model)
        {
            if (!this.data.Repositories.Any(r=>r.Id ==model.Id))
            {
                return NotFound();
            }

            var commit = new Commit
            {
                Description = model.Description,
                RepositoryId = model.Id,
                CreatorId = this.User.Id
            };

            this.data.Commits.Add(commit);
            this.data.SaveChanges();

            return Redirect("/Repositories/All");
        }


    }
}
