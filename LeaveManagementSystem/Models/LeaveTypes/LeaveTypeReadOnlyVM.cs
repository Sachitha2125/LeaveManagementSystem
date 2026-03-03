using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Models.LeaveTypes
{
    public class LeaveTypeReadOnlyVM:BaseLeaveTypeVM
    {
        public string LeaveTypeName { get; set; } = string.Empty;

        [Display(Name = "Maximum Number of Days")]
        public int NumberOfDays { get; set; }

    }
}
