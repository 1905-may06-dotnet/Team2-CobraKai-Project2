namespace Project.Domain
{
    public class Person
    {
        //gets or sets User ID
        //to be referenced in Journal and PlayList tables
        public int Id { get; set; }

        //gets or sets user's email
        public string Email { get; set; }
        //gets or sets user's username
        public string Username { get; set; }
        //gets or sets user's password
        public string Password { get; set; }
        //gets or sets user's first name
        public string FirstName { get; set; }
        //gets or sets user's last name
        public string LastName { get; set; }
    }
}
