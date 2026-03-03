using LeaveManagementSystem.Models.LeaveTypes;

namespace LeaveManagementSystem.Services
{
    public interface ILeaveTypeService
    {
        Task<bool> CheckIfLeaveTypeNameExists(string name);
        Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leavetypeedit);
        Task Edit(LeaveTypeEditVM leavetypeedit);
        Task Create(LeaveTypeCreateVM model);
        Task<T> Get<T>(int id) where T : class;
        Task<List<LeaveTypeReadOnlyVM>> GetAllLeaveTypes();
        bool LeaveTypeExists(int id);
        Task Remove(int id);
    }
}
