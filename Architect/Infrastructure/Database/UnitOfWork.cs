using Olympus.Architect.Application.Interfaces;

namespace Olympus.Architect.Infrastructure.Database;

public class UnitOfWork(DefaultDatabaseContext context) : IUnitOfWork {

	private readonly DefaultDatabaseContext _context = context;

	public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {

		return _context.SaveChangesAsync(cancellationToken);

	}

}
