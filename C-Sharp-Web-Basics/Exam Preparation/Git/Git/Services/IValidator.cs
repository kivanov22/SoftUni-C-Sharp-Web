using Git.ModelViews.Commit;
using Git.ModelViews.Repository;
using Git.ModelViews.User;

namespace Git.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel model);

        ICollection<string> ValidateRepository(CreateRepositoryFormModel model);

        ICollection<string> ValidateCommit(CreateCommitFormModel model);
    }
}
