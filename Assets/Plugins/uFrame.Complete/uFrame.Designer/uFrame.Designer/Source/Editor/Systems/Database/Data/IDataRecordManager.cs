using System;
using System.Collections.Generic;
using System.Reflection;

namespace uFrame.Editor.Database.Data
{
    public interface IDataRecordManager
    {
        void Initialize(IRepository repository);
        Type For { get; }
        PropertyInfo[] ForeignKeys { get; set; }
        IDataRecord GetSingle(string identifier);
        IEnumerable<IDataRecord> GetAll();
        void Add(IDataRecord o);
        void Commit();
        void Remove(IDataRecord item);
        void Import(ExportedRecord record);
    }
}