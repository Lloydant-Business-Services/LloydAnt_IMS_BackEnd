using LiteHR.Dtos;
using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface ITrainingRequestService
    {
        Task<StaffTrainingRequest> GetTrainingRequestById(long Id);
        Task<bool> DeleteTrainingRequest(long Id);
        Task<List<StaffTrainingRequest>> GetTrainingRequests();
        Task<StaffTrainingRequest> GetTrainingRequestByStaff(Staff staff);
        Task<List<StaffTrainingRequest>> GetTrainingRequests(Staff staff);
        Task<int> MakeRequest(StaffTrainingDto trainingDto);
        //Task<StaffTrainingRequest> MakeRequest(StaffTrainingRequest trainingRequest);
        Task<bool> UpdateRequest(long Id, StaffTrainingRequest trainingRequest);
        Task ApproveAsSupervisor(StaffTrainingRequest trainingRequest);
        Task ApproveAsHR(StaffTrainingRequest trainingRequest);
        Task<IEnumerable<TrainingScheduleDto>> GetTrainingSchedule(long DeptId);
        Task<IEnumerable<TrainingScheduleDto>> GetTrainingScheduleFaculty(long FacId);
    }
}
