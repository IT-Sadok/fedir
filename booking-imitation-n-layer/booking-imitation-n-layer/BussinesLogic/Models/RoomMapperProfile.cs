using AutoMapper;
using booking_imitation_n_layer.BussinesLogic.Models.Domain;
using booking_imitation_n_layer.BussinesLogic.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booking_imitation_n_layer.BussinesLogic.Models
{
    public class RoomMapperProfile : Profile
    {
        public RoomMapperProfile()
        {
            CreateMap<Room, RoomDTO>().ReverseMap();
        }
    }
}
