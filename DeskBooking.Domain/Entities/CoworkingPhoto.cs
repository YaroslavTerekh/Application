﻿using DeskBooking.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.Entities;

public class CoworkingPhoto : BaseEntity
{
    public required string ImageName { get; set; }
    public required string ImagePath { get; set; }
    public required string Description { get; set; }

    public Guid CoworkingId { get; set; }
    public Coworking? Coworking { get; set; }
}
