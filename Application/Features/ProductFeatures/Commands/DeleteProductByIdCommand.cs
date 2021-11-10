using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProductFeatures.Commands;

public class DeleteProductByIdCommand : IRequest<int>
{
    public int Id { get; set; }

    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProductByIdCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<int> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var product = await _context
                .Products
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (product is null) return default;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }
    }
}