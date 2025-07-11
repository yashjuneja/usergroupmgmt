namespace UserGroupManagement.Repository.Entities
{
    public class GroupEntity
    {
        public int Id { get; set; }
        public string GroupName { get; set; } = null!;
        public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
    }
}
