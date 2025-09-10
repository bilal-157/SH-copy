using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public static class Messages
    {
        public static string Header = "Jewel Manager " + DateTime.Now.Year + "";
        public static string Saved = "Data Saved Sucessfully";
        public static string Updated = "Data Update Sucessfully";
        public static string Deleted = "Data Deleted Sucessfully";
        public static string Empty = " is Required Field..";
        public static string Sure = " Are you sure to Save this Record....??";
        public static string DeleteWarning = " Are you sure to Delete this Record....??";
        public static string NotFound = "Record Not Found";
    }
}
