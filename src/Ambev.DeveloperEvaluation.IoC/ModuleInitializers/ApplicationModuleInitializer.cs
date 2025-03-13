namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProducts;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Branchs.CreateBranch;
using Ambev.DeveloperEvaluation.Application.Branchs.GetBranch;
using Ambev.DeveloperEvaluation.Application.Branchs.UpdateBranch;
using Ambev.DeveloperEvaluation.Application.Branchs.DeleteBranch;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ambev.DeveloperEvaluation.Application.Branchs.GetBranchs;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;
using Ambev.DeveloperEvaluation.Application.Events.Sales;

public class ApplicationModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        // Serviços Auxiliares
        builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
        builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        // Handlers do CQRS (MediatR)
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<CreateUserHandler>();
            cfg.RegisterServicesFromAssemblyContaining<GetUserHandler>();
            cfg.RegisterServicesFromAssemblyContaining<DeleteUserHandler>();

            cfg.RegisterServicesFromAssemblyContaining<CreateSaleHandler>();
            cfg.RegisterServicesFromAssemblyContaining<GetSaleHandler>();
            cfg.RegisterServicesFromAssemblyContaining<CancelSaleHandler>();
            cfg.RegisterServicesFromAssemblyContaining<GetSalesHandler>();

            cfg.RegisterServicesFromAssemblyContaining<CreateProductHandler>();
            cfg.RegisterServicesFromAssemblyContaining<GetProductHandler>();
            cfg.RegisterServicesFromAssemblyContaining<GetProductsHandler>();
            cfg.RegisterServicesFromAssemblyContaining<DeleteProductHandler>();
            cfg.RegisterServicesFromAssemblyContaining<UpdateProductHandler>();

            cfg.RegisterServicesFromAssemblyContaining<CreateBranchHandler>();
            cfg.RegisterServicesFromAssemblyContaining<DeleteBranchHandler>();
            cfg.RegisterServicesFromAssemblyContaining<GetBranchHandler>();
            cfg.RegisterServicesFromAssemblyContaining<GetBranchsHandler>();
            cfg.RegisterServicesFromAssemblyContaining<UpdateBranchHandler>();

            cfg.RegisterServicesFromAssemblyContaining<SaleCreatedEventHandler>();
            cfg.RegisterServicesFromAssemblyContaining<SaleModifiedEventHandler>();
            cfg.RegisterServicesFromAssemblyContaining<SaleCancelledEventHandler>();
            cfg.RegisterServicesFromAssemblyContaining<ItemCancelledEventHandler>();
        });
        // User Validators
        builder.Services.AddScoped<IValidator<DeleteUserCommand>, DeleteUserValidator>();

        // Branch Validators
        builder.Services.AddScoped<IValidator<CreateBranchCommand>, CreateBranchValidator>();
        builder.Services.AddScoped<IValidator<UpdateBranchCommand>, UpdateBranchValidator>();
        builder.Services.AddScoped<IValidator<DeleteBranchCommand>, DeleteBranchValidator>(); 

        // Product Validators
        builder.Services.AddScoped<IValidator<CreateProductCommand>, CreateProductValidator>();
        builder.Services.AddScoped<IValidator<UpdateProductCommand>, UpdateProductValidator>();
        builder.Services.AddScoped<IValidator<DeleteProductCommand>, DeleteProductValidator>();

        // Sales Validators
        builder.Services.AddScoped<IValidator<CreateSaleCommand>, CreateSaleValidator>();        
        builder.Services.AddScoped<IValidator<GetSalesQuery>, GetSalesValidator>();
    }
}
