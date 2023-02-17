using Microsoft.EntityFrameworkCore;
using SpeakMore.Application.Shared.Context;
using SpeakMore.Application.Shared.Domain.Contracts;
using SpeakMore.Application.Shared.Domain.Entities;

namespace SpeakMore.Application.Shared.Repositories
{
    public class PhoneCallRateRepository : IPhoneCallRateRepository
    {
        private readonly ApplicationDbContext _context;
        public PhoneCallRateRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PhoneCallRate> GetPhoneCallRateByOriginAndDestinationAsync(int origin, int destination, CancellationToken cancellationToken)
        {
            return await  _context.PhoneCallRates.Where(e => e.Origin == origin && e.Destination == destination).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}
