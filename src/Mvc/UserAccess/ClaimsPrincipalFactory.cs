//using System.Collections.Generic;
//using System.Diagnostics.Contracts;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Options;

//namespace Picums.Mvc.UserAccess
//{
//    internal sealed class ClaimsPrincipalFactory<TUser> : IUserClaimsPrincipalFactory<TUser>
//         where TUser : class, IClaimsProvider
//    {
//        public ClaimsPrincipalFactory(
//            UserManager<TUser> userManager
//            , IOptions<IdentityOptions> optionsAccessor)
//        {
//            Contract.Requires(
//                userManager == null
//                , "user manager object could not be null");

//            Contract.Requires(
//                optionsAccessor == null || optionsAccessor.Value == null
//                , "option accessors is not available");

//            this.UserManager = userManager;
//            this.Options = optionsAccessor.Value;
//        }

//        public IdentityOptions Options { get; }
//        public UserManager<TUser> UserManager { get; }

//        public async Task<ClaimsPrincipal> CreateAsync(TUser user)
//        {
//            //var claimsIdentity = new ClaimsIdentity(
//            //    this.Options.Cookies.ApplicationCookieAuthenticationScheme
//            //    , this.Options.ClaimsIdentity.UserNameClaimType
//            //    , this.Options.ClaimsIdentity.RoleClaimType);

//            //claimsIdentity.AddClaim(await GetUserIdClaim(user));
//            //claimsIdentity.AddClaim(await GetUserNameClaim(user));
//            //claimsIdentity.AddClaims(await GetUserClaims(user));

//            var claimsIdentity = new ClaimsIdentity(this.YieldClaims(user));

//            return new ClaimsPrincipal(claimsIdentity);
//        }

//        public Task<List<Claim>> GetUserClaims(IClaimsProvider user)
//            => Task.Run(() => user.Claims.ToList());

//        private IEnumerable<Claim> YieldClaims(TUser user)
//        {
//            yield return GetUserNameClaim(user).Result;
//            yield return GetUserIdClaim(user).Result;
//            foreach (var userClaim in GetUserClaims(user).Result)
//            {
//                yield return userClaim;
//            }
//        }

//        private async Task<Claim> GetUserIdClaim(TUser user)
//            => new Claim(
//                this.Options.ClaimsIdentity.UserIdClaimType
//                , await this.UserManager.GetUserIdAsync(user));

//        private async Task<Claim> GetUserNameClaim(TUser user)
//            => new Claim(
//                this.Options.ClaimsIdentity.UserNameClaimType
//                , await this.UserManager.GetUserNameAsync(user));
//    }
//}