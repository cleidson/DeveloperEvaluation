using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.Branchs.CreateBranch;
using Ambev.DeveloperEvaluation.Application.Branchs.GetBranch;
using Ambev.DeveloperEvaluation.Application.Branchs.UpdateBranch;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ambev.DeveloperEvaluation.Application.Products.GetProducts;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class ApplicationModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        //Serviços Auxiliares
        builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
        builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>(); // Adicione essa linha

        //Handlers do CQRS (MediatR)
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<CreateUserHandler>();
            cfg.RegisterServicesFromAssemblyContaining<GetUserHandler>();
            cfg.RegisterServicesFromAssemblyContaining<DeleteUserHandler>();
            cfg.RegisterServicesFromAssemblyContaining<CreateSaleHandler>();
            cfg.RegisterServicesFromAssemblyContaining<GetSaleHandler>();
            cfg.RegisterServicesFromAssemblyContaining<CancelSaleHandler>();
            cfg.RegisterServicesFromAssemblyContaining<CreateProductHandler>();
            cfg.RegisterServicesFromAssemblyContaining<GetProductHandler>();
            cfg.RegisterServicesFromAssemblyContaining<GetProductsHandler>();
            cfg.RegisterServicesFromAssemblyContaining<UpdateProductHandler>();
            cfg.RegisterServicesFromAssemblyContaining<CreateBranchHandler>();
            cfg.RegisterServicesFromAssemblyContaining<GetBranchHandler>();
            cfg.RegisterServicesFromAssemblyContaining<UpdateBranchHandler>();
        });   
    }
}
