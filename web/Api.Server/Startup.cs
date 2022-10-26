using Baisse.Model.Models.ApiModel;
using Baisse.Model.Models.AppsettingModel;
using HDF.Blog.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Text;

namespace HDF.Blog.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// ���÷���
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //ӳ�����õ�ʵ��
            var settings = _configuration.GetSection("AppSettings").Get<AppSettings>();

            //AddTransient˲ʱģʽ��ÿ�����󣬶���ȡһ���µ�ʵ������ʹͬһ�������ȡ���Ҳ���ǲ�ͬ��ʵ��
            //AddScoped��ÿ�����󣬶���ȡһ���µ�ʵ����ͬһ�������ȡ��λ�õ���ͬ��ʵ��
            //AddSingleton����ģʽ��ÿ�ζ���ȡͬһ��ʵ��
            services.AddSingleton(settings)
                    .AddSingleton(settings.TokenConfig)
                    .AddSingleton(settings.CorsConfig)
                    .AddSingleton(settings.SpaConfig)
                    .AddSingleton(settings.DBConfig);

            services.AddHttpContextAccessor();

            //ӳ��Token��Ϣ��ʵ��
            services.AddScoped(typeof(AccessToken));
            //����
            //services.AddDbContext<ConcardContext>(options => options.UseSqlServer(settings.DBConfig.SqlServerConnection));

            // ����EF����ע��
            //services.AddDbContext<common.Core.ConcardContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SchoolConnection")));
            //services.AddScoped<IconcardContext, ConcardContext>();
            //services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            //services.AddScoped<IUserService, UserService>();

            ////��Ӷ�AutoMapper��֧�֣���������г����м̳��� Profile ����
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //��ӿ�������������Json���л�����
            services.AddControllers().AddNewtonsoftJsonService();

            //���DBContext����
            //services.AddDbContextService(settings.DBConfig);

            //if (settings.DbType == DBType.SqlServer)
            //    services.AddDbContextService(options => options.UseSqlServer(settings.ConnectionStrings["GTCMCDS"]));
            //if (settings.DbType == DBType.MySql)
            //    services.AddDbContextService(options => options.UseMySql(settings.ConnectionStrings["MySqlDB"]));
            //services.AddEmrContextService(settings.ConnectionStrings["EMRDB"]);

            //���api�汾���Ʒ���
            services.AddApiVersioningService();
            //���Swagger����
            services.AddSwaggerService();
            //��������֤����
            services.AddAuthenticationService(settings.TokenConfig);
            //��ӿ������
            //services.AddCorsService(settings.CorsConfig);
            //���SPA����
            //services.AddSpaService(settings.SpaConfig);

            //StringBuilder sb = new StringBuilder();
            //foreach (var item in services)
            //{
            //    sb.AppendLine(item.ServiceType.Name);
            //} 
            //services.AddHttpsRedirection(options =>
            //{
            //    options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
            //    options.HttpsPort = 443;
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, AppSettings settings)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHsts();
            //app.UseHttpsRedirection(); // https�ض���

            app.UseSwaggerMiddleware(provider);// ����swagger�м��

            app.UseAuthentication();//�����֤

            app.UseRouting();

            //app.UserCorsMiddleware(settings.CorsConfig);//���ÿ������

            app.UseAuthorization();//��Ȩ

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //SignalR  hub ...
            });

            //app.UseSpaMiddleware(env, settings.SpaConfig);//����SPA�м��
        }



        ///// <summary>
        ///// Autofac��������
        ///// </summary>
        ///// <param name="builder"></param>
        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    // ****************************************************************
        //    // Gocent.GTCMCDS.WebApi��Ŀ�Ѿ���Gocent.GTCMCDS.Services�����������
        //    // ������Gocent.GTCMCDS.IServices�����
        //    // �Ѿ����������ԭ����վ����ֻ�ᷢ�����õ���Ŀ�ļ������Services��Ŀ������Ϊ�˷�������
        //    // ****************************************************************

        //    //��Ŀ�������ʵ��dll(Server��Repository)�踴�Ƶ�����Ŀ¼��
        //    var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
        //    var servicesDllFile = Path.Combine(basePath, "Gocent.GTCMCDS.Services.dll");//��ȡע����Ŀ����·��

        //    Assembly service = Assembly.LoadFile(servicesDllFile);//ֱ�Ӳ��ü����ļ��ķ���
        //    builder.RegisterAssemblyTypes(service).Where(t => t.Name.EndsWith("Services"))
        //        .AsImplementedInterfaces()
        //        .InstancePerDependency();
        //    //.EnableInterfaceInterceptors()//����Autofac.Extras.DynamicProxy;
        //    //.InterceptedBy(cacheType.ToArray());//����������������б�����ע�ᡣ

        //}












    }
}
