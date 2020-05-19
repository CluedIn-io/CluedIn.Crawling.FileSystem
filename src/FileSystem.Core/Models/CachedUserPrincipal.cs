// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CachedUserPrincipal.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the CachedUserPrincipal type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.DirectoryServices.AccountManagement;

namespace CluedIn.Crawling.FileSystem.Models
{
    public class CachedUserPrincipal
    {
        /**********************************************************************************************************
         * FIELDS
         **********************************************************************************************************/
        
        private bool initialized;

        /**********************************************************************************************************
         * CONSTRUCTORS
         **********************************************************************************************************/

        private CachedUserPrincipal()
        {
        }

        /**********************************************************************************************************
         * PROPERTIES
         **********************************************************************************************************/

        public string Name { get; private set; }

        public string DisplayName { get; private set; }

        public string SamAccountName { get; private set; }

        public string UserPrincipalName { get; private set; }

        public string EmailAddress { get; private set; }

        public string EmployeeId { get; private set; }

        public string GivenName { get; private set; }

        public string MiddleName { get; private set; }

        public string Surname { get; private set; }

        public string VoiceTelephoneNumber { get; private set; }

        public Guid? Guid { get; private set; }

        public string Description { get; private set; }

        public string DistinguishedName { get; private set; }

        /**********************************************************************************************************
         * METHODS
         **********************************************************************************************************/
        
        public static CachedUserPrincipal Create(UserPrincipal principal)
        {
            var instance = new CachedUserPrincipal();

            instance.Name                 = instance.TryGetValue(principal, p => p.Name);
            instance.DisplayName          = instance.TryGetValue(principal, p => p.DisplayName);
            instance.SamAccountName       = instance.TryGetValue(principal, p => p.SamAccountName);
            instance.UserPrincipalName    = instance.TryGetValue(principal, p => p.UserPrincipalName);
            instance.EmailAddress         = instance.TryGetValue(principal, p => p.EmailAddress);
            instance.EmployeeId           = instance.TryGetValue(principal, p => p.EmployeeId);
            instance.GivenName            = instance.TryGetValue(principal, p => p.GivenName);
            instance.MiddleName           = instance.TryGetValue(principal, p => p.MiddleName);
            instance.Surname              = instance.TryGetValue(principal, p => p.Surname);
            instance.VoiceTelephoneNumber = instance.TryGetValue(principal, p => p.VoiceTelephoneNumber);
            instance.Guid                 = instance.TryGetValue(principal, p => p.Guid);
            instance.Description          = instance.TryGetValue(principal, p => p.Description);
            instance.DistinguishedName    = instance.TryGetValue(principal, p => p.DistinguishedName);

            return instance.initialized ? instance : null;
        }

        private T TryGetValue<T>(UserPrincipal p, Func<UserPrincipal, T> func)
        {
            try
            {
                var value = func(p);

                initialized = true;
                return value;
            }
            catch (Exception)
            {
            }

            return default(T);
        }
    }
}
