using System;
namespace JQOfficeTools
{
    public interface IOfficePlate
    {
        JQCollection<DataSourceItem> DataSource { get;}
        bool Output(int Mode);
        bool MarkException { get;set;}
        JQCollection<TagItem> Tags { get;}
        void OnAfterOutput(EventArgs value);
    }

    /// <summary>
    /// The enum of reference mode of officeplate
    /// </summary>
    public enum PlateModeType
    {
        Xml,
        Com
    }
}
