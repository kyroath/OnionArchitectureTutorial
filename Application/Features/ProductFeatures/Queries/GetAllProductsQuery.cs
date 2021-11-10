﻿using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProductFeatures.Queries;

public class GetAllProductsQuery : IRequest<IEnumerable<Product>?>
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>?>
    {
        private readonly IApplicationDbContext _context;

        public GetAllProductsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>?> Handle(GetAllProductsQuery request,
            CancellationToken cancellationToken)
        {
            var productList = await _context.Products.ToListAsync(cancellationToken);
            return productList?.AsReadOnly();
        }
    }
}