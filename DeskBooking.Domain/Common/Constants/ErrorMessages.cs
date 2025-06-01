using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.Common.Constants;

public static class ErrorMessages 
{
    public const string TypedRoomsNotFound = "Вільних кімнат цього типу немає";
    public const string FreeDesksNotFound = "Вільних столів на цей період немає";
    public const string RoomNotFound = "Кімнату не знайдено";
    public const string BookingNotFound = "Бронювання не знайдено";
    public const string NoFreeRooms = "Всі кімнати цього типу вже зарезервовані в цей час, або таких кімнат немає";
    public const string WrongDateFormat = "Дата завершення має бути пізніше дати початку";

    public const string Status500InternalServerError = "Відбулася невідома помилка";
    public const string Status401UserNotAuthorized = "Ви не ввійшли в акаунт";
}
