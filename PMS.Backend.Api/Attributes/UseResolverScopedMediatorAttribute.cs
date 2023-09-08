using System;
using System.Reflection;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace PMS.Backend.Api.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class UseResolverScopedMediatorAttribute : ObjectFieldDescriptorAttribute
{
    protected override void OnConfigure(
        IDescriptorContext descriptorContext,
        IObjectFieldDescriptor descriptor,
        MemberInfo member)
    {
        string scopedServiceName = typeof(IMediator).FullName ?? nameof(IMediator);

        descriptor.Use(next => async context =>
        {
            using IServiceScope scope = context.Service<IServiceProvider>().CreateScope();

            try
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                context.SetLocalState(scopedServiceName, mediator);

                await next(context);
            }
            finally
            {
                context.RemoveLocalState(scopedServiceName);
            }
        });
    }
}
