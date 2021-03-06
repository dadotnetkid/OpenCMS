/*using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace OpenCMS.Infrastructure.Manager
{
    public class ResourcesManager
    {
        private static readonly ResourcesManager _resourceManager
           = new ResourceManager("Microsoft.Extensions.Identity.Core.Resources", typeof(ResourcesManager).GetTypeInfo().Assembly);

        /// <summary>
        /// Optimistic concurrency failure, object has been modified.
        /// </summary>
        internal static string ConcurrencyFailure
        {
            get => GetString("ConcurrencyFailure");
        }

        /// <summary>
        /// Optimistic concurrency failure, object has been modified.
        /// </summary>
        internal static string FormatConcurrencyFailure()
            => GetString("ConcurrencyFailure");

        /// <summary>
        /// An unknown failure has occurred.
        /// </summary>
        internal static string DefaultError
        {
            get => GetString("DefaultError");
        }

        /// <summary>
        /// An unknown failure has occurred.
        /// </summary>
        internal static string FormatDefaultError()
            => GetString("DefaultError");

        /// <summary>
        /// Email '{0}' is already taken.
        /// </summary>
        internal static string DuplicateEmail
        {
            get => GetString("DuplicateEmail");
        }

        /// <summary>
        /// Email '{0}' is already taken.
        /// </summary>
        internal static string FormatDuplicateEmail(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("DuplicateEmail"), p0);

        /// <summary>
        /// Role name '{0}' is already taken.
        /// </summary>
        internal static string DuplicateRoleName
        {
            get => GetString("DuplicateRoleName");
        }

        /// <summary>
        /// Role name '{0}' is already taken.
        /// </summary>
        internal static string FormatDuplicateRoleName(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("DuplicateRoleName"), p0);

        /// <summary>
        /// User name '{0}' is already taken.
        /// </summary>
        internal static string DuplicateUserName
        {
            get => GetString("DuplicateUserName");
        }

        /// <summary>
        /// User name '{0}' is already taken.
        /// </summary>
        internal static string FormatDuplicateUserName(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("DuplicateUserName"), p0);

        /// <summary>
        /// Email '{0}' is invalid.
        /// </summary>
        internal static string InvalidEmail
        {
            get => GetString("InvalidEmail");
        }

        /// <summary>
        /// Email '{0}' is invalid.
        /// </summary>
        internal static string FormatInvalidEmail(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("InvalidEmail"), p0);

        /// <summary>
        /// Type {0} must derive from {1}&lt;{2}&gt;.
        /// </summary>
        internal static string InvalidManagerType
        {
            get => GetString("InvalidManagerType");
        }

        /// <summary>
        /// Type {0} must derive from {1}&lt;{2}&gt;.
        /// </summary>
        internal static string FormatInvalidManagerType(object p0, object p1, object p2)
            => string.Format(CultureInfo.CurrentCulture, GetString("InvalidManagerType"), p0, p1, p2);

        /// <summary>
        /// The provided PasswordHasherCompatibilityMode is invalid.
        /// </summary>
        internal static string InvalidPasswordHasherCompatibilityMode
        {
            get => GetString("InvalidPasswordHasherCompatibilityMode");
        }

        /// <summary>
        /// The provided PasswordHasherCompatibilityMode is invalid.
        /// </summary>
        internal static string FormatInvalidPasswordHasherCompatibilityMode()
            => GetString("InvalidPasswordHasherCompatibilityMode");

        /// <summary>
        /// The iteration count must be a positive integer.
        /// </summary>
        internal static string InvalidPasswordHasherIterationCount
        {
            get => GetString("InvalidPasswordHasherIterationCount");
        }

        /// <summary>
        /// The iteration count must be a positive integer.
        /// </summary>
        internal static string FormatInvalidPasswordHasherIterationCount()
            => GetString("InvalidPasswordHasherIterationCount");

        /// <summary>
        /// Role name '{0}' is invalid.
        /// </summary>
        internal static string InvalidRoleName
        {
            get => GetString("InvalidRoleName");
        }

        /// <summary>
        /// Role name '{0}' is invalid.
        /// </summary>
        internal static string FormatInvalidRoleName(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("InvalidRoleName"), p0);

        /// <summary>
        /// Invalid token.
        /// </summary>
        internal static string InvalidToken
        {
            get => GetString("InvalidToken");
        }

        /// <summary>
        /// Invalid token.
        /// </summary>
        internal static string FormatInvalidToken()
            => GetString("InvalidToken");

        /// <summary>
        /// User name '{0}' is invalid, can only contain letters or digits.
        /// </summary>
        internal static string InvalidUserName
        {
            get => GetString("InvalidUserName");
        }

        /// <summary>
        /// User name '{0}' is invalid, can only contain letters or digits.
        /// </summary>
        internal static string FormatInvalidUserName(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("InvalidUserName"), p0);

        /// <summary>
        /// A user with this login already exists.
        /// </summary>
        internal static string LoginAlreadyAssociated
        {
            get => GetString("LoginAlreadyAssociated");
        }

        /// <summary>
        /// A user with this login already exists.
        /// </summary>
        internal static string FormatLoginAlreadyAssociated()
            => GetString("LoginAlreadyAssociated");

        /// <summary>
        /// AddIdentity must be called on the service collection.
        /// </summary>
        internal static string MustCallAddIdentity
        {
            get => GetString("MustCallAddIdentity");
        }

        /// <summary>
        /// AddIdentity must be called on the service collection.
        /// </summary>
        internal static string FormatMustCallAddIdentity()
            => GetString("MustCallAddIdentity");

        /// <summary>
        /// No IUserTwoFactorTokenProvider&lt;{0}&gt; named '{1}' is registered.
        /// </summary>
        internal static string NoTokenProvider
        {
            get => GetString("NoTokenProvider");
        }

        /// <summary>
        /// No IUserTwoFactorTokenProvider&lt;{0}&gt; named '{1}' is registered.
        /// </summary>
        internal static string FormatNoTokenProvider(object p0, object p1)
            => string.Format(CultureInfo.CurrentCulture, GetString("NoTokenProvider"), p0, p1);

        /// <summary>
        /// User security stamp cannot be null.
        /// </summary>
        internal static string NullSecurityStamp
        {
            get => GetString("NullSecurityStamp");
        }

        /// <summary>
        /// User security stamp cannot be null.
        /// </summary>
        internal static string FormatNullSecurityStamp()
            => GetString("NullSecurityStamp");

        /// <summary>
        /// Incorrect password.
        /// </summary>
        internal static string PasswordMismatch
        {
            get => GetString("PasswordMismatch");
        }

        /// <summary>
        /// Incorrect password.
        /// </summary>
        internal static string FormatPasswordMismatch()
            => GetString("PasswordMismatch");

        /// <summary>
        /// Passwords must have at least one digit ('0'-'9').
        /// </summary>
        internal static string PasswordRequiresDigit
        {
            get => GetString("PasswordRequiresDigit");
        }

        /// <summary>
        /// Passwords must have at least one digit ('0'-'9').
        /// </summary>
        internal static string FormatPasswordRequiresDigit()
            => GetString("PasswordRequiresDigit");

        /// <summary>
        /// Passwords must have at least one lowercase ('a'-'z').
        /// </summary>
        internal static string PasswordRequiresLower
        {
            get => GetString("PasswordRequiresLower");
        }

        /// <summary>
        /// Passwords must have at least one lowercase ('a'-'z').
        /// </summary>
        internal static string FormatPasswordRequiresLower()
            => GetString("PasswordRequiresLower");

        /// <summary>
        /// Passwords must have at least one non alphanumeric character.
        /// </summary>
        internal static string PasswordRequiresNonAlphanumeric
        {
            get => GetString("PasswordRequiresNonAlphanumeric");
        }

        /// <summary>
        /// Passwords must have at least one non alphanumeric character.
        /// </summary>
        internal static string FormatPasswordRequiresNonAlphanumeric()
            => GetString("PasswordRequiresNonAlphanumeric");

        /// <summary>
        /// Passwords must have at least one uppercase ('A'-'Z').
        /// </summary>
        internal static string PasswordRequiresUpper
        {
            get => GetString("PasswordRequiresUpper");
        }

        /// <summary>
        /// Passwords must have at least one uppercase ('A'-'Z').
        /// </summary>
        internal static string FormatPasswordRequiresUpper()
            => GetString("PasswordRequiresUpper");

        /// <summary>
        /// Passwords must be at least {0} characters.
        /// </summary>
        internal static string PasswordTooShort
        {
            get => GetString("PasswordTooShort");
        }

        /// <summary>
        /// Passwords must be at least {0} characters.
        /// </summary>
        internal static string FormatPasswordTooShort(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("PasswordTooShort"), p0);

        /// <summary>
        /// Role {0} does not exist.
        /// </summary>
        internal static string RoleNotFound
        {
            get => GetString("RoleNotFound");
        }

        /// <summary>
        /// Role {0} does not exist.
        /// </summary>
        internal static string FormatRoleNotFound(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("RoleNotFound"), p0);

        /// <summary>
        /// Store does not implement IQueryableRoleStore&lt;TRole&gt;.
        /// </summary>
        internal static string StoreNotIQueryableRoleStore
        {
            get => GetString("StoreNotIQueryableRoleStore");
        }

        /// <summary>
        /// Store does not implement IQueryableRoleStore&lt;TRole&gt;.
        /// </summary>
        internal static string FormatStoreNotIQueryableRoleStore()
            => GetString("StoreNotIQueryableRoleStore");

        /// <summary>
        /// Store does not implement IQueryableUserStore&lt;TUser&gt;.
        /// </summary>
        internal static string StoreNotIQueryableUserStore
        {
            get => GetString("StoreNotIQueryableUserStore");
        }

        /// <summary>
        /// Store does not implement IQueryableUserStore&lt;TUser&gt;.
        /// </summary>
        internal static string FormatStoreNotIQueryableUserStore()
            => GetString("StoreNotIQueryableUserStore");

        /// <summary>
        /// Store does not implement IRoleClaimStore&lt;TRole&gt;.
        /// </summary>
        internal static string StoreNotIRoleClaimStore
        {
            get => GetString("StoreNotIRoleClaimStore");
        }

        /// <summary>
        /// Store does not implement IRoleClaimStore&lt;TRole&gt;.
        /// </summary>
        internal static string FormatStoreNotIRoleClaimStore()
            => GetString("StoreNotIRoleClaimStore");

        /// <summary>
        /// Store does not implement IUserAuthenticationTokenStore&lt;User&gt;.
        /// </summary>
        internal static string StoreNotIUserAuthenticationTokenStore
        {
            get => GetString("StoreNotIUserAuthenticationTokenStore");
        }

        /// <summary>
        /// Store does not implement IUserAuthenticationTokenStore&lt;User&gt;.
        /// </summary>
        internal static string FormatStoreNotIUserAuthenticationTokenStore()
            => GetString("StoreNotIUserAuthenticationTokenStore");

        /// <summary>
        /// Store does not implement IUserClaimStore&lt;TUser&gt;.
        /// </summary>
        internal static string StoreNotIUserClaimStore
        {
            get => GetString("StoreNotIUserClaimStore");
        }

        /// <summary>
        /// Store does not implement IUserClaimStore&lt;TUser&gt;.
        /// </summary>
        internal static string FormatStoreNotIUserClaimStore()
            => GetString("StoreNotIUserClaimStore");

        /// <summary>
        /// Store does not implement IUserConfirmationStore&lt;TUser&gt;.
        /// </summary>
        internal static string StoreNotIUserConfirmationStore
        {
            get => GetString("StoreNotIUserConfirmationStore");
        }

        /// <summary>
        /// Store does not implement IUserConfirmationStore&lt;TUser&gt;.
        /// </summary>
        internal static string FormatStoreNotIUserConfirmationStore()
            => GetString("StoreNotIUserConfirmationStore");

        /// <summary>
        /// Store does not implement IUserEmailStore&lt;TUser&gt;.
        /// </summary>
        internal static string StoreNotIUserEmailStore
        {
            get => GetString("StoreNotIUserEmailStore");
        }

        /// <summary>
        /// Store does not implement IUserEmailStore&lt;TUser&gt;.
        /// </summary>
        internal static string FormatStoreNotIUserEmailStore()
            => GetString("StoreNotIUserEmailStore");

        /// <summary>
        /// Store does not implement IUserLockoutStore&lt;TUser&gt;.
        /// </summary>
        internal static string StoreNotIUserLockoutStore
        {
            get => GetString("StoreNotIUserLockoutStore");
        }

        /// <summary>
        /// Store does not implement IUserLockoutStore&lt;TUser&gt;.
        /// </summary>
        internal static string FormatStoreNotIUserLockoutStore()
            => GetString("StoreNotIUserLockoutStore");

        /// <summary>
        /// Store does not implement IUserLoginStore&lt;TUser&gt;.
        /// </summary>
        internal static string StoreNotIUserLoginStore
        {
            get => GetString("StoreNotIUserLoginStore");
        }

        /// <summary>
        /// Store does not implement IUserLoginStore&lt;TUser&gt;.
        /// </summary>
        internal static string FormatStoreNotIUserLoginStore()
            => GetString("StoreNotIUserLoginStore");

        /// <summary>
        /// Store does not implement IUserPasswordStore&lt;TUser&gt;.
        /// </summary>
        internal static string StoreNotIUserPasswordStore
        {
            get => GetString("StoreNotIUserPasswordStore");
        }

        /// <summary>
        /// Store does not implement IUserPasswordStore&lt;TUser&gt;.
        /// </summary>
        internal static string FormatStoreNotIUserPasswordStore()
            => GetString("StoreNotIUserPasswordStore");

        /// <summary>
        /// Store does not implement IUserPhoneNumberStore&lt;TUser&gt;.
        /// </summary>
        internal static string StoreNotIUserPhoneNumberStore
        {
            get => GetString("StoreNotIUserPhoneNumberStore");
        }

        /// <summary>
        /// Store does not implement IUserPhoneNumberStore&lt;TUser&gt;.
        /// </summary>
        internal static string FormatStoreNotIUserPhoneNumberStore()
            => GetString("StoreNotIUserPhoneNumberStore");

        /// <summary>
        /// Store does not implement IUserRoleStore&lt;TUser&gt;.
        /// </summary>
        internal static string StoreNotIUserRoleStore
        {
            get => GetString("StoreNotIUserRoleStore");
        }

        /// <summary>
        /// Store does not implement IUserRoleStore&lt;TUser&gt;.
        /// </summary>
        internal static string FormatStoreNotIUserRoleStore()
            => GetString("StoreNotIUserRoleStore");

        /// <summary>
        /// Store does not implement IUserSecurityStampStore&lt;TUser&gt;.
        /// </summary>
        internal static string StoreNotIUserSecurityStampStore
        {
            get => GetString("StoreNotIUserSecurityStampStore");
        }

        /// <summary>
        /// Store does not implement IUserSecurityStampStore&lt;TUser&gt;.
        /// </summary>
        internal static string FormatStoreNotIUserSecurityStampStore()
            => GetString("StoreNotIUserSecurityStampStore");

        /// <summary>
        /// Store does not implement IUserAuthenticatorKeyStore&lt;User&gt;.
        /// </summary>
        internal static string StoreNotIUserAuthenticatorKeyStore
        {
            get => GetString("StoreNotIUserAuthenticatorKeyStore");
        }

        /// <summary>
        /// Store does not implement IUserAuthenticatorKeyStore&lt;User&gt;.
        /// </summary>
        internal static string FormatStoreNotIUserAuthenticatorKeyStore()
            => GetString("StoreNotIUserAuthenticatorKeyStore");

        /// <summary>
        /// Store does not implement IUserTwoFactorStore&lt;TUser&gt;.
        /// </summary>
        internal static string StoreNotIUserTwoFactorStore
        {
            get => GetString("StoreNotIUserTwoFactorStore");
        }

        /// <summary>
        /// Store does not implement IUserTwoFactorStore&lt;TUser&gt;.
        /// </summary>
        internal static string FormatStoreNotIUserTwoFactorStore()
            => GetString("StoreNotIUserTwoFactorStore");

        /// <summary>
        /// Recovery code redemption failed.
        /// </summary>
        internal static string RecoveryCodeRedemptionFailed
        {
            get => GetString("RecoveryCodeRedemptionFailed");
        }

        /// <summary>
        /// Recovery code redemption failed.
        /// </summary>
        internal static string FormatRecoveryCodeRedemptionFailed()
            => GetString("RecoveryCodeRedemptionFailed");

        /// <summary>
        /// User already has a password set.
        /// </summary>
        internal static string UserAlreadyHasPassword
        {
            get => GetString("UserAlreadyHasPassword");
        }

        /// <summary>
        /// User already has a password set.
        /// </summary>
        internal static string FormatUserAlreadyHasPassword()
            => GetString("UserAlreadyHasPassword");

        /// <summary>
        /// User already in role '{0}'.
        /// </summary>
        internal static string UserAlreadyInRole
        {
            get => GetString("UserAlreadyInRole");
        }

        /// <summary>
        /// User already in role '{0}'.
        /// </summary>
        internal static string FormatUserAlreadyInRole(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("UserAlreadyInRole"), p0);

        /// <summary>
        /// User is locked out.
        /// </summary>
        internal static string UserLockedOut
        {
            get => GetString("UserLockedOut");
        }

        /// <summary>
        /// User is locked out.
        /// </summary>
        internal static string FormatUserLockedOut()
            => GetString("UserLockedOut");

        /// <summary>
        /// Lockout is not enabled for this user.
        /// </summary>
        internal static string UserLockoutNotEnabled
        {
            get => GetString("UserLockoutNotEnabled");
        }

        /// <summary>
        /// Lockout is not enabled for this user.
        /// </summary>
        internal static string FormatUserLockoutNotEnabled()
            => GetString("UserLockoutNotEnabled");

        /// <summary>
        /// User {0} does not exist.
        /// </summary>
        internal static string UserNameNotFound
        {
            get => GetString("UserNameNotFound");
        }

        /// <summary>
        /// User {0} does not exist.
        /// </summary>
        internal static string FormatUserNameNotFound(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("UserNameNotFound"), p0);

        /// <summary>
        /// User is not in role '{0}'.
        /// </summary>
        internal static string UserNotInRole
        {
            get => GetString("UserNotInRole");
        }

        /// <summary>
        /// User is not in role '{0}'.
        /// </summary>
        internal static string FormatUserNotInRole(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("UserNotInRole"), p0);

        /// <summary>
        /// Store does not implement IUserTwoFactorRecoveryCodeStore&lt;User&gt;.
        /// </summary>
        internal static string StoreNotIUserTwoFactorRecoveryCodeStore
        {
            get => GetString("StoreNotIUserTwoFactorRecoveryCodeStore");
        }

        /// <summary>
        /// Store does not implement IUserTwoFactorRecoveryCodeStore&lt;User&gt;.
        /// </summary>
        internal static string FormatStoreNotIUserTwoFactorRecoveryCodeStore()
            => GetString("StoreNotIUserTwoFactorRecoveryCodeStore");

        /// <summary>
        /// Passwords must use at least {0} different characters.
        /// </summary>
        internal static string PasswordRequiresUniqueChars
        {
            get => GetString("PasswordRequiresUniqueChars");
        }

        /// <summary>
        /// Passwords must use at least {0} different characters.
        /// </summary>
        internal static string FormatPasswordRequiresUniqueChars(object p0)
            => string.Format(CultureInfo.CurrentCulture, GetString("PasswordRequiresUniqueChars"), p0);

        /// <summary>
        /// No RoleType was specified, try AddRoles&lt;TRole&gt;().
        /// </summary>
        internal static string NoRoleType
        {
            get => GetString("NoRoleType");
        }

        /// <summary>
        /// No RoleType was specified, try AddRoles&lt;TRole&gt;().
        /// </summary>
        internal static string FormatNoRoleType()
            => GetString("NoRoleType");

        /// <summary>
        /// Store does not implement IProtectedUserStore&lt;TUser&gt; which is required when ProtectPersonalData = true.
        /// </summary>
        internal static string StoreNotIProtectedUserStore
        {
            get => GetString("StoreNotIProtectedUserStore");
        }

        /// <summary>
        /// Store does not implement IProtectedUserStore&lt;TUser&gt; which is required when ProtectPersonalData = true.
        /// </summary>
        internal static string FormatStoreNotIProtectedUserStore()
            => GetString("StoreNotIProtectedUserStore");

        /// <summary>
        /// No IPersonalDataProtector service was registered, this is required when ProtectPersonalData = true.
        /// </summary>
        internal static string NoPersonalDataProtector
        {
            get => GetString("NoPersonalDataProtector");
        }

        /// <summary>
        /// No IPersonalDataProtector service was registered, this is required when ProtectPersonalData = true.
        /// </summary>
        internal static string FormatNoPersonalDataProtector()
            => GetString("NoPersonalDataProtector");

        private static string GetString(string name, params string[] formatterNames)
        {
            var value = _resourceManager.GetString(name);

            System.Diagnostics.Debug.Assert(value != null);

            if (formatterNames != null)
            {
                for (var i = 0; i < formatterNames.Length; i++)
                {
                    value = value.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
                }
            }

            return value;
        }
    }
}
}
*/