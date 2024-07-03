﻿using AutoMapper;

namespace FitnessTracker.Application.Common.Interfaces
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile)
            => profile.CreateMap(
                sourceType: typeof(T),
                destinationType: GetType());
    }
}