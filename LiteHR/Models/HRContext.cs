using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LiteHR.Models;

namespace LiteHR.Models
{
    public class HRContext : DbContext
    {
        public HRContext(DbContextOptions<HRContext> options) : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelbuilder)
        //{
        //    modelbuilder.Entity<Faculty>().HasData(
        //        new Faculty
        //        {
        //            Id = 1,
        //            Name = "-",
        //            Active = true
        //        }
        //        );
        //    modelbuilder.Entity<ParentMenu>().HasData(
        //        new ParentMenu
        //        {
        //            Id = 1,
        //            Name = "-",
        //            Active = true
        //        }
            
        //        );
        //    foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(f => f.GetForeignKeys()))
        //    {
        //        relationship.DeleteBehavior = DeleteBehavior.Restrict;
        //    }
        //    base.OnModelCreating(modelbuilder);
        //}
        public DbSet<Asset> ASSET { get; set; }
        public DbSet<AssetType> ASSET_TYPE { get; set; }
        public DbSet<Broadcast> BROADCAST { get; set; }
        public DbSet<Event> EVENT { get; set; }
        public DbSet<Gender> GENDER { get; set; }
        public DbSet<InstitutionAppointment> INSTITUTION_APPOINTMENT { get; set; }
        public DbSet<InstitutionCalendar> INSTITUTION_CALENDAR { get; set; }
        public DbSet<InstitutionCalendarDetail> INSTITUTION_CALENDAR_DETAIL { get; set; }
        public DbSet<InstitutionChart> INSTITUTION_CHART { get; set; }
        public DbSet<InstitutionChartDetail> INSTITUTION_CHART_DETAIL { get; set; }
        public DbSet<InstitutionDepartment> INSTITUTION_DEPARTMENT { get; set; }
        public DbSet<InstitutionRank> INSTITUTION_RANK { get; set; }
        public DbSet<InstitutionStaffCategory> INSTITUTION_STAFF_CATEGORY { get; set; }
        public DbSet<InstitutionStaffType> INSTITUTION_STAFF_TYPE { get; set; }
        public DbSet<InstitutionUnit> INSTITUTION_UNIT { get; set; }
        public DbSet<Leave> LEAVE { get; set; }
        public DbSet<LeaveAssignment> LEAVE_ASSIGNMENT { get; set; }
        public DbSet<Lga> LGA { get; set; }
        public DbSet<MaritalStatus> MARITAL_STATUS { get; set; }
        public DbSet<Person> PERSON { get; set; }
        public DbSet<Religion> RELIGION { get; set; }
        public DbSet<Settings> SETTINGS { get; set; }
        public DbSet<Staff> STAFF { get; set; }
        public DbSet<StaffAssets> STAFF_ASSET { get; set; }
        public DbSet<StaffAttendance> STAFF_ATTENDANCE { get; set; }
        //public DbSet<StaffHistory> STAFF_HISTORY { get; set; }
        //public DbSet<StaffLeaveRequest> STAFF_LEAVE_REQUEST { get; set; }
        public DbSet<StaffTrainingRequest> STAFF_TRAINING_REQUEST { get; set; }
        public DbSet<State> STATE { get; set; }
        public DbSet<TrainingType> TRAINING_TYPE { get; set; }
        public DbSet<TrainingTypeAssignment> TRAINING_TYPE_ASSIGNMENT { get; set; }
        public DbSet<User> USER { get; set; }
        public DbSet<Role> ROLE { get; set; }
        //public DbSet<ApplicationForm> APPLICATION { get; set; }
        public DbSet<EducationalQualification> EDUCATIONAL_QUALIFICATION { get; set; }
        public DbSet<PersonCertification> PERSON_CERTIFICATION { get; set; }
        public DbSet<PersonEducation> PERSON_EDUCATION { get; set; }
        public DbSet<PersonExperience> PERSON_EXPERIENCE { get; set; }
        public DbSet<PersonJournal> PERSON_JOURNAL { get; set; }
        public DbSet<PersonProfessionalBody> PERSON_PROFESSIONAL_BODY { get; set; }
        public DbSet<PersonReferee> PERSON_REFEREE { get; set; }
        public DbSet<PersonResearchGrant> PERSON_RESEARCH_GRANT { get; set; }
        public DbSet<JobVacancy> JOB_VACANCY { get; set; }
        public DbSet<JobType> JOB_TYPE { get; set; }
        public DbSet<ApplicationSectionHeader> APPLICATION_SECTION_HEADER { get; set; }
        public DbSet<ApplicationSectionWeight> APPLICATION_SECTION_WEIGHT { get; set; }
        public DbSet<StaffStatus> STAFF_STATUS { get; set; }

        public DbSet<GradeBenefit> GRADE_BENEFIT { get; set; }

        public DbSet<StaffGrade> STAFF_GRADE { get; set; }

        public DbSet<SalaryType> SALARY_TYPE { get; set; }

        public DbSet<SalaryGrade> SALARY_GRADE { get; set; }

        public DbSet<SalaryGradeCategory> SALARY_GRADE_CATEGORY { get; set; }

        public DbSet<SalaryLevel> SALARY_LEVEL { get; set; }

        public DbSet<SalaryStep> SALARY_STEP { get; set; }

        public DbSet<SalaryExtraEarning> SALARY_EXTRA_EARNING { get; set; }

        public DbSet<SalaryExtraType> SALARY_EXTRA_TYPE { get; set; }

        public DbSet<StaffRetirement> STAFF_RETIREMENT { get; set; }
        public DbSet<AppointmentType> APPOINTMENT_TYPE { get; set; }
        public DbSet<LeaveType> LEAVE_TYPE { get; set; }
        public DbSet<LeaveRequest> LEAVE_REQUEST { get; set; }
        //public DbSet<LeaveResponseChain> LEAVE_RESPONSE_CHAIN { get; set; }
        //public DbSet<LeaveResponse> LEAVE_RESPONSE { get; set; }
        public DbSet<StaffDocument> STAFF_DOCUMENT { get; set; }
        public DbSet<DocumentType> DOCUMENT_TYPE { get; set; }
        public DbSet<LeaveTypeRank> LEAVE_TYPE_RANK { get; set; }
        public DbSet<DepartmentHead> DEPARTMENT_HEAD { get; set; }
        public DbSet<StaffLeaveRecord> STAFF_LEAVE_RECORD { get; set; }
        public DbSet<Menu> MENU { get; set; }
        public DbSet<ChangeOfName> CHANGE_OF_NAME { get; set; }
        public DbSet<DepartmentTransfer> DEPARTMENT_TRANSFER { get; set; }
        public DbSet<Faculty> FACULTY { get; set; }
        public DbSet<ParentMenu> PARENT_MENU { get; set; }
        public DbSet<StaffNominalRoll> STAFF_NOMINAL_ROLL { get; set; }
        public DbSet<StaffSalaryCategoryReference> STAFF_SALARY_CATEGORY_REFERENCE { get; set; }
        public DbSet<GeneratedStaffNumberRecords> GENERATED_STAFFNUMBER_RECORDS { get; set; }
        public DbSet<JobRecipients> JOB_RECIPIENTS { get; set; }
        public DbSet<Holidays> HOLIDAYS { get; set; }
        public DbSet<FailedStaffUploads> FAILED_STAFF_UPLOADS { get; set; }
        public DbSet<PfaName> PFA_NAME { get; set; }
        public DbSet<PfaStatus> PFA_STATUS { get; set; }
        public DbSet<AreaOfSpecialization> AREA_OF_SPECIALIZATION { get; set; }
        public DbSet<StaffAdditionalInfo> STAFF_ADDITIONAL_INFO { get; set; }
        //public DbSet<InstitutionMemorandum> INSTITUITION_MEMORANDUM { get; set; }
        //public DbSet<InstitutionMemorandumTarget> INSTITUITION_MEMORANDUM_TARGET { get; set; }
        public DbSet<ForeignVisitation> FOREIGN_VISITATION { get; set; }
        public DbSet<Country> COUNTRY { get; set; }
        public DbSet<PersonNextOfKin> PERSON_NEXT_OF_KIN { get; set; }
        public DbSet<VisitationType> VISITATION_TYPE { get; set; }
       //public DbSet<ActiveMemoDesk> ACTIVE_MEMO_DESK { get; set; }
       public DbSet<Mailing> MAILING { get; set; }
       //public DbSet<MailingDeskChain> MAILING_DESK_CHAIN { get; set; }
       //public DbSet<MailingComments> MAILING_COMMENTS { get; set; }
       public DbSet<StaffDBCommitDetails> STAFF_DB_COMMIT_DETAILS { get; set; }
       //public DbSet<MailArchiveFileType> MAIL_ARCHIVE_FILE_TYPE { get; set; }
       //public DbSet<MailArchive> MAIL_ARCHIVE { get; set; }

        //public DbSet<LeaveStatusDetail> LEAVE_STATUS_DETAIL { get; set; }

    }
}