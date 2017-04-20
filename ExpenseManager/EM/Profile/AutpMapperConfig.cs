using AutoMapper;
using EM.DomainModel;
using EM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EM.MappingProfile
{
   public class OverallMapping : Profile
    {
       public OverallMapping()
        {
            CreateMap<Expense, ExpenseVM>();
            CreateMap<ExpenseVM, Expense>();
            CreateMap<ExpenseCategory, ExpenseCategoryVM>();
            CreateMap<ExpenseCategoryVM, ExpenseCategory>();

        }
    }
   public static class MappingConfiguration
   {
       public static void CreateMapping()
       {
            //var profiles = (from type in typeof(MappingConfiguration).Assembly.GetTypes()
            //                where typeof(Profile).IsAssignableFrom(type) && !type.IsAbstract && type.GetConstructor(Type.EmptyTypes) != null
            //                select type).Select(d => (Profile)Activator.CreateInstance(d))
            //               .ToArray();

            var config = new MapperConfiguration(cfg =>
            {
                //foreach (var profile in profiles)
                //{
                    cfg.AddProfile(new OverallMapping());
                //}
            });
            Mapper.MapperConfiguration = config.CreateMapper();
        }
   }

   public static class Mapper
   {
       public static IMapper MapperConfiguration;
   }
}