using LiteHR.Dtos;
using LiteHR.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Interface
{
    public interface IServicomService
    {
        Task<ExcelUploadRecordCountDto> ProcessStaffAttendanceFromExcel(IEnumerable<AttendanceExcelModel> attendanceModel);
        Task<ExcelUploadRecordCountDto> ProcessStaffBiometricInfoFromExcel(IEnumerable<BiometricExcelModel> biometricModel);
        Task<CustomOkObject> GetAttendanceReportByDepartmentAndDate(DateTime From, DateTime To, long DepartmentId);
    }
}
