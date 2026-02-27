using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Models.LeaveTypes
{
    public class IndexVM
    {
        public int LeaveTypeId { get; set; }

        [Required]
        [Length(4,150, ErrorMessage = "Leave Type Name must be between 4 and 150 characters.")]
        public string LeaveTypeName { get; set; }= string.Empty;

        [Required]
        [Range(1,90)]
        public int Days { get; set; }
    }
}
