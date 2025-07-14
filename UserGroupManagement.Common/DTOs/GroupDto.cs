namespace UserGroupManagement.Common.DTOs
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; } = null!;
        public List<UserDto> Members { get; set; } = new();
    }
}