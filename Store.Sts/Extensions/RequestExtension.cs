using OpenIddict.Abstractions;

namespace Store.Sts.Extensions
{
    public static class RequestExtension
    {
        public static bool IsVerificationTokenGrantType(this OpenIddictRequest request)
        {
            return request.GrantType == "verification_token";
        }
    }
}