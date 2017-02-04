namespace PatientService
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PatientModel : DbContext
    {
        // Your context has been configured to use a 'PatientModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'PatientService.PatientModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'PatientModel' 
        // connection string in the application configuration file.
        public PatientModel()
            : base("name=PatientModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Patient> Patients { get; set; }
    }

    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}