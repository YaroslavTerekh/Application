using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.Common.Constants;

public static class ValidationErrors
{
    public const string InvalidEmailFormat = "Введіть коректну email-адресу";
    public const string EmptyEmail = "Введіть email-адресу";

    public const string EmptyName = "Введіть своє ім'я";
    public const string NameTooLong = "Ім'я задовге";
    public const string NameTooShort = "Ім'я закоротке";

    public const string EmptyAnonUserId = "AnonUserId обов'язковий";
    public const string InvalidRoomType = "Невірний тип кімнати";
    public const string InvalidCapacity = "Місткість має бути більшою за 0";
    public const string InvalidDateRange = "EndDate має бути пізніше StartDate";
    public const string DurationLimitExceeded = "Тривалість бронювання не може перевищувати {0} днів для {1}";
    public const string EmptyReservationId = "BookingId не може бути порожнім";
    public const string EmptyRoomId = "RoomId не може бути порожнім";
}