//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30128.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::System.Data.Objects.DataClasses.EdmSchemaAttribute()]

// Original file name:
// Generation date: 2010/5/11 17:31:01
namespace MWizard
{
    
    /// <summary>
    /// There are no comments for MWizardContainer in the schema.
    /// </summary>
    public partial class MWizardContainer : global::System.Data.Objects.ObjectContext
    {
        /// <summary>
        /// Initializes a new MWizardContainer object using the connection string found in the 'MWizardContainer' section of the application configuration file.
        /// </summary>
        public MWizardContainer() : 
                base("name=MWizardContainer", "MWizardContainer")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new MWizardContainer object.
        /// </summary>
        public MWizardContainer(string connectionString) : 
                base(connectionString, "MWizardContainer")
        {
            this.OnContextCreated();
        }
        /// <summary>
        /// Initialize a new MWizardContainer object.
        /// </summary>
        public MWizardContainer(global::System.Data.EntityClient.EntityConnection connection) : 
                base(connection, "MWizardContainer")
        {
            this.OnContextCreated();
        }
        partial void OnContextCreated();
    }
}
