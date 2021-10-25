using AutoMapper;
using EmployeeWebAPI.Domain.ValueObjects.Ids;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeWebAPI.Application.CQRS.Mapper
{
    public class MappingIds : Profile
    {
        public MappingIds()
        {
            CreateMap<Guid, EmployeeId>().ConstructUsing(c => new EmployeeId(c));
            CreateMap<EmployeeId, Guid>().ConstructUsing(c => c.Value);
        }
    }

}
