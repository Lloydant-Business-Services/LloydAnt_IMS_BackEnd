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
    public class PfaService : IPfaService
    {
        private readonly HRContext _context;

        public PfaService(HRContext context)
        {
            _context = context;

        }

        public async Task<long> AddPFA(CommonDto postDto)
        {
            string[] separator = new string[] { ";" };
            string text = postDto.Name;

            foreach (string item in text.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                PfaName pfaName = new PfaName();
                pfaName.Name = item;
                pfaName.Active = true;
                _context.Add(pfaName);

            }
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;

        }

        public async Task<long> AddPFAStatus(CommonDto postDto)
        {
            string[] separator = new string[] { ";" };
            string text = postDto.Name;

            foreach (string item in text.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                PfaStatus pfaStatus = new PfaStatus();
                pfaStatus.Name = item;
                pfaStatus.Active = true;
                _context.Add(pfaStatus);

            }
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;

        }


        public async Task<long> AddAreaofSpecialization(CommonDto postDto)
        {
            string[] separator = new string[] { ";" };
            string text = postDto.Name;

            foreach (string item in text.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                AreaOfSpecialization aos = new AreaOfSpecialization();
                aos.Name = item;
                aos.Active = true;
                _context.Add(aos);

            }
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;

        }

        public async Task<long> EditPFAName(long id, CommonDto editDto)
        {
            var editData = await _context.PFA_NAME.Where(p => p.Id == id).FirstOrDefaultAsync();
            editData.Name = editDto.Name;
            _context.Update(editData);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        public async Task<long> EditPFAStatus(long id, CommonDto editDto)
        {
            var editData = await _context.PFA_STATUS.Where(p => p.Id == id).FirstOrDefaultAsync();
            editData.Name = editDto.Name;
            _context.Update(editData);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        public async Task<long> EditAreaOfSpecialization(long id, CommonDto editDto)
        {
            var editData = await _context.AREA_OF_SPECIALIZATION.Where(p => p.Id == id).FirstOrDefaultAsync();
            editData.Name = editDto.Name;
            _context.Update(editData);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        public async Task<long> DeletePFAName(long id)
        {
            var deleteData = await _context.PFA_NAME.Where(p => p.Id == id).FirstOrDefaultAsync();
            deleteData.Active = false;
            _context.Update(deleteData);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        public async Task<long> DeletePFAStatus(long id)
        {
            var deleteData = await _context.PFA_STATUS.Where(p => p.Id == id).FirstOrDefaultAsync();
            deleteData.Active = false;
            _context.Update(deleteData);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        public async Task<long> DeleteAreaOfSpecialization(long id)
        {
            var deleteData = await _context.AREA_OF_SPECIALIZATION.Where(p => p.Id == id).FirstOrDefaultAsync();
            deleteData.Active = false;
            _context.Update(deleteData);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }
    }
}
