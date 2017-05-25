using System.Threading.Tasks;
using Moq;
using Picums.Mvc.Authorization;
using Xunit;

namespace Picums.Mvc.Tests.Authorization
{
    public sealed class AuthorizeByPermissionFilterTests
    {
        [Fact]
        public async Task ForUnauthorizeUserRedirectHappen()
        {
            var contoller = "contoller";
            var action = "action";
            var filter = new AuthorizeByPermissionFilter(contoller, action);
            var context = Mock.Of<AuthorizationFilterContext>();

            await filter.OnAuthorizationAsync(context);
        }
    }
}