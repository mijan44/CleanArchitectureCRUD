

using Application.Contracts;
using Application.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementation
{
    public class EmployeeRepo : IEmployee
    {
        private readonly AppDbContext appDbContext;

        public EmployeeRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<ServiceResponse> AddAsync(Employee employee)
        {
            //var check = await appDbContext.Employees.FirstOrDefaultAsync(x => x.Name.ToLower() == employee.Name.ToLower());
            //if (check != null)
            //    return new ServiceResponse(false, "Employee already exist");
            

            appDbContext.Employees.Add(employee);
            await SaveChangesAsync();
            return new ServiceResponse(true, "Added");
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var employee = await appDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return new ServiceResponse(false, "User not found");
            }
            appDbContext.Employees.Remove(employee);
            await SaveChangesAsync();
            return new ServiceResponse(true, "Deleted");
        }

        public async Task<List<Employee>> GetAsync() => await appDbContext.Employees.AsNoTracking().ToListAsync();




        public async Task<Employee> GetByIdAsync(int id) => await appDbContext.Employees.FindAsync(id );
        
        

        public async Task<ServiceResponse> UpdateAsync(Employee employee)
        {
            appDbContext.Update(employee);
            await SaveChangesAsync();
            return new ServiceResponse(true, "Updated");
        }

        private async Task SaveChangesAsync() => await appDbContext.SaveChangesAsync();
    }
}
