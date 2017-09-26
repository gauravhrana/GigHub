using AutoMapper;
using GigHub.Core.Dtos;
using GigHub.Core.Models;

namespace GigHub.App_Start
{
    public class AutoMapperBootStrapper
    {
        public static void BootStrap()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<ApplicationUser, UserDto>();
                cfg.CreateMap<Gig, GigDto>();
                cfg.CreateMap<Notification, NotificationDto>();
            });
        }
    }
}