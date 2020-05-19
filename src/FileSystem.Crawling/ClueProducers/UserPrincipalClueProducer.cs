using System;
using System.Text.RegularExpressions;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.FileSystem.Core;
using CluedIn.Crawling.Helpers;

using CluedIn.Crawling.FileSystem.Vocabularies;
using CluedIn.Crawling.FileSystem.Core.Models;
using CluedIn.Crawling.FileSystem.Models;
using RuleConstants = CluedIn.Core.Constants.Validation.Rules;

namespace CluedIn.Crawling.FileSystem.ClueProducers
{
    public class UserPrincipalClueProducer : BaseClueProducer<FileSystemPrincipal>
    {
        private readonly IClueFactory _factory;

        public UserPrincipalClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(FileSystemPrincipal input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Infrastructure.User, input.Sid.ToString(), accountId);

            var data = clue.Data.EntityData;

            data.Name = input.Principal.PrintIfAvailable(v => v.Name);
            data.Name = data.Name ?? input.Account.PrintIfAvailable(v => v.Value);
            data.DisplayName = input.Principal.PrintIfAvailable(v => v.DisplayName);

            if (input.Principal != null)
            {
                if (input.Principal.SamAccountName != null)
                    data.Aliases.Add(input.Principal.SamAccountName);

                if (input.Principal.UserPrincipalName != null)
                    data.Aliases.Add(input.Principal.UserPrincipalName);
            }

            if (input.Account != null)
                data.Aliases.Add(input.Account.ToString());

            if (input.Principal != null && input.Principal.EmailAddress != null && MailAddressUtility.IsValid(input.Principal.EmailAddress))
                data.Codes.Add(new EntityCode(EntityType.Infrastructure.User, CodeOrigin.CluedIn.CreateSpecific("email"), input.Principal.EmailAddress.ToLowerInvariant()));

            if (input.Principal != null && input.Principal.UserPrincipalName != null && MailAddressUtility.IsValid(input.Principal.UserPrincipalName))
                data.Codes.Add(new EntityCode(EntityType.Infrastructure.User, CodeOrigin.CluedIn.CreateSpecific("email"), input.Principal.UserPrincipalName.ToLowerInvariant()));

            data.Properties[FileSystemVocabulary.UserPrincipal.EmailAddress] = input.Principal.PrintIfAvailable(v => v.EmailAddress);
            data.Properties[FileSystemVocabulary.UserPrincipal.EmployeeId] = input.Principal.PrintIfAvailable(v => v.EmployeeId);
            data.Properties[FileSystemVocabulary.UserPrincipal.GivenName] = input.Principal.PrintIfAvailable(v => v.GivenName);
            data.Properties[FileSystemVocabulary.UserPrincipal.MiddleName] = input.Principal.PrintIfAvailable(v => v.MiddleName);
            data.Properties[FileSystemVocabulary.UserPrincipal.Surname] = input.Principal.PrintIfAvailable(v => v.Surname);
            data.Properties[FileSystemVocabulary.UserPrincipal.Name] = input.Principal.PrintIfAvailable(v => v.Name);
            data.Properties[FileSystemVocabulary.UserPrincipal.VoiceTelephoneNumber] = input.Principal.PrintIfAvailable(v => v.VoiceTelephoneNumber);
            data.Properties[FileSystemVocabulary.UserPrincipal.Guid] = input.Principal.PrintIfAvailable(v => v.Guid);
            data.Properties[FileSystemVocabulary.UserPrincipal.Description] = input.Principal.PrintIfAvailable(v => v.Description);
            data.Properties[FileSystemVocabulary.UserPrincipal.DisplayName] = input.Principal.PrintIfAvailable(v => v.DisplayName);
            data.Properties[FileSystemVocabulary.UserPrincipal.SamAccountName] = input.Principal.PrintIfAvailable(v => v.SamAccountName);
            data.Properties[FileSystemVocabulary.UserPrincipal.UserPrincipalName] = input.Principal.PrintIfAvailable(v => v.UserPrincipalName);

            data.Properties[FileSystemVocabulary.UserPrincipal.Account] = input.Account.PrintIfAvailable(v => v.Value);

            data.Properties[FileSystemVocabulary.UserPrincipal.Sid] = input.Sid.PrintIfAvailable(v => v.Value);

            {
                var distinguishedName = input.Principal.PrintIfAvailable(v => v.DistinguishedName);

                if (distinguishedName != null)
                {
                    Regex regex = new Regex(@"^CN=.+DC=.+$", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

                    if (regex.IsMatch(distinguishedName))
                        data.Properties[FileSystemVocabulary.UserPrincipal.LdapDistinguishedName] = distinguishedName;
                    else
                        data.Properties[FileSystemVocabulary.UserPrincipal.DistinguishedName] = distinguishedName;
                }
            }

            _factory.CreateEntityRootReference(clue, EntityEdgeType.ManagedIn);

            clue.ValidationRuleSuppressions.AddRange(new[]
            {
                RuleConstants.METADATA_002_Uri_MustBeSet
            });

            return clue;
        }
    }
}
