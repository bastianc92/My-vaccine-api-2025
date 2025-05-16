using System.Transactions;
using Microsoft.AspNetCore.Identity;
using MyVaccine.WebApi.Dtos;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;

namespace MyVaccine.WebApi.Repositories.Implementations;

public class UserRepository : BaseRepository<User>,IUserRepository
{
    private readonly MyVaccineAppDbContext _context;
    private readonly UserManager<User> _userManager;
    public UserRepository(MyVaccineAppDbContext context, UserManager<User> userManager):base(context)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IdentityResult> AddUser(RegisterRequetDto request)
    {
        var response = new IdentityResult();
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var user = new User
            {
                UserName = request.Username.ToLower(),
                Email = request.Username,

                FirstName = request.FirstName,
                LastName = request.LastName,
                Birthdate = request.Birthdate,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            response = result;

            if (!result.Succeeded)
            {
                return response;
            }
            scope.Complete();
        }



        //var user = new ApplicationUser
        //{
        //    UserName = request.Email.ToLower(),
        //    Email = request.Email,

        //};

        //var result = await _userManager.CreateAsync(user, model.Password);
        return response;
    }
}
