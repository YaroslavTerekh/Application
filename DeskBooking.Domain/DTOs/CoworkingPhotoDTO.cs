using DeskBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.DTOs;

public class CoworkingPhotoDTO
{
    public Guid Id { get; set; }
    public required string ImagePath { get; set; }
}
