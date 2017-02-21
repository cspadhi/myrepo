using System;

namespace EFEndToEnd.DataAccess.Repository.Core
{
    public interface IWriteRepository : IDisposable
    {

        TItem Update<TItem>(TItem item, bool saveImmediately = true) where TItem : class, new();


        TItem Delete<TItem>(TItem item, bool saveImmediately = true) where TItem : class, new();


        TItem Insert<TItem>(TItem item, bool saveImmediately = true) where TItem : class, new();


        void Save();

    }
}
