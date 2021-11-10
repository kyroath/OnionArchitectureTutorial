﻿using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProductFeatures.Commands;

public class CreateProductCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Barcode { get; set; }
    public string Description { get; set; }
    public decimal Rate { get; set; }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Barcode = request.Barcode,
                Name = request.Name,
                Description = request.Description,
                Rate = request.Rate
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }
    }
}