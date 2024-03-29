﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Reports.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddMediatR(typeof(IAssemblyMarker));

        return collection;
    }
}