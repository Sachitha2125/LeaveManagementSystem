using AutoMapper;
using LeaveManagementSystem.Data;
using LeaveManagementSystem.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LeaveManagementSystem.Services
{

    public class LeaveTypeService(ApplicationDbContext _context, IMapper _mapper) : ILeaveTypeService
    {
        public async Task<List<LeaveTypeReadOnlyVM>> GetAllLeaveTypes()
        {
            //var data=SELECT * FROM LeaveType
            var data = await _context.LeaveTypes.ToListAsync();
            var viewData = _mapper.Map<List<LeaveTypeReadOnlyVM>>(data);
            return viewData;

        }
        public async Task<T> Get<T>(int id) where T : class
        {
            var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.LeaveTypeId == id);
            if (data == null)
            {
                return default;
            }
            var viewData = _mapper.Map<T>(data);
            return viewData;
        }
        public async Task Remove(int id)
        {
            var data = await _context.LeaveTypes.FirstOrDefaultAsync(x => x.LeaveTypeId == id);
            if (data != null)
            {
                _context.Remove(data);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Edit(LeaveTypeEditVM model)
        {
            var leaveType = _mapper.Map<LeaveType>(model);

            _context.Update(leaveType);
            await _context.SaveChangesAsync();

        }
        public async Task Create(LeaveTypeCreateVM model)
        {
            var leaveType = _mapper.Map<LeaveType>(model);

            _context.Add(leaveType);
            await _context.SaveChangesAsync();
        }
         public bool LeaveTypeExists(int id)
        {
            return _context.LeaveTypes.Any(e => e.LeaveTypeId == id);
        }
        public async Task<bool> CheckIfLeaveTypeNameExists(string name)
        {
            var lowercaseName = name.ToLower();
            return await _context.LeaveTypes.AnyAsync(q => q.LeaveTypeName.ToLower() == lowercaseName);
        }
        public async Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leavetypeedit)
        {
            var lowercaseName = leavetypeedit.LeaveTypeName.ToLower();
            return await _context.LeaveTypes.AnyAsync(q => q.LeaveTypeName.ToLower().Equals(lowercaseName) && q.LeaveTypeId != leavetypeedit.LeaveTypeId);

        }
    }
}
