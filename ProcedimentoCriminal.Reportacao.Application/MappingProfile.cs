﻿using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using ProcedimentoCriminal.Core.Application.Mappings;

namespace ProcedimentoCriminal.Reportacao.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) ||
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                                 ?? (type.GetInterface("IMapFrom`1") ?? type.GetInterface("IMapTo`1")).GetMethod(
                                     "Mapping");

                methodInfo?.Invoke(instance, new object[] {this});
            }
        }
    }
}