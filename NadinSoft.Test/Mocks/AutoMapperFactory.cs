using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NadinSoft.Infrastructure.Utilites;

namespace NadinSoft.Test.Mocks
{
    public static class AutoMapperFactory
    {
        public static IMapper CreateMapper()
        {
            return new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            }).CreateMapper();
        }
    }
}