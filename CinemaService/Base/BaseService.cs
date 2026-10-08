using AutoMapper;
using CinemaDomain.Base;
using FluentValidation;
using Microsoft.IdentityModel.Tokens.Experimental;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaRepository.Base
{
    public class BaseService<TypeEntity> : IBaseService<TypeEntity> where TypeEntity : IBaseEntity
    {
        private readonly IBaseRepository<TypeEntity> _baseRepository;
        private readonly IMapper _mapper;

        public BaseService(IBaseRepository<TypeEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        private void Validate(TypeEntity obj, AbstractValidator<TypeEntity> validator)
        {
            validator.ValidateAndThrow(obj);
        }
        public TypeOutPutModel Create<TypeInputModel, TypeOutPutModel, TypeValidator>(TypeEntity entity) where TypeInputModel : class where TypeOutPutModel : class where TypeValidator : FluentValidation.AbstractValidator<TypeEntity>
        {
            var e = _mapper.Map<TypeEntity>(entity);
            Validate(e, Activator.CreateInstance<TypeValidator>());
            _baseRepository.Create(e);
            var outputModel = _mapper.Map<TypeOutPutModel>(e);
            return outputModel;
        }

        public TypeOutPutModel ReadById<TypeOutPutModel>(int id) where TypeOutPutModel : class
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TypeOutPutModel> ReadAll<TypeOutPutModel>() where TypeOutPutModel : class
        {
            throw new NotImplementedException();
        }

        public TypeOutPutModel Update<TypeInputModel, TypeOutPutModel, TypeValidator>(TypeEntity entity) where TypeInputModel : class where TypeOutPutModel : class where TypeValidator : FluentValidation.AbstractValidator<TypeEntity>
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
