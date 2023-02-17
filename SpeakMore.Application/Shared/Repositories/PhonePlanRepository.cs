using Microsoft.EntityFrameworkCore;
using SpeakMore.Application.Shared.Context;
using SpeakMore.Application.Shared.Domain.Contracts;
using SpeakMore.Application.Shared.Domain.Entities;

namespace SpeakMore.Application.Shared.Repositories
{
    public class PhonePlanRepository : IPhonePlanRepository
    {
        private readonly ApplicationDbContext _context;
        public PhonePlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PhonePlan> GetPhonePlanByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _context.PhonePlans.Where(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}
