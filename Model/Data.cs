using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace arun.Model
{
    public class Data
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Age { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string MartialStatus { get; set; }
        [DataMember]
        public string Occupation { get; set; }
        [DataMember]
        public string NoofChildrens { get; set; }
        [DataMember]
        public string AnnualIncome { get; set; }
        [DataMember]
        public string Education { get; set; }
        [DataMember] 
        public string LivingStatus { get; set; }
        [DataMember]
        public string Nationality { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string MobileNumber { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string BloodGroup { get; set; }
        [DataMember]
        public string FatherName { get; set; }
        [DataMember]
        public string DateOfBirth { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string PersonId { get; set; }
        [DataMember]
        public string SlotID { get; set; }
        [DataMember]
        public string Latitude { get; set; }
        [DataMember]
        public string Longitude { get; set; }
        [DataMember]
        public int IsSystem { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string ChangePassword { get; set; }
    }
}
