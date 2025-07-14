namespace UserGroupManagement.Repository.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
        public string Email { get; set; } = null!;
        //public ICollection<GroupEntity> Groups { get; set; } = new List<GroupEntity>();
        public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    }
}
