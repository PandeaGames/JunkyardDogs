using System;
using System.Collections.Generic;

namespace PandeaGames.Data.Static
{
    public struct StaticDataEntry<TData>
    {
        public readonly TData Data;
        public readonly string ID;

        public StaticDataEntry(TData Data, string ID)
        {
            this.Data = Data;
            this.ID = ID;
        }
    }

    public interface IStaticDataDirectorySource
    {
        string[] GetIDs();
        string[] GetIDs(Type filterType);
        string[] GetIDs<TFilterType>();
    }
    
    public interface IStaticDataDirectorySource<TData> : IEnumerable<StaticDataEntry<TData>>, IStaticDataDirectorySource
    {
        
    }
}