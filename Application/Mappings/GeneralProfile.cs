using Application.DTOs.Schedules.Responses;
using Application.Features.Auth.Commands.RegisterUser;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<RegisterUserCommand, User>().ReverseMap();
            CreateMap<IEnumerable<JobTypesDto>, IEnumerable<Job>>().ReverseMap();
            CreateMap<IEnumerable<ShiftTypesDto>, IEnumerable<ShiftType>>().ReverseMap();
        }
    }
}
