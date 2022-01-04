using LiteHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Dtos
{
    public class ExcelUploadRecordCountDto
    {
        public string Message { get; set; }
        public long SuccessfullUpload { get; set; }
        public long FailedUpload { get; set; }
        public long UpdatedRecords { get; set; }
        public List<FailedAttendanceUpload> FailedAttendanceUpload { get; set; }
        public List<FaileStaffBiometricModel> FailedBiometricsUpload { get; set; }
    }
}
