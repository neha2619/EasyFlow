﻿using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Util;

namespace EasyFlow.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
 services.AddCors(options =>
 {
     options.AddPolicy("" +
         "CorsPolicy", builder =>
     builder.AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());
 });
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
 services.Configure<IISOptions>(options =>
 {
 });
        public static void ConfigureLoggerService(this IServiceCollection services) =>
 services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureValidationServices(this IServiceCollection services) => services.AddScoped<IGlobalValidationUtil, GlobalValidationUtil>();
        public static void ConfigureUtilityServices(this IServiceCollection services) => services.AddScoped<IUtil, Utilities>();
        public static void ConfigureOTPDto(this IServiceCollection services) => services.AddScoped<OTPs>();
        public static void ConfigureWorkerReqEntity(this IServiceCollection services) => services.AddScoped<WorkerReq>();
        public static void ConfigureCompanyReqEntity(this IServiceCollection services) => services.AddScoped<CompanyReq>();
        public static void ConfigureAdminCompanyEntity(this IServiceCollection services) => services.AddScoped<AdminCompany>();
        public static void ConfigureDashBoardsDto(this IServiceCollection services) => services.AddScoped<DashBoardDto>();
        
        public static void ConfigureSqlContext(this IServiceCollection services,
IConfiguration configuration) =>
 services.AddDbContext<RepositoryContext>(opts =>
 opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b =>
b.MigrationsAssembly("EasyFlow")));
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
 services.AddScoped<IRepositoryManager, RepositoryManager>();
    }
}
