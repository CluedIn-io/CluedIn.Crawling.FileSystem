using CluedIn.Core;
using CluedIn.Core.Messages.Processing;
using System;
using System.Collections.Generic;
using CluedIn.Core.Data;
using CluedIn.Core.Mesh;
using CluedIn.Core.Messages.WebApp;
using CluedIn.Crawling.FileSystem.Core;

namespace CluedIn.Providers.Mesh
{
  public class FileSystem_Command_MeshProcessor : BaseMeshProcessor
  {
    public FileSystem_Command_MeshProcessor(ApplicationContext appContext)
        : base(appContext)
    {
    }

    public override Guid GetProviderId() =>
      FileSystemConstants.ProviderId;

    /* TODO de-comment and implement the override for Accept method in your deriverd Provider class if requiring any more behavious than base class implementation
    public override bool Accept(MeshDataCommand command, MeshQuery query, IEntity entity)
    {
      // TODO filter on action 
      // e.g. query.Action == ActionType.*;
      return base.Accept(command, query, entity);
    }
    */

    public override void DoProcess(ExecutionContext context, MeshDataCommand command, IDictionary<string, object> jobData, MeshQuery query)
    {
      throw new NotImplementedException();
    }

    public override string GetLookupId(IEntity entity)
    {
      throw new NotImplementedException();
    }

    public override List<RawQuery> GetRawQueries(IDictionary<string, object> config, IEntity entity, Core.Mesh.Properties properties)
    {
      throw new NotImplementedException();
    }

    public override string GetVocabularyProviderKey()
    {
      throw new NotImplementedException();
    }

    public override List<QueryResponse> RunQueries(IDictionary<string, object> config, string id, Core.Mesh.Properties properties)
    {
      throw new NotImplementedException();
    }

    public override List<QueryResponse> Validate(ExecutionContext context, MeshDataCommand command, IDictionary<string, object> config, string id, MeshQuery query)
    {
      throw new NotImplementedException();
    }
  }

}
