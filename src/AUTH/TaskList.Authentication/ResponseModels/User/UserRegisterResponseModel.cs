namespace TaskList.Authentication.ResponseModels.User
{
    internal sealed record UserRegisterResponseModel(bool Succeeded, IEnumerable<string> Errors);
}
