namespace Practical_17.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }  // Admin or User

        public ICollection<User> Users { get; set; }
    }

}
