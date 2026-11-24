using Hackathon_Desync_Structure.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackathon_Desync_Structure.Core.Services
{
    public class CurrentUserService
    {
        public Person? User { get; private set; }
        public bool IsLoggedIn => User != null;
        public bool IsTeacher => User?.Role == "Teacher";

        public void Login(Person person) => User = person;
        public void Logout() => User = null;

    }
}
