using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Delfos.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique, MaxLength(50)]
        public string Username { get; set; }

        [Unique, MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Password { get; set; }
    }
}
