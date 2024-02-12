using TaskList.Authentication.ResponseModels.Token;

namespace TaskList.Authentication.ResponseModels.User
{
    internal sealed record UserLoginResponseModel(bool Succeeded, string Message, TokenResponseModel? Token);
}