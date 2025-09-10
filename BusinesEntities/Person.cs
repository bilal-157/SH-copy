using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
 public    class Person
    {
     public string Name { get; set; }
     public string ContactNo { get; set; }
     private Nullable<DateTime> dateOfBirth=null;

     public Nullable<DateTime> DateOfBirth
     {
         get { return dateOfBirth; }
         set { dateOfBirth = value; }
     }
     
     public string Email { get; set; }
     public string Address { get; set; }
     public decimal TPurity { get; set; }
     public string Address2 { get; set; }
     public string Phase { get; set; }
    // public int ID { get; set; }
     private Nullable<int> iD = null;

     public Nullable<int> ID
     {
         get { return iD; }
         set { iD = value; }
     }
     public Person(int pid)
     {
         this.ID = pid;
     }
     public Person(int pid, string pname)
     {
         this.ID = pid;
         this.Name = pname;
     }
     public Person(int pid, string pname, string contact, string address)
     {
         this.ID = pid;
         this.Name = pname;
         this.ContactNo = contact;
         this.Address = address;
     }
     public Person() { }
    }
}
