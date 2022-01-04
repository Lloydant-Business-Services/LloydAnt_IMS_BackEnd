using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR.Infrastructure
{
    public enum ApplicationEnum
    {
        BioData=1,
        EducationandQualification=2,
        ProfessionalBody=3,
        JournalandPublication=4,
        WorkExperience=5,
        ResearchGrant=6,
        ProfessionalQualification=7,
        Referee=8
    }

    public enum StaffType
    {
        NonAcademicStaff = 1,
        AcademicStaff = 2
    }
    public enum AppointmentTypes
    {
        Regular = 1,
        Temporal = 2
    }
    public enum UserRole
    {
        SuperAdmin=1,
        HRAdmin=2,
        Staff=3,
        Regularization=4,
        Dean=5,
        VC=6,
        HOD=7,
        DeputyRegistrar = 13,

        //Local
        VC_Secretary = 18,
        VC_OutgoingMailOfficer = 19,
        VC_IncomingMailOfficer = 16,

        //------------------------

        //Production
        //VC_IncomingMailOfficer = 16,
        //VC_OutgoingMailOfficer = 17,
        //VC_Secretary = 18,

    }

    public enum ProgressTrack
    {
        HOD_Phase = 1,
        Dean_Phase = 2,
        VC_Phase = 3,
        Declined = 4,
        Processing = 0
    }

    public enum StatusCode
    {
        Status = 200,
        FailedResponse = 400,
        NotClosed = 300,
        NoAssumptionOfDuty = 701,
        NoSalaryRecord = 702,
        AssumptionSalaryNull = 703,
        NullDepartment = 704,
        RequiredDataNull = 705,
        NoChainSet = 706,
        DurationDisparity = 707,
        Ineligible = 708

    }
    public enum ProgressPercentage
    {
        CaseOne = 33,
        CaseTwo = 66,
        CaseThree = 100,

    }
    public enum LeaveRep
    {
        AnnualLeave = 7
    }

    public enum StatusProgress
    {
        Approved = 1,
        Processing = 2,
        Declined = 3
    }

    public enum StaffServiceStatus
    {
        Disengaged = 1,
        Retired = 2
    }

    public enum MenuList
    {
        StandAlone = 1,
        
    }

    public enum VerificationCategories
    {
       
        JobRecipient = 1,
        PasswordReset = 2,

    }

    public enum MailingActionType
    {
        Acknowledged = 1,
        Rejected = 2,
        AcknowledgeAndForward = 3
    }

}
