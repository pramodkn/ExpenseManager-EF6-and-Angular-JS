using AutoMapper;
using EM.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EM.MappingProfile
{
    public class BaseMapper<T, U> : Profile where T : class where U : class, new()
    {
        protected IMappingExpression<U, T> DtoToDomainMapping { get; private set; }
        protected IMappingExpression<T, U> DomainToDtoMapping { get; private set; }

        public BaseMapper()
        {
            //DomainToDtoMapping = CreateMap<T, U>();

            //CreateMap<PagedResult<T>, PagedResult<U>>();
        }

        public PagedResult<U> MapToDtoPagedResult(PagedResult<T> pagedResult)
        {
            if (pagedResult == null)
                return null;
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<T, U>();
                cfg.CreateMap<PagedResult<T>, PagedResult<U>>();
            });
            var dtoResult = new PagedResult<U>();
            IMapper mapper = config.CreateMapper();
            var dest = mapper.Map<PagedResult<U>>(pagedResult);
            //dest.Results=mapper.Map<IList<U>>(pagedResult.Results);
            return dest;
        }

        
    }
}