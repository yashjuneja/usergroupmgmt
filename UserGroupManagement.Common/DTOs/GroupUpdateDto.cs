namespace UserGroupManagement.Common.DTOs
{
    public class GroupUpdateDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; } = null!;
        public List<int> MemberIds { get; set; } = new();
    }
}
