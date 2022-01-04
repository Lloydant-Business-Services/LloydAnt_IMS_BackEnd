using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LiteHR.Models;
using Microsoft.AspNetCore.Authorization;
using LiteHR.Dtos;
using System.IO;
using OfficeOpenXml;

namespace LiteHR.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionDepartmentsController : ControllerBase
    {
        private readonly HRContext _context;

        public InstitutionDepartmentsController(HRContext context)
        {
            _context = context;
        }

        // GET: api/InstitutionDepartments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentListDto>>> GetINSTITUTION_DEPARTMENT()
        {
         
            return await _context.INSTITUTION_DEPARTMENT
                .OrderBy(d => d.Name.Trim())
                .Include(f => f.Faculty)
                .Select(d => new DepartmentListDto
                {
                    Name = d.Name,
                    FacultyName = d.Faculty.Name,
                    FacultyId = d.FacultyId,
                    Id = d.Id

                }).ToListAsync();
                
        }

        // GET: api/InstitutionDepartments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InstitutionDepartment>> GetInstitutionDepartment(long id)
        {
            var institutionDepartment = await _context.INSTITUTION_DEPARTMENT.FindAsync(id);

            if (institutionDepartment == null)
            {
                return NotFound();
            }

            return institutionDepartment;
        }

        // PUT: api/InstitutionDepartments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> PutInstitutionDepartment(long id, DepartmentListDto departmentListDto)
        {
            var institutionDepartment = await _context.INSTITUTION_DEPARTMENT.Where(i => i.Id == id).FirstOrDefaultAsync();
            institutionDepartment.Name = departmentListDto.Name;
            institutionDepartment.FacultyId = departmentListDto.FacultyId;
            _context.Update(institutionDepartment);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        // POST: api/InstitutionDepartments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<int>> PostInstitutionDepartment(DepartmentListDto departmentListDto)
        {
            InstitutionDepartment institutionDepartment = new InstitutionDepartment();
            institutionDepartment.Name = departmentListDto.Name;
            institutionDepartment.FacultyId = departmentListDto.FacultyId;
            institutionDepartment.Active = true;
            _context.Add(institutionDepartment);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;

        }

        // DELETE: api/InstitutionDepartments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteInstitutionDepartment(long id)
        {
            var institutionDepartment = await _context.INSTITUTION_DEPARTMENT.FindAsync(id);
            if (institutionDepartment == null)
            {
                return NotFound();
            }

            _context.INSTITUTION_DEPARTMENT.Remove(institutionDepartment);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }
        //Get Departments by facylty
        [HttpGet("ByFaculty")]
        public async Task<IEnumerable<DepartmentListDto>> GetDepartmentsByFaculty(long facultyId)
        {
            return await _context.INSTITUTION_DEPARTMENT.Where(f => f.FacultyId == facultyId)
                .Select(f => new DepartmentListDto
                {
                    Name = f.Name,
                    Id = f.Id
                }).ToListAsync();
        }

        private bool InstitutionDepartmentExists(long id)
        {
            return _context.INSTITUTION_DEPARTMENT.Any(e => e.Id == id);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> UploadDepartmentListFromExcelSheet(IFormFile file)
        {
            try
            {
                long size = file.Length;

                if (size > 0)
                {
                    var filePath = Path.GetTempFileName();
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);

                        //Initialize the Data from the excel sheet into stream
                        ExcelPackage package = new ExcelPackage(stream);
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                        if (worksheet != null)
                        {
                            //Give 2 row space from the top to allow for Excel Headers
                            int totalRows = worksheet.Dimension.Rows;

                            for (int i = 2; i <= totalRows; i++)
                            {
                                int serialNumber = Convert.ToInt32(worksheet.Cells[i, 1].Value);
                                string departmentName = worksheet.Cells[i, 2].Value != null ? worksheet.Cells[i, 2].Value.ToString() : null;
                                string facultyName = worksheet.Cells[i, 3].Value != null ? worksheet.Cells[i, 3].Value.ToString() : null;




                                var departmentExists = await _context.INSTITUTION_DEPARTMENT
                                                                      .Where(d => d.Name == departmentName)
                                                                      .FirstOrDefaultAsync();

                                var facultytExists = await _context.FACULTY
                                                                     .Where(d => d.Name == facultyName)
                                                                     .FirstOrDefaultAsync();

                                if (facultytExists == null)
                                {
                                    var newFaculty = new Faculty()
                                    {
                                        Name = facultyName,
                                        Active = true
                                    };

                                    _context.Add(newFaculty);
                                    await _context.SaveChangesAsync();
                                }

                                if (departmentExists == null)
                                {
                                    var newDepartment = new InstitutionDepartment()
                                    {
                                        Name = departmentName,
                                        Active = true
                                    };

                                    _context.Add(newDepartment);
                                    await _context.SaveChangesAsync();
                                }
                            }
                        }
                        else
                        {
                            return BadRequest(new { Message = "Excel Sheet is empty and Invalid" });
                        }
                    }

                    return Ok(new { Message = "Excel Sheet was backed up successfully" });
                }

                return NoContent();
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
