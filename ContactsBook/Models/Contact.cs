using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ContactsBook.Models
{   
    
    public class Contact
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        public string HomePhone { get; set; }

        public string BirthDay { get; set; }


        public string WorkPhone { get; set; }

        public string Location { get; set; }

        public string Notes { get; set; }

        public string imageUrl { get; set; }
        
    }
}
