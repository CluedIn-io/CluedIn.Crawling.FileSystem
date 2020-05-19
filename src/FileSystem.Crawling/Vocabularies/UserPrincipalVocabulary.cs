// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserPrincipalVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the UserPrincipalVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.FileSystem.Vocabularies
{
    /// <summary>
    /// The user principal vocabulary.
    /// </summary>
    public class UserPrincipalVocabulary : SimpleVocabulary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserPrincipalVocabulary"/> class.
        /// </summary>
        public UserPrincipalVocabulary()
        {
            this.VocabularyName = "File System User";
            this.KeyPrefix      = "filesystem.user";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Infrastructure.User;

            this.AddGroup("File System User Details", group =>
            {
                this.Guid                   = group.Add(new VocabularyKey("guid",                       VocabularyKeyDataType.Guid,         VocabularyKeyVisibility.Hidden));
                this.Sid                    = group.Add(new VocabularyKey("sid",                                                            VocabularyKeyVisibility.Hidden));

                this.EmailAddress           = group.Add(new VocabularyKey("emailAddress",               VocabularyKeyDataType.Email));
                this.GivenName              = group.Add(new VocabularyKey("givenName"));
                this.MiddleName             = group.Add(new VocabularyKey("middleName"));
                this.Surname                = group.Add(new VocabularyKey("surname"));
                this.Name                   = group.Add(new VocabularyKey("name"));
                this.VoiceTelephoneNumber   = group.Add(new VocabularyKey("voiceTelephoneNumber",       VocabularyKeyDataType.PhoneNumber));

                this.DisplayName            = group.Add(new VocabularyKey("displayName"));
                this.Description            = group.Add(new VocabularyKey("description"));
                this.EmployeeId             = group.Add(new VocabularyKey("employeeId"));
                this.DistinguishedName      = group.Add(new VocabularyKey("distinguishedName",                                              VocabularyKeyVisibility.Hidden));
                this.LdapDistinguishedName  = group.Add(new VocabularyKey("ldapDistinguishedName",                                          VocabularyKeyVisibility.Hidden));
                this.Account                = group.Add(new VocabularyKey("account"));
                this.SamAccountName         = group.Add(new VocabularyKey("samAccountName"));
                this.UserPrincipalName      = group.Add(new VocabularyKey("userPrincipalName"));
            });

            this.AddMapping(this.EmailAddress,          CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.Email);
            this.AddMapping(this.GivenName,             CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.FirstName);
            this.AddMapping(this.MiddleName,            CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.MiddleName);
            this.AddMapping(this.Surname,               CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.LastName);
            this.AddMapping(this.Name,                  CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.FullName);
            this.AddMapping(this.VoiceTelephoneNumber,  CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.PhoneNumber);
            this.AddMapping(this.Sid,                   CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.UserSID);
            this.AddMapping(this.EmployeeId,            CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.EmployeeId);
            this.AddMapping(this.LdapDistinguishedName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.LdapDistinguishedName);
        }

        public VocabularyKey EmailAddress { get; protected set; }
        public VocabularyKey EmployeeId { get; protected set; }
        public VocabularyKey GivenName { get; protected set; }
        public VocabularyKey MiddleName { get; protected set; }
        public VocabularyKey Surname { get; protected set; }
        public VocabularyKey Name { get; protected set; }
        public VocabularyKey VoiceTelephoneNumber { get; protected set; }
        public VocabularyKey Guid { get; protected set; }
        public VocabularyKey Description { get; protected set; }
        public VocabularyKey DisplayName { get; protected set; }
        public VocabularyKey DistinguishedName { get; protected set; }
        public VocabularyKey LdapDistinguishedName { get; protected set; }
        public VocabularyKey SamAccountName { get; protected set; }
        public VocabularyKey UserPrincipalName { get; protected set; }
        public VocabularyKey Account { get; protected set; }
        public VocabularyKey Sid { get; protected set; }
    }
}