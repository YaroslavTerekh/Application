using DeskBooking.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.BL.Behaviours.Coworkings.GetAllCoworkings;

public record GetAllCoworkingsQuery() : IRequest<List<CoworkingDTO>>;
