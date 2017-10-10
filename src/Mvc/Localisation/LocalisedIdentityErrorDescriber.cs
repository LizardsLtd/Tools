using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Picums.Mvc.Localisation
{
    internal class LocalisedIdentityErrorDescriber : IdentityErrorDescriber
    {
        private readonly IStringLocalizer localiser;

        public LocalisedIdentityErrorDescriber(IStringLocalizer localiser)
            => this.localiser = localiser;

        public override IdentityError PasswordTooShort(int length)
            => this.CreateTranslatedIdentityError(nameof(this.PasswordTooShort), length);

        public override IdentityError ConcurrencyFailure()
            => this.CreateTranslatedIdentityError(nameof(this.ConcurrencyFailure));

        public override IdentityError DefaultError()
            => this.CreateTranslatedIdentityError(nameof(this.DefaultError));

        public override IdentityError DuplicateEmail(string email)
            => this.CreateTranslatedIdentityError(nameof(this.DuplicateEmail), email);

        public override IdentityError DuplicateUserName(string userName)
            => this.CreateTranslatedIdentityError(nameof(this.DuplicateUserName), userName);

        public override IdentityError InvalidEmail(string email)
            => this.CreateTranslatedIdentityError(nameof(this.InvalidEmail), email);

        public override IdentityError InvalidToken()
            => this.CreateTranslatedIdentityError(nameof(this.InvalidToken));

        public override IdentityError InvalidUserName(string userName)
            => this.CreateTranslatedIdentityError(nameof(this.InvalidUserName), userName);

        public override IdentityError LoginAlreadyAssociated()
            => this.CreateTranslatedIdentityError(nameof(this.LoginAlreadyAssociated));

        public override IdentityError PasswordMismatch()
            => this.CreateTranslatedIdentityError(nameof(this.PasswordMismatch));

        public override IdentityError PasswordRequiresDigit()
            => this.CreateTranslatedIdentityError(nameof(this.PasswordRequiresDigit));

        public override IdentityError PasswordRequiresLower()
            => this.CreateTranslatedIdentityError(nameof(this.PasswordRequiresLower));

        public override IdentityError PasswordRequiresUpper()
            => this.CreateTranslatedIdentityError(nameof(this.PasswordRequiresUpper));

        public override IdentityError UserAlreadyHasPassword()
            => this.CreateTranslatedIdentityError(nameof(this.UserAlreadyHasPassword));

        public override IdentityError UserLockoutNotEnabled()
            => this.CreateTranslatedIdentityError(nameof(this.UserLockoutNotEnabled));

        private IdentityError CreateTranslatedIdentityError(
            string translationKey,
            params object[] parameters)
                => new IdentityError
                {
                    Code = translationKey,
                    Description = this
                        .localiser
                        .GetString($"Registration.Errors.{translationKey}", parameters),
                };
    }
}