using LiteHR.Dtos;
using LiteHR.Helpers;
using LiteHR.Infrastructure;
using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Services
{
    public class LeaveTypeService:ILeaveTypeService
    {
        public readonly HRContext _context;
        private readonly IFileUpload _fileUpload;

        public LeaveTypeService(HRContext context, IFileUpload fileUpload)
        {
            _context = context;
            _fileUpload = fileUpload;

        }

        public async Task<long> AddLeaveType(string LeaveTypeName)
        {
            if(String.IsNullOrEmpty(LeaveTypeName))
                throw new AppException("Please provide required information");
                var lowerCaseName = LeaveTypeName.ToLower();
                var existingLeaveType=await _context.LEAVE_TYPE.Where(f => f.Name.ToLower() == lowerCaseName).FirstOrDefaultAsync();
                if (existingLeaveType != null)
                    throw new AppException("LeaveType already exist.");
                LeaveType leaveType = new LeaveType()
                {
                    Active = true,
                    Name = LeaveTypeName,

                };
                _context.Add(leaveType);
                var created=await _context.SaveChangesAsync();
                if (created > 0)
                {
                    return leaveType.Id;
                }
            
            return 0;
        }

        public async Task<IEnumerable<LeaveTypeDto>> GetLeaveType()
        {
           return await _context.LEAVE_TYPE
                .Select(f=>new LeaveTypeDto
                {
                    Id=f.Id,
                    Name=f.Name,
                    Active=f.Active
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<LeaveTypeRankDto>> GetLeaveTypeRankBy(long RankId)
        {

            return await _context.LEAVE_TYPE_RANK
                .Include(f=>f.LeaveType)
                 .Include(f => f.Rank)
                 .Where(f => f.RankId == RankId)
                 .Select(f => new LeaveTypeRankDto
                 {
                     Id = f.Id,
                     DurationInDays = f.Duration,
                     Name = f.LeaveType.Name,
                     LeaveTypeId = f.LeaveTypeId,
                     RankName = f.Rank.Name,
                     Active=f.Active
                 })
                 .ToListAsync();
        }
        public async Task<long> AddLeaveTypeRank(AddLeaveTypeRankDto addLeaveTypeRankDto)
        {
            if (addLeaveTypeRankDto == null)
                throw new AppException("Please provide required information");
            if (addLeaveTypeRankDto?.RankId == 0)
                throw new AppException("No staff rank selected");

                var existingLeaveTypeRank = await _context.LEAVE_TYPE_RANK.Where(f => f.RankId== addLeaveTypeRankDto.RankId && f.LeaveTypeId== addLeaveTypeRankDto.LeaveTypeId &&
                f.Active).FirstOrDefaultAsync();
                if (existingLeaveTypeRank != null)
                    throw new AppException("LeaveType already exist.");
                LeaveTypeRank leaveTypeRank = new LeaveTypeRank()
                {
                    Active = true,
                    RankId = addLeaveTypeRankDto.RankId,
                    Duration = addLeaveTypeRankDto.DurationInDays,
                    LeaveTypeId = addLeaveTypeRankDto.LeaveTypeId,

                };
                _context.Add(leaveTypeRank);
                var created = await _context.SaveChangesAsync();
                if (created > 0)
                {
                    return leaveTypeRank.Id;
                }
            
            return 0;
        }
    }
}
