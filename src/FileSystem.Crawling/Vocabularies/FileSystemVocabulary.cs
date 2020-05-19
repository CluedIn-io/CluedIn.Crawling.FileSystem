// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileSystemVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the FileSystemVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CluedIn.Crawling.FileSystem.Vocabularies
{
    /// <summary>The file system vocabulary.</summary>
    public static class FileSystemVocabulary
    {
        /// <summary>
        /// Initializes static members of the <see cref="FileSystemVocabulary" /> class.
        /// </summary>
        static FileSystemVocabulary()
        {
            DirectoryInfo   = new DirectoryInfoVocabulary();
            FileInfo        = new FileInfoVocabulary();
            ComputerInfo    = new ComputerInfoVocabulary();
            UserPrincipal   = new UserPrincipalVocabulary();
        }

        /// <summary>Gets the card.</summary>
        /// <value>The card.</value>
        public static DirectoryInfoVocabulary DirectoryInfo { get; private set; }

        /// <summary>Gets the check list item.</summary>
        /// <value>The check list item.</value>
        public static FileInfoVocabulary FileInfo { get; private set; }

        /// <summary>Gets the computer information.</summary>
        /// <value>The computer information.</value>
        public static ComputerInfoVocabulary ComputerInfo { get; private set; }

        /// <summary>Gets the user principal.</summary>
        /// <value>The user principal.</value>
        public static UserPrincipalVocabulary UserPrincipal { get; private set; }
    }
}