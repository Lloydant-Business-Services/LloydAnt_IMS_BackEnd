using LiteHR.Dtos;
using LiteHR.Helpers;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Services
{
    public class DepartmentTransferService:IDepartmentTransferService
    {
        public readonly HRContext _context;

        public DepartmentTransferService(HRContext context)
        {
            _context = context;
        }

      

        public async Task<int> MakeDepartmentTransferRequest(DepartmentTransferDto departmentTransferDto)
        {

            var checkDept = await _context.INSTITUTION_DEPARTMENT.Where(d => d.Id == departmentTransferDto.NewDepartmentId).FirstOrDefaultAsync();
            var checkStaff = await _context.STAFF.Where(s => s.Id == departmentTransferDto.StaffId).FirstOrDefaultAsync();

            //Check if a request is still active and unattended to
            var activeRequest = await _context.DEPARTMENT_TRANSFER.Where(d => d.StaffId == departmentTransferDto.StaffId && d.IsClosed == false && d.IsApproved == false).FirstOrDefaultAsync();
            if (activeRequest != null)
                return StatusCodes.Status406NotAcceptable;

            if (checkDept == null || checkStaff == null)
                return StatusCodes.Status404NotFound;
            DepartmentTransfer departmentTransfer = new DepartmentTransfer()
            {
                DeptFrom = departmentTransferDto.CurrentDepartment,
                DeptTo = checkDept.Name,
                NewDepartmentId = departmentTransferDto.NewDepartmentId,
                StaffId = departmentTransferDto.StaffId,
                DateOfRequest = DateTime.Now,
                Reasons = departmentTransferDto.Reasons,
                IsApproved = false,
                IsClosed = false
            };
            _context.Add(departmentTransfer);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;


           
        }

        public async Task<int> ActOnTransferRequest(ActOnDeptTransferDto actOnDeptTransferDto)
        {
            var getStaff = await _context.STAFF.Where(s => s.Id == actOnDeptTransferDto.StaffId).FirstOrDefaultAsync();

            //Check against double approval/decline
            var requestRecord = await _context.DEPARTMENT_TRANSFER.Where(d => d.StaffId == actOnDeptTransferDto.StaffId && d.IsClosed == false).FirstOrDefaultAsync();
            if (requestRecord == null)
                throw new AppException("Already Acted Upon Request");
            if(!actOnDeptTransferDto.ApproveRequest)
            {
                requestRecord.IsApproved = false;
                requestRecord.IsClosed = true;
                _context.Update(requestRecord);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;

            }else if (actOnDeptTransferDto.ApproveRequest)
            {
                requestRecord.IsApproved = true;
                requestRecord.IsClosed = true;
                _context.Update(requestRecord);
                getStaff.DepartmentId = requestRecord.NewDepartmentId;
                _context.Update(getStaff);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }
            return 0;
           
        }

        public async Task<IEnumerable<DepartmentTransfterListDto>> GetListByAdmin()
        {
            return await _context.DEPARTMENT_TRANSFER
                .Include(s => s.Staff)
                .Include(p => p.Staff.Person)
                .Select(f => new DepartmentTransfterListDto
                {
                    CurrentDepartment = f.DeptFrom,
                    NewDepartment = f.DeptTo,
                    Reasons = f.Reasons,
                    StaffId = f.StaffId,
                    NewDepartmentId = f.NewDepartmentId,
                    StaffNumber = f.Staff.GeneratedStaffNumber,
                    DateOfRequest = f.DateOfRequest,
                    IsApproved = f.IsApproved,
                    IsClosed = f.IsClosed,
                    StaffName = f.Staff.Person.Surname + " " + f.Staff.Person.Firstname + " " + f.Staff.Person.Othername
                }).ToListAsync();
        }


        public async Task<IEnumerable<DepartmentTransfterListDto>> GetListByStaff(long staffId)
        {
            return await _context.DEPARTMENT_TRANSFER
                .Where(s => s.StaffId == staffId)
                .Select(f => new DepartmentTransfterListDto
                {
                    CurrentDepartment = f.DeptFrom,
                    NewDepartment = f.DeptTo,
                    Reasons = f.Reasons,
                    NewDepartmentId = f.NewDepartmentId,
                    DateOfRequest = f.DateOfRequest,
                    IsApproved = f.IsApproved,
                    IsClosed = f.IsClosed
                }).ToListAsync();
        }

    }
}
