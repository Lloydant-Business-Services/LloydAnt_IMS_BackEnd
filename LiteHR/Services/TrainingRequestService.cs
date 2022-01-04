using LiteHR.Dtos;
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
    public class TrainingRequestService : ITrainingRequestService
    {
        public TrainingRequestService(HRContext context)
        {
            _context = context;
        }

        public HRContext _context { get; }

        public async Task ApproveAsHR(StaffTrainingRequest trainingRequest)
        {
            var request = await GetTrainingRequestById(trainingRequest.Id);
            if (request != null)
            {
                request.Approved = true;
                request.ApprovedDate = DateTime.Now;
                request.ApprovedById = trainingRequest.Staff.Id;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ApproveAsSupervisor(StaffTrainingRequest trainingRequest)
        {
            var request = await GetTrainingRequestById(trainingRequest.Id);
            if (request != null)
            {
                request.Approved = true;
                request.ApprovedDate = DateTime.Now;
                request.ApprovedById = trainingRequest.Staff.Id;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteTrainingRequest(long Id)
        {
            int status = 0;
            var request = await GetTrainingRequestById(Id);
            if (request != null && !request.Approved)
            {
                _context.Remove(request);
                status = await _context.SaveChangesAsync();
            }
            return (status > 0) ? true : false;

        }

        public async Task<StaffTrainingRequest> GetTrainingRequestById(long Id)
        {
            return await _context.STAFF_TRAINING_REQUEST.Include(x => x.TrainingType).Include(x => x.Staff).ThenInclude(x => x.Person)
                .Include(x => x.Staff.Rank).Include(x => x.Staff.Department).SingleOrDefaultAsync(o => o.Id == Id);
        }

        public async Task<StaffTrainingRequest> GetTrainingRequestByStaff(Staff staff)
        {
            return await _context.STAFF_TRAINING_REQUEST.Include(x => x.TrainingType).Include(x => x.Staff).ThenInclude(x => x.Person)
                .Include(x => x.Staff.Rank).Include(x => x.Staff.Department).SingleOrDefaultAsync(o => o.Staff.Id == staff.Id);
        }

        public async Task<List<StaffTrainingRequest>> GetTrainingRequests()
        {
            return await _context.STAFF_TRAINING_REQUEST.Include(x => x.TrainingType).Include(x => x.Staff).ThenInclude(x => x.Person)
                .Include(x => x.Staff.Rank).Include(x => x.Staff.Department).ToListAsync();
        }

        public async Task<List<StaffTrainingRequest>> GetTrainingRequests(Staff staff)
        {
            return await _context.STAFF_TRAINING_REQUEST.Include(x => x.TrainingType).Include(x => x.Staff).ThenInclude(x => x.Person)
                .Include(x => x.Staff.Rank).Include(x => x.Staff.Department).Where(o => o.Staff.Id == staff.Id).ToListAsync();
        }

        public async Task<int> MakeRequest(StaffTrainingDto trainingDto)
        {

            try
            {
                StaffTrainingRequest trainingRequest = new StaffTrainingRequest();
                trainingRequest.TrainingTypeId = trainingDto.TrainingTypeId;
                trainingRequest.StartDate = trainingDto.StartDate;
                trainingRequest.EndDate = trainingDto.EndDate;
                trainingRequest.StaffId = trainingDto.StaffId;
                trainingRequest.Reason = trainingDto.Reason;
                _context.Add(trainingRequest);
                await _context.SaveChangesAsync();
                return StatusCodes.Status200OK;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateRequest(long Id, StaffTrainingRequest trainingRequest)
        {
            int status = 0;
            var request = await GetTrainingRequestById(Id);
            if (request != null)
            {
                if (!request.Approved && trainingRequest.Approved)
                {
                    request.ApprovedDate = DateTime.Now;
                }
                request.Approved = trainingRequest.Approved;
                request.ApprovedById = trainingRequest.ApprovedById;
                request.StartDate = trainingRequest.StartDate;
                request.EndDate = trainingRequest.EndDate;
                request.AttachmentUrl = trainingRequest.AttachmentUrl;
                request.Reason = trainingRequest.Reason;
                request.TrainingTypeId = trainingRequest.TrainingTypeId;
                status = await _context.SaveChangesAsync();
            }

            return (status > 0) ? true : false;

        }
        public async Task<IEnumerable<TrainingScheduleDto>> GetTrainingSchedule(long DeptId)
        {
            return await _context.STAFF.Where(d => d.DepartmentId == DeptId)
                .Include(p => p.Person)
                .Include(d => d.Department)
                .ThenInclude(f => f.Faculty)
                .Select(s => new TrainingScheduleDto { 
                    StaffName = s.Person.Surname + " " + s.Person.Firstname + " " + s.Person.Othername,
                    FacultyUnit = s.Department.Faculty.Name,
                    Username = s.GeneratedStaffNumber,
                    StaffNumber = s.StaffNumber != null ? s.StaffNumber : "-"
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TrainingScheduleDto>> GetTrainingScheduleFaculty(long FacId)
        {
            return await _context.STAFF.Where(d => d.Department.FacultyId == FacId)
                .Include(p => p.Person)
                .Include(d => d.Department)
                .ThenInclude(f => f.Faculty)
                .Select(s => new TrainingScheduleDto
                {
                    StaffName = s.Person.Surname + " " + s.Person.Firstname + " " + s.Person.Othername,
                    FacultyUnit = s.Department.Faculty.Name,
                    Username = s.GeneratedStaffNumber,
                    StaffNumber = s.StaffNumber != null ? s.StaffNumber : "-"
                })
                .ToListAsync();
        }
    }
}
