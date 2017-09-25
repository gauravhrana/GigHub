using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;

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