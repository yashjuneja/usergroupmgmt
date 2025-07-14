namespace UserGroupManagement.Repository.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; } = null!;
        //public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
        public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    }
}
