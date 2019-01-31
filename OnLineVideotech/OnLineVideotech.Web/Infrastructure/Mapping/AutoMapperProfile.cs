﻿using AutoMapper;
using OnLineVideotech.Common.Mapping;
using OnLineVideotech.Data.Models;
using OnLineVideotech.Services.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnLineVideotech.Web.Infrastructure.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, AdminUserListingServiceModel>();

            //IEnumerable<Type> allTypes = AppDomain
            //    .CurrentDomain
            //    .GetAssemblies()
            //    .Where(a => a.GetName().Name.Contains("OnLineVideotech"))
            //    .SelectMany(a => a.GetTypes());

            //allTypes
            //    .Where(t => t.IsClass && !t.IsAbstract && t
            //        .GetInterfaces()
            //        .Where(i => i.IsGenericType)
            //        .Select(i => i.GetGenericTypeDefinition())
            //        .Contains(typeof(IMapFrom<>)))
            //     .Select(t => new
            //     {
            //         Destination = t,
            //         Source = t
            //            .GetInterfaces()
            //            .Where(i => i.IsGenericType)
            //            .Select(i => new
            //            {
            //                Definition = i.GetGenericTypeDefinition(),
            //                Arguments = i.GetGenericArguments()
            //            })
            //            .Where(i => i.Definition == typeof(IMapFrom<>))
            //            .SelectMany(i => i.Arguments)
            //            .First()
            //     })
            //     .ToList()
            //     .ForEach(mapping => this.CreateMap(mapping.Source, mapping.Destination));

            //allTypes
            //    .Where(t => t.IsClass
            //        && !t.IsAbstract
            //        && typeof(IHaveCustomMapping).IsAssignableFrom(t))
            //    .Select(Activator.CreateInstance)
            //    .Cast<IHaveCustomMapping>()
            //    .ToList()
            //    .ForEach(mapping => mapping.ConfigureMapping(this));
        }
    }
}