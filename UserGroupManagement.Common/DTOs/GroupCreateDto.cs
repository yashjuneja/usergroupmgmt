namespace UserGroupManagement.Common.DTOs
{
    public class GroupCreateDto
    {
        public string GroupName { get; set; }
        public List<int> MemberIds { get; set; } = new List<int>();
    }
}
