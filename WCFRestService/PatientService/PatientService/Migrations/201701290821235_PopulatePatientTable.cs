namespace PatientService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatePatientTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO patients (name) values ('Patient One')");
            Sql("INSERT INTO patients (name) values ('Patient Two')");
            Sql("INSERT INTO patients (name) values ('Patient Three')");
            Sql("INSERT INTO patients (name) values ('Patient Four')");
        }
        
        public override void Down()
        {
        }
    }
}
