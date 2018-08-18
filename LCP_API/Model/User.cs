namespace LCP_API
{
    public class User
    {
        // This one shouldn't be to hard to understand

        public int Id { get; set; }  // The id is unique and given by the server the first time you sign up 
        public string Username { get; set; }

        public User() { }

        public User(int id, string username)
        {
            Id = id;
            Username = username;
        }
    }
}
