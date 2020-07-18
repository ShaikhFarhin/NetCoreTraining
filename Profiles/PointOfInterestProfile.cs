using AutoMapper;
using NetCoreTraining.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTraining.Profiles
{
    public class PointOfInterestProfile:Profile
    {
        public PointOfInterestProfile() 
        {
            CreateMap<PointOfInterest, Models.PointOfInterestDto>();
            CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
            CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>()
                .ReverseMap();

        }
    }
}


