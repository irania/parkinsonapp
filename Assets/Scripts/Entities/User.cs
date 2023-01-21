using System;

namespace Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Password { get; set; }
        
        public DateTime LastActivity { set; get; }
        
        public DateTime FirstActivity { set; get; }
        
        public string Email { set; get; }

        public Guid? AppId { get; set; }
        
    }

}