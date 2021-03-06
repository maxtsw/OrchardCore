using Fluid;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Alias.Drivers;
using OrchardCore.Alias.Handlers;
using OrchardCore.Alias.Indexes;
using OrchardCore.Alias.Indexing;
using OrchardCore.Alias.Liquid;
using OrchardCore.Alias.Models;
using OrchardCore.Alias.Services;
using OrchardCore.Alias.Settings;
using OrchardCore.Alias.ViewModels;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data;
using OrchardCore.Data.Migration;
using OrchardCore.Indexing;
using OrchardCore.Liquid;
using OrchardCore.Modules;

namespace OrchardCore.Alias
{
    public class Startup : StartupBase
    {
        static Startup()
        {
            TemplateContext.GlobalMemberAccessStrategy.Register<AliasPartViewModel>();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IScopedIndexProvider, AliasPartIndexProvider>();
            services.AddScoped<IDataMigration, Migrations>();
            services.AddScoped<IContentHandleProvider, AliasPartContentHandleProvider>();

            // Identity Part
            services.AddContentPart<AliasPart>()
                .UseDisplayDriver<AliasPartDisplayDriver>()
                .AddHandler<AliasPartHandler>();

            services.AddScoped<IContentPartIndexHandler, AliasPartIndexHandler>();
            services.AddScoped<IContentTypePartDefinitionDisplayDriver, AliasPartSettingsDisplayDriver>();

            services.AddScoped<ILiquidTemplateEventHandler, ContentAliasLiquidTemplateEventHandler>();
        }
    }
}
