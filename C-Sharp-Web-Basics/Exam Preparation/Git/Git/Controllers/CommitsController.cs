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
        public HttpResponse Create(string Id)
        {
            var repositoryCommit = this.data.Repositories
                .Where(c => c.Id == Id)
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

        [Authorize]
        public HttpResponse All()
        {
            var commits = this.data.Commits
                .Where(c => c.CreatorId == this.User.Id)
                .Select(x => new CommitsListingViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn.ToLocalTime().ToString("F"),
                    Repository = x.Repository.Name
                })
                .ToList();


            return View(commits);
        }

        [Authorize]
        public HttpResponse Delete(string Id)
        {
            var commit = this.data.Commits.Where(x=>x.Id == Id).FirstOrDefault();

            if (commit ==null || commit.CreatorId != this.User.Id)
            {
                return BadRequest();
            }

            this.data.Commits.Remove(commit);
            this.data.SaveChanges();

            return Redirect("/Commits/All");
        }
    }
}
