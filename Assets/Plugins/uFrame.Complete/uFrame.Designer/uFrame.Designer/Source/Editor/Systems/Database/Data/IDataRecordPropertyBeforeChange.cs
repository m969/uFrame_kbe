namespace uFrame.Editor.Database.Data
{
    public interface IDataRecordPropertyBeforeChange
    {
        void BeforePropertyChanged(IDataRecord record, string name, object previousValue, object nextValue);
    }
}