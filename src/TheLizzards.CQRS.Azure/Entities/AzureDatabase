using System;
using System.Linq;

namespace TheLizzards.CQRS.Azure.Entities
{
  public sealed class AzureDatabase
  {
    public AzureDatabase(string name, IEnumerable<string> collections)
    {
      this.Name = name;
      this.Collections = collections.ToArray();
    }
    
    public string Name { get; }
    
    public string[] Collections{ get; }
  }
}
