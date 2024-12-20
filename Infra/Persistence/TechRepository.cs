using Trackit.Core.Entities;
using Trackit.Core.Gateways;
using Trackit.Infra.Data;

namespace Trackit.Infra.Persistence;

public class TechRepository(ApplicationContext context) : ITechGateway
{
    private readonly ApplicationContext _context = context;

    public async Task CreateAsync(Tech tech)
    {
        await _context.Techs.AddAsync(tech);
        await _context.SaveChangesAsync();
        return;
    }

    public async Task<Tech?> FindByIdAsync(string id)
    {
        if(Guid.TryParse(id, out Guid Id))
        {
            var tech = await _context.Techs.FindAsync(Id);
            return tech;
        }
        
        return null;
    }
}
