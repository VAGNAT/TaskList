namespace TaskList.Authentication.ResponseModels.Token
{
    internal sealed record TokenResponseModel(string Token, DateTime Expiration);
}
