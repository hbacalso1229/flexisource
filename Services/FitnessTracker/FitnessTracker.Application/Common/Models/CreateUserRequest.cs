﻿using AutoMapper;
using FitnessTracker.Application.Common.Interfaces;
using UserEntity = FitnessTracker.Domain.Aggregates.UserAggregates.Entities.User;


namespace FitnessTracker.Application.Common.Models
{
    public class CreateUserRequest : UserRequestDto, IMapFrom<UserEntity>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserRequest, UserEntity>();
            profile.CreateMap<string, string>().ConvertUsing(x => x ?? String.Empty);
        }
    }
}
