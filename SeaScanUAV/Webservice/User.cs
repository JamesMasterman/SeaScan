using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace SeaScanUAV
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int ID{get; set;}

        [DataMember]
        public string UserName{get; set;}

        [DataMember]
        public string Password{get; set;}

        [DataMember]
        public string FirstName{get; set;}

        [DataMember]
        public string LastName{get; set;}

        [DataMember]
        public string Description{get; set;}

        [DataMember]
        public int    Enabled{get; set;}

        [DataMember]
        public int    IsAdmin{get; set;}      
     
        public User()
        {
        }

        public User(int ID, string userName, string password, string firstName, string lastName, string description, int isAdmin)
        {
          this.ID = ID;
          this.UserName = userName;
          this.Password = password;
          this.FirstName = firstName;
          this.LastName = lastName;
          this.Description = description;
          this.IsAdmin = isAdmin;
        
        }


        public override string ToString()
        {
            return FirstName + " " + LastName;           
        }

    }
}
