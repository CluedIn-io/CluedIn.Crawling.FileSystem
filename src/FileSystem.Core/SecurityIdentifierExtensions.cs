// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityIdentifierExtensions.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the SecurityIdentifierExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using CluedIn.Crawling.FileSystem.Core.Models;

namespace CluedIn.Crawling.FileSystem.Core
{
    /// <summary>
    /// The security identifier extensions.
    /// </summary>
    public static class SecurityIdentifierExtensions
    {
        public static CachedUserPrincipal GetUserPrincipal(this SecurityIdentifier sid)
        {
            using (var ctx = new PrincipalContext(ContextType.Domain))
            {
                var p = UserPrincipal.FindByIdentity(ctx, IdentityType.Sid, sid.ToString());

                if (p != null)
                    return CachedUserPrincipal.Create(p);

                return null;
            }
        }
    }
}
