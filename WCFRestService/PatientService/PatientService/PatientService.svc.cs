using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PatientService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PatientService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PatientService.svc or PatientService.svc.cs at the Solution Explorer and start debugging.
    public class PatientService : IPatientService
    {
        public IEnumerable<Patient> GetPatients()
        {
            using (var db = new PatientModel())
            {
                return db.Patients.ToList();
            }
        }

        public Patient GetPatient(int id)
        {
            using (var db = new PatientModel())
            {
                return db.Patients.SingleOrDefault(p => p.Id == id);
            }
        }
    }
}
