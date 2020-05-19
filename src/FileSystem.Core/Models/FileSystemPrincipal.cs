// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileSystemPrincipal.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the FileSystemPrincipal type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using CluedIn.Core;
using CluedIn.Crawling.FileSystem.Core.Helpers;

namespace CluedIn.Crawling.FileSystem.Core.Models
{
    public class FileSystemPrincipal : IIdentifiable
    {
        public static void Test()
        {
            var principal = GetOwner(new DirectoryInfo(@"\\san01\files\CluedIn"), LookupCache.Create());
        }

        public FileSystemPrincipal(SecurityIdentifier sid, IdentityReference account, CachedUserPrincipal principal)
        {
            Sid       = sid;
            Account   = account;
            Principal = principal;
        }

        object IIdentifiable.Id => Sid.ToString();

        public SecurityIdentifier Sid { get; }
        public IdentityReference Account { get; }
        public CachedUserPrincipal Principal { get; }

        public static FileSystemPrincipal GetOwner(FileSystemInfo item, LookupCache cache)
        {
            SecurityIdentifier  ownerSID        = null;
            IdentityReference   ownerNTAccount  = null;
            CachedUserPrincipal ownerPrincipal  = null;

            try
            {
                FileSystemSecurity fac = null;

                if (item is FileInfo)
                    fac = File.GetAccessControl(item.FullName);
                if (item is DirectoryInfo)
                    fac = Directory.GetAccessControl(item.FullName);

                if (fac != null)
                {
                    ownerSID = fac.GetOwner(typeof(SecurityIdentifier)) as SecurityIdentifier;

                    if (ownerSID != null)
                    {
                        ownerNTAccount = cache.GetName<NTAccount>(FileSystemObjectTypes.NTAccount, ownerSID);
                        ownerPrincipal = cache.GetName<CachedUserPrincipal>(FileSystemObjectTypes.Principal, ownerSID);
                    }
                }

                return new FileSystemPrincipal(ownerSID, ownerNTAccount, ownerPrincipal);
            }
            catch (Exception)
            {
            }

            return null;
        }
    }
}
