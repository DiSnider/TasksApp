using AutoMapper;
using System;
using System.Linq;
using TasksApp.Models;
using TasksApp.ViewModels;

namespace TasksApp
{
    public static class AutoMapperConfig
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<AppUser, UserInfoViewModel>()
                .ForMember(a => a.Login, opt =>
                {
                    opt.MapFrom(src => src.UserName);
                });

                cfg.CreateMap<RegisterViewModel, AppUser>()
                    .ForMember(a => a.Email, opt =>
                    {
                        opt.MapFrom(src => src.Email);
                    })
                    .ForMember(a => a.UserName, opt =>
                    {
                        opt.MapFrom(src => src.Email);
                    })
                    .ForAllOtherMembers(opts => opts.Ignore());

                cfg.CreateMap<Task, TaskDetailsViewModel>()
                    .ForMember(a => a.RelatedTags, opt =>
                    {
                        opt.ResolveUsing((task, taskDetails, i, context) =>
                        {
                            return task.RelatedTags.Select(tag => context.Mapper.Map<Tag, TagViewModel>(tag));
                        });
                    });

                cfg.CreateMap<Task, TaskViewModel>()
                    .ForMember(a => a.RelatedTags, opt =>
                    {
                        opt.MapFrom(src => src.RelatedTags.Select(t => t.Name));
                    });

                cfg.CreateMap<CreateTaskViewModel, Task>()
                    .ForMember(a => a.IsDone, opt =>
                    {
                        opt.MapFrom(src => false);
                    })
                    .ForMember(a => a.CreationDate, opt =>
                    {
                        opt.MapFrom(src => DateTime.UtcNow);
                    })
                    .ForMember(a => a.LastModificationDate, opt =>
                    {
                        opt.MapFrom(src => DateTime.UtcNow);
                    })
                    .ForMember(a => a.Id, opt => opt.Ignore())
                    .ForMember(a => a.User, opt => opt.Ignore())
                    .ForMember(a => a.RelatedTags, opt => opt.Ignore());

                cfg.CreateMap<Tag, TagViewModel>()
                    .ForMember(a => a.Id, opt =>
                    {
                        opt.MapFrom(src => src.Id.ToString());
                    });
            });

            config.AssertConfigurationIsValid();

            IMapper mapper = config.CreateMapper();
            return mapper;
        }
    }
}