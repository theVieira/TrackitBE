using Microsoft.EntityFrameworkCore;
using Trackit.Domain.Entities;
using Trackit.Infra.Persistence;

namespace Trackit.Seed;

public class CreateDefaultUser(AppDbContext context, IConfiguration config)
{
    private readonly AppDbContext _context = context;
    private readonly IConfiguration _config = config;
    
    public async Task Create()
    {
        var user = _config.GetSection("User").Get<UserConfig>();

        if(user is null) throw new NullReferenceException("Default user configuration is null");
        
        var findUser = await _context.Techs.Where(x => x.Email == user.Email).FirstOrDefaultAsync();

        if (findUser is not null) return;
        
        var tech = Tech.Factory.Create(user.Name, user.Password, user.Phone, user.Email, user.ETechRole);
        
        await _context.Techs.AddAsync(tech);
        await _context.SaveChangesAsync();
    }
}

public class UserConfig
{
    public required string Name { get; init; }
    public required string Password { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public eTechRole ETechRole { get; init; }
}