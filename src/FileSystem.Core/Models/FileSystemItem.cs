﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileSystemItem.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the FileSystemItem type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;

namespace CluedIn.Crawling.FileSystem.Core.Models
{
    public class FileSystemItem<T>
        where T : FileSystemInfo
    {
        public List<CrawlEntry> _startingPoints;
        public FileSystemItem(T item, FileSystemPrincipal owner)
        {
            Item  = item;
            Owner = owner;
        }

        public FileSystemItem(T item, FileSystemCrawlJobData fylesystemcrawlJobData)
            : this(
                item,
                fylesystemcrawlJobData.FetchOwners 
                    ? FileSystemPrincipal.GetOwner(item, fylesystemcrawlJobData.LookupCache) 
                    : null)
        {
        }

        object Id => Item.FullName;

        public T Item { get; }

        public FileSystemPrincipal Owner { get; }
    }
}
