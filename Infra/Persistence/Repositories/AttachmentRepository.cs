using Trackit.Domain.Entities;
using Trackit.Domain.Interfaces;

namespace Trackit.Infra.Persistence.Repositories;

public class AttachmentRepository(AppDbContext context) : IAttachment
{
    private readonly AppDbContext _context = context;
    
    public async Task AddAsync(Attachment entity)
    {
         await _context.Attachments.AddAsync(entity);
         await _context.SaveChangesAsync();
    }

    public async Task<Attachment?> FindByIdAsync(Guid id)
    {
        var attachment = await _context.Attachments.FindAsync(id);
        return attachment;
    }

    public Task<BaseListResponse<Attachment>> ListAsync(int skip, int take)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Attachment entity)
    {
        throw new NotImplementedException();
    }
}