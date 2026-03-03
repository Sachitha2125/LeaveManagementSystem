using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Models.LeaveTypes
{
    public class LeaveTypeEditVM: BaseLeaveTypeVM
    {
        [Required]
        [Length(4, 150, ErrorMessage = "Leave Type Name must be between 4 and 150 characters.")]
        public string LeaveTypeName { get; set; } = string.Empty;
        [Required]
        [Range(1, 90)]
        [Display(Name = "Number of Days")]
        public int NumberOfDays { get; set; }
    }
}
