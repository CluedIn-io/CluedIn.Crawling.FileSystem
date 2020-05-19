using System.Collections.Generic;
using CluedIn.Crawling.FileSystem.Core;

namespace Tests.Integration.FileSystem
{
  public static class FileSystemConfiguration
  {
    public static Dictionary<string, object> Create()
    {
      return new Dictionary<string, object>
            {
                { FileSystemConstants.KeyName.ApiKey, "demo" }
            };
    }
  }
}
