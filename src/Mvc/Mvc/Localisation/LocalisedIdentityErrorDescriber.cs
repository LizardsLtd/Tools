using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Picums.Mvc.Localisation
{
    internal class LocalisedIdentityErrorDescriber : IdentityErrorDescriber
    {
        private readonly IStringLocalizer localiser;

        public LocalisedIdentityErrorDescriber(IStringLocalizer localiser)
        {
            this.localiser = localiser;
        }

        public override IdentityError PasswordTooShort(int length)
            => CreateTranslatedIdentityError(nameof(PasswordTooShort), length);

        public override IdentityError ConcurrencyFailure()
            => CreateTranslatedIdentityError(nameof(ConcurrencyFailure));

        public override IdentityError DefaultError()
            => CreateTranslatedIdentityError(nameof(DefaultError));

        public override IdentityError DuplicateEmail(string email)
            => CreateTranslatedIdentityError(nameof(DuplicateEmail), email);

        public override IdentityError DuplicateUserName(string userName)
            => CreateTranslatedIdentityError(nameof(DuplicateUserName), userName);

        public override IdentityError InvalidEmail(string email)
            => CreateTranslatedIdentityError(nameof(InvalidEmail), email);

        public override IdentityError InvalidToken()
            => CreateTranslatedIdentityError(nameof(InvalidToken));

        public override IdentityError InvalidUserName(string userName)
            => CreateTranslatedIdentityError(nameof(InvalidUserName), userName);

        public override IdentityError LoginAlreadyAssociated()
            => CreateTranslatedIdentityError(nameof(LoginAlreadyAssociated));

        public override IdentityError PasswordMismatch()
            => CreateTranslatedIdentityError(nameof(PasswordMismatch));

        public override IdentityError PasswordRequiresDigit()
            => CreateTranslatedIdentityError(nameof(PasswordRequiresDigit));

        public override IdentityError PasswordRequiresLower()
            => CreateTranslatedIdentityError(nameof(PasswordRequiresLower));

        public override IdentityError PasswordRequiresUpper()
            => CreateTranslatedIdentityError(nameof(PasswordRequiresUpper));

        public override IdentityError UserAlreadyHasPassword()
            => CreateTranslatedIdentityError(nameof(UserAlreadyHasPassword));

        public override IdentityError UserLockoutNotEnabled()
            => CreateTranslatedIdentityError(nameof(UserLockoutNotEnabled));

        private IdentityError CreateTranslatedIdentityError(
            string translationKey
            , params object[] parameters)
            => new IdentityError
            {
                Code = translationKey,
                Description = this
                    .localiser
                    .GetString($"Registration.Errors.{translationKey}", parameters)
            };
    }
}