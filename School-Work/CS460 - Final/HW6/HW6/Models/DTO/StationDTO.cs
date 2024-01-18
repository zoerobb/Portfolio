using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace HW6.Models.DTO
{
    public class StationDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

}

namespace HW6.ExtensionMethods
{
    public static class StationExtensions
    {
        public static HW6.Models.DTO.StationDTO ToStationDTO(this HW6.Models.Station station)
        {
            return new HW6.Models.DTO.StationDTO
            {
                Id = station.Id,
                Name = station.Name
            };
        }

        public static HW6.Models.Station ToStation(this HW6.Models.DTO.StationDTO station)
        {
            return new HW6.Models.Station
            {
                Id = station.Id,
                Name = station.Name
            };
        }
    }
}