using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Business.IRepository.IFinancialAidRepositories;
using DataAcesss.Data;
using DataAcesss.Data.FinancialAidModels;
using DataAcesss.Data.ProductModels;
using DataAcesss.Data.Shared;

namespace Business.Repository.FinancialAidRepositories
{
    public class FinancialAidRepository : IFinancialAidRepository
    {
        private readonly AppDbContext context;

        public FinancialAidRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<FinancialAid> CreateFinancialAid(FinancialAid financialAid)
        {
            if(financialAid != null && financialAid.Establishment != null && financialAid.ProductType_FinancialAids != null)
            {
                var dbFinancialAid = await context.FinancialAids.AddAsync(financialAid);
                await context.SaveChangesAsync();
                return (dbFinancialAid.Entity);
            }
            return null;
        }

        public async Task<FinancialAid> GetFinancialAid(int id)
        {
            if(id > 0)
            {
                FinancialAid financialAid = await context.FinancialAids.Include(x => x.ProductType_FinancialAids).ThenInclude(y => y.ProductType)
                                                                       .Include(x => x.Establishment)
                                                                       .Include(x => x.Orders)
                                                                       .FirstOrDefaultAsync(x => x.FinancialAidId == id);
                if(financialAid != null)
                {
                    return financialAid;
                }
            }
            return null;
        }

        public List<FinancialAid> GetAllFinancialAids()
        {
            List<FinancialAid> financialAids = context.FinancialAids.Include(x => x.ProductType_FinancialAids).ThenInclude(y => y.ProductType)
                                                                       .Include(x => x.Establishment)
                                                                       .Include(x => x.Orders).ToList();
            if (financialAids.Any())
            {
                return financialAids;
            }
            return null;
        }

        public async Task<List<FinancialAid>> GetEstablishmentFinancialAids(int id)
        {
            if(id > 0)
            {
                List<FinancialAid> financialAids = await context.FinancialAids.Include(x => x.ProductType_FinancialAids).ThenInclude(y => y.ProductType)
                                                                       .Include(x => x.Establishment)
                                                                       .Include(x => x.Orders)
                                                                       .Where(x => x.EstablishmentId == id)
                                                                       .ToListAsync();
                if (financialAids.Any())
                {
                    return financialAids;
                }
            }
            return null;
        }

        public async Task<List<FinancialAid>> GetProductTypeFinancialAids(int id)
        {
            if (id > 0)
            {
                List<FinancialAid> financialAids = await context.FinancialAids.Include(x => x.ProductType_FinancialAids).ThenInclude(y => y.ProductType)
                                                                             .Include(x => x.Establishment)
                                                                             .Include(x => x.Orders).Where(x => x.ProductType_FinancialAids
                                                                             .FirstOrDefault(y => y.ProductTypeId == id).ProductTypeId == id).ToListAsync();
                if (financialAids.Any())
                {
                    return financialAids;
                }
            }
            return null;
        }

        public async Task<FinancialAid> UpdateFinancialAid(int id, FinancialAid financialAid)
        {
            if (id > 0 && financialAid != null && financialAid.Establishment != null && financialAid.ProductType_FinancialAids != null)
            {
                var dbFinancialAid = context.FinancialAids.Update(financialAid);
                await context.SaveChangesAsync();
                return (dbFinancialAid.Entity);
            }
            return null;
        }

        public async void DeleteFinancialAid(int id)
        {
            if(id.ToString() != null)
            {
                FinancialAid financialAid = await context.FinancialAids.Include(x => x.ProductType_FinancialAids).FirstOrDefaultAsync(x => x.FinancialAidId == id);
                if(financialAid != null)
                {
                    context.FinancialAids.Remove(financialAid);
                    context.SaveChanges();
                }
            }
        }
    }
}
