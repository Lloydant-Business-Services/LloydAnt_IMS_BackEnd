using LiteHR.Interface;
using LiteHR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteHR
{
    public static class DbInitializer
    {
        public async static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new HRContext(serviceProvider.GetRequiredService<DbContextOptions<HRContext>>()))
            {

                context.Database.EnsureCreated();
                // Look for any students.
                if (await context.ROLE.AnyAsync())
                {
                    return;   // DB has been seeded
                }

                var user = new User
                {
                    Username = "admin",
                    Password = "password",
                    Staff = new Staff
                    {
                        StaffNumber = "N/99/A2344",
                        Person = new Person
                        {
                            Surname = "Nefefe",
                            Firstname = "Tempus",
                            BirthDay = DateTime.Now,
                            Email = "tempusnefefe@gmail.com",
                            PhoneNumber = "08046107232"

                        }
                    }
                };

                var roles = new Role[]
                  {
                    new Role{ Active = true, Name = "Super Admin"},
                    new Role{ Active = true, Name = "HR Admin"},
                    new Role{ Active = true, Name = "Staff"},
                    new Role{ Active = true, Name = "Regularization"},
                    new Role{ Active = true, Name = "Dean"},
                    new Role{ Active = true, Name = "Vice Chancellor"},
                    new Role{ Active = true, Name = "HOD"},


                  };
                foreach (Role role in roles)
                {
                    context.Add(role);
                }
                await context.SaveChangesAsync();

             

                var assetTypes = new AssetType[]
                {
                new AssetType{ Active = true, Name = "Furniture"},
                new AssetType{ Active = true, Name = "Computer"},
                new AssetType{ Active = true, Name = "Book"},
                };
                foreach (AssetType asset in assetTypes)
                {
                    context.Add(asset);
                }
                await context.SaveChangesAsync();



              





                var appointments = new InstitutionAppointment[]
                {
                new InstitutionAppointment { Active = true, Name = "HOD"},
                new InstitutionAppointment { Active = true, Name = "Dean"},
                new InstitutionAppointment { Active = true, Name = "Registrar"},
                new InstitutionAppointment { Active = true, Name = "Deputy Vice Chancellor"},
                new InstitutionAppointment { Active = true, Name = "Vice Chancellor"},
                new InstitutionAppointment { Active = true, Name = "Chancellor"},

   
                };
                foreach (InstitutionAppointment appointment in appointments)
                {
                    context.Add(appointment);
                }
                await context.SaveChangesAsync();

                var genders = new Gender[]
                {
                    new Gender { Active = true, Name = "Male"},
                    new Gender { Active = true, Name = "Female"},
                };
                foreach (Gender gender in genders)
                {
                    context.Add(gender);
                }
                await context.SaveChangesAsync();


               // var states = new State[]
               //{
               //     new State { Active = true, Name = "Abia"},
               //     new State { Active = true, Name = "Adamawa"},
               //};
               // foreach (State state in states)
               // {
               //     context.Add(state);
               // }
               // await context.SaveChangesAsync();


               // var lgas = new Lga[]
               //{
               //     new Lga { Active = true, Name = "Umuahia North", StateId = 1},
               //     new Lga { Active = true, Name = "Adamawa North", StateId = 2},
               //};
               // foreach (Lga lga in lgas)
               // {
               //     context.Add(lga);
               // }
               // await context.SaveChangesAsync();


                var maritalStatuses = new MaritalStatus[]
               {
                    new MaritalStatus { Active = true, Name = "Single"},
                    new MaritalStatus { Active = true, Name = "Married"},
                    new MaritalStatus { Active = true, Name = "Divorced"},
                    new MaritalStatus { Active = true, Name = "Widowed"},
               };
                foreach (MaritalStatus maritalStatus in maritalStatuses)
                {
                    context.Add(maritalStatus);
                }
                await context.SaveChangesAsync();

                var religions = new Religion[]
             {
                    new Religion { Active = true, Name = "Christian"},
                    new Religion { Active = true, Name = "Muslim"},
                    new Religion { Active = true, Name = "Aetheist"},
                    new Religion { Active = true, Name = "African Traditionalist"},
                    new Religion { Active = true, Name = "Other"},
             };
                foreach (Religion religion in religions)
                {
                    context.Add(religion);
                }
                await context.SaveChangesAsync();



                //var units = new InstitutionUnit[]
                //{
                //new InstitutionUnit { Active = true, Name = "Bursary/Accountant"},
                //new InstitutionUnit { Active = true, Name = "Registry"},
                //new InstitutionUnit { Active = true, Name = "Administrative"},
                //new InstitutionUnit { Active = true, Name = "Engineers"},
                //new InstitutionUnit { Active = true, Name = "Estate Officer"},
                //new InstitutionUnit { Active = true, Name = "Executive Officer"},
                //new InstitutionUnit { Active = true, Name = "Architect"},
                //new InstitutionUnit { Active = true, Name = "Chief Clerical Officer"},
                //new InstitutionUnit { Active = true, Name = "Executive Officer"},
                //new InstitutionUnit { Active = true, Name = "Computer System Analyst"},
                //new InstitutionUnit { Active = true, Name = "Craftsman"},
                //new InstitutionUnit { Active = true, Name = "Data Procesing"},
                //new InstitutionUnit { Active = true, Name = "Dean Of Student Affairs"},
                //new InstitutionUnit { Active = true, Name = "Directors Of Directorates"},
                //new InstitutionUnit { Active = true, Name = "Directorate Of Sports"},
                //new InstitutionUnit { Active = true, Name = "Engineers"},
                //new InstitutionUnit { Active = true, Name = "Health Assistance"},
                //new InstitutionUnit { Active = true, Name = "Health Technician"},
                //new InstitutionUnit { Active = true, Name = "Programmer"},
                //new InstitutionUnit { Active = true, Name = "Lectureship"},
                //new InstitutionUnit { Active = true, Name = "Legal Officer"},
                //new InstitutionUnit { Active = true, Name = "Librarian Officer"},
                //};
                //foreach (InstitutionUnit unit in units)
                //{
                //    context.Add(unit);
                //}
                //await context.SaveChangesAsync();

                var departments = new InstitutionDepartment[]
                {
                new InstitutionDepartment { Active = true, Name = "Bursary"},
                new InstitutionDepartment { Active = true, Name = "Registry"},
                new InstitutionDepartment { Active = true, Name = "Electrical Engineering"},
                new InstitutionDepartment { Active = true, Name = "Accountancy"},
                new InstitutionDepartment { Active = true, Name = "Anatomy"},
                new InstitutionDepartment { Active = true, Name = "Public Administration"},
                };
                foreach (InstitutionDepartment department in departments)
                {
                    context.Add(department);
                }
                await context.SaveChangesAsync();

               // var ranks = new InstitutionRank[]
               //{
               // new InstitutionRank { Active = true, Name = "Bursar"},
               // new InstitutionRank { Active = true, Name = "Registrar"},
               // new InstitutionRank { Active = true, Name = "Lecturer I"},
               // new InstitutionRank { Active = true, Name = "Lecturer II"},
               // new InstitutionRank { Active = true, Name = "Lecturer III"},
               // new InstitutionRank { Active = true, Name = "Graduate Assistant"},
               //};
               // foreach (InstitutionRank rank in ranks)
               // {
               //     context.Add(rank);
               // }
               // await context.SaveChangesAsync();

                var staffCategories = new InstitutionStaffCategory[]
                  {
                    new InstitutionStaffCategory { Active = true, Name = "Permanent"},
                    new InstitutionStaffCategory { Active = true, Name = "Contract"},
                  };
                foreach (InstitutionStaffCategory category in staffCategories)
                {
                    context.Add(category);
                }
                await context.SaveChangesAsync();

                var staffTypes = new InstitutionStaffType[]
                 {
                    new InstitutionStaffType { Active = true, Name = "Academic"},
                    new InstitutionStaffType { Active = true, Name = "Non-Academic"},
                 };
                foreach (InstitutionStaffType staffType in staffTypes)
                {
                    context.Add(staffType);
                }
                await context.SaveChangesAsync();

                var leaves = new Leave[]
                {
                    new Leave { Active = true, Name = "Casual", StaffTypeId = 1},
                    new Leave { Active = true, Name = "Casual", StaffTypeId = 2},
                    new Leave { Active = true, Name = "Annual",  StaffTypeId = 1},
                    new Leave { Active = true, Name = "Annual",  StaffTypeId = 2},
                    new Leave { Active = true, Name = "Sabbatical", StaffTypeId = 1},
                    new Leave { Active = true, Name = "Study", StaffTypeId = 1},
                };
                foreach (Leave leave in leaves)
                {
                    context.Add(leave);
                }
                await context.SaveChangesAsync();


                var trainingTypes = new TrainingType[]
                {
                    new TrainingType { Active = true, Name = "Annual"},
                    new TrainingType { Active = true, Name = "Promotion"},
                };
                foreach (TrainingType training in trainingTypes)
                {
                    context.Add(training);
                }
                await context.SaveChangesAsync();

                var broadcasts = new Broadcast[]
               {
                    new Broadcast { Subject = "Welcome to LiteHR", Date = DateTime.Now, Details= "Welcome to a better way of managing your HR processes."},
                    new Broadcast { Subject = "Update your profiles", Date = DateTime.Now, Details="Update your profiles to start enjoying all new features."},
               };
                foreach (Broadcast broadcast in broadcasts)
                {
                    context.Add(broadcast);
                }


                var events = new Event[]
               {
                    new Event { Name = "General meeting", Date = DateTime.Now, Venue= "5000 Capacity Hall"},
                    new Event { Name = "Management meeting", Date = DateTime.Now.AddDays(5), Venue="VC's Boardroom"},
               };
                foreach (Event eventss in events)
                {
                    context.Add(eventss);
                }
                await context.SaveChangesAsync();

                var eduQualifications = new EducationalQualification[]
              {
                    new EducationalQualification { Active = true, Name = "B.Sc"},
                    new EducationalQualification { Active = true, Name = "M.Sc"},
              };
                foreach (EducationalQualification educationalQualification in eduQualifications)
                {
                    context.Add(educationalQualification);
                }
                await context.SaveChangesAsync();
                var jobTypes = new JobType[]
                {
                    new JobType { Active = true, Name = "Lecturer I"},
                    new JobType { Active = true, Name = "Lecturer II"},
                };
                foreach (JobType jobType in jobTypes)
                {
                    context.Add(jobType);
                }
                await context.SaveChangesAsync();

                var applicationSectionHeaders = new ApplicationSectionHeader[]
                {
                    new ApplicationSectionHeader { Active = true, Name = "Bio-Data"},
                    new ApplicationSectionHeader { Active = true, Name = "Education and Qualification"},
                    new ApplicationSectionHeader { Active = true, Name = "Professional Body"},
                    new ApplicationSectionHeader { Active = true, Name = "Journal and Publication"},
                    new ApplicationSectionHeader { Active = true, Name = "Work Experience"},
                    new ApplicationSectionHeader { Active = true, Name = "Research Grant"},
                    new ApplicationSectionHeader { Active = true, Name = "Professional Qualification"},
                    new ApplicationSectionHeader { Active = true, Name = "Referee"},

                };
                foreach (ApplicationSectionHeader applicationSectionHeader in applicationSectionHeaders)
                {
                    context.Add(applicationSectionHeader);
                }
                await context.SaveChangesAsync();

                var appointmentTypes = new AppointmentType[]
                {
                    new AppointmentType { Active = true, Name = "Temporal"},
                    new AppointmentType { Active = true, Name = "Regular"},
                };
                foreach (AppointmentType appointmentType in appointmentTypes)
                {
                    context.Add(appointmentType);
                }
                await context.SaveChangesAsync();



            }
        }
    }
}
