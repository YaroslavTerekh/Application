using DeskBooking.Domain.Entities;
using DeskBooking.Domain.Enum.Rooms;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskBooking.Domain.DatabaseConnection.DataSeeding;

public static class DataContextSeeding
{
    public static async Task SeedBasicDataAsync(DataContext context)
    {
        if (!await context.Rooms.AnyAsync())
        {
            List<Amenity> amenities = [];
            List<Room> rooms = [];
            List<OpenspaceDesk> desks = [];

            if (!await context.Amenities.AnyAsync())
            {
                foreach (AmenityType type in AmenityType.GetValues(typeof(AmenityType)))
                {
                    amenities.Add(new Amenity
                    {
                        AmenityName = type.ToString(),
                        AmenityType = type
                    });
                }

                await context.Amenities.AddRangeAsync(amenities);
            }

            // Додаємо коворкінг
            var coworking = new Coworking
            {
                Name = "TechHub Lviv",
                Description = "A modern coworking space for professionals, startups, and freelancers.",
                Address = "Lviv, Sichovykh Striltsiv St, 15",
            };
            await context.Coworking.AddAsync(coworking);

            // Додаємо фото до коворкінгу
            var coworkingPhotos = new List<CoworkingPhoto>
        {
            new CoworkingPhoto { Coworking = coworking, ImageName = "coworking1.png", ImagePath = "coworking1.png", Description = "Main area" },
        };
            await context.CoworkingPhoto.AddRangeAsync(coworkingPhotos);

            #region Open space 
            var openspace = new Room
            {
                RoomType = RoomType.Openspace,
                RoomName = "Open space",
                Description = "A vibrant shared area perfect for freelancers or small teams who enjoy a collaborative atmosphere. Choose any available desk and get to work with flexibility and ease.",
                Coworking = coworking
            };

            await context.Rooms.AddAsync(openspace);

            for (int i = 1; i < 24; i++)
            {
                var desk = new OpenspaceDesk
                {
                    DeskNumber = i,
                    Room = openspace
                };

                await context.OpenspaceDesks.AddAsync(desk);
            }

            var openspaceAmenities = new List<RoomAmenity>
        {
            new RoomAmenity { AmenityId = amenities.First(a => a.AmenityType == AmenityType.Conditioner).Id, Room = openspace },
            new RoomAmenity { AmenityId = amenities.First(a => a.AmenityType == AmenityType.GameConsole).Id, Room = openspace },
            new RoomAmenity { AmenityId = amenities.First(a => a.AmenityType == AmenityType.Wifi).Id, Room = openspace },
            new RoomAmenity { AmenityId = amenities.First(a => a.AmenityType == AmenityType.Coffee).Id, Room = openspace },
        };

            await context.RoomAmenity.AddRangeAsync(openspaceAmenities);

            var openSpacePhotos = new List<RoomPhoto>
        {
            new RoomPhoto { Room = openspace, ImageName = "openspace1.png", ImagePath = "openspace1.png", Description = "Open space view 1" },
            new RoomPhoto { Room = openspace, ImageName = "openspace2.png", ImagePath = "openspace2.png", Description = "Open space view 2" },
            new RoomPhoto { Room = openspace, ImageName = "openspace3.png", ImagePath = "openspace3.png", Description = "Open space view 3" },
        };
            await context.RoomPhoto.AddRangeAsync(openSpacePhotos);
            #endregion

            #region Private rooms
            foreach ((int count, int capacity) in new[] { (6, 1), (3, 2), (2, 5), (1, 10) })
            {
                for (int i = 0; i < count; i++)
                {
                    var room = new Room
                    {
                        RoomType = RoomType.PrivateRoom,
                        RoomName = "Private rooms",
                        Description = "Ideal for focused work, video calls, or small team huddles. These fully enclosed rooms offer privacy and come in a variety of sizes to fit your needs.",
                        Capacity = capacity,
                        Coworking = coworking
                    };

                    await context.Rooms.AddAsync(room);

                    var privateRoomAmenities = new List<RoomAmenity>
                {
                    new RoomAmenity { AmenityId = amenities.First(a => a.AmenityType == AmenityType.Conditioner).Id, Room = room },
                    new RoomAmenity { AmenityId = amenities.First(a => a.AmenityType == AmenityType.Wifi).Id, Room = room },
                    new RoomAmenity { AmenityId = amenities.First(a => a.AmenityType == AmenityType.Headphone).Id, Room = room },
                };

                    await context.RoomAmenity.AddRangeAsync(privateRoomAmenities);

                    if (capacity == 1 && i == 0)
                    {
                        var privateImages = new List<RoomPhoto>
                    {
                        new RoomPhoto { Room = room, ImageName = "private1.png", ImagePath = "private1.png", Description = "Private room 1" },
                        new RoomPhoto { Room = room, ImageName = "private2.png", ImagePath = "private2.png", Description = "Private room 2" },
                        new RoomPhoto { Room = room, ImageName = "private3.png", ImagePath = "private3.png", Description = "Private room 3" },
                    };
                        await context.RoomPhoto.AddRangeAsync(privateImages);
                    }
                }
            }
            #endregion

            #region Meeting rooms
            foreach ((int count, int capacity) in new[] { (4, 10), (1, 20) })
            {
                for (int i = 0; i < count; i++)
                {
                    var meeting = new Room
                    {
                        RoomType = RoomType.MeetingRoom,
                        RoomName = "Meeting room",
                        Description = "Designed for productive meetings, workshops, or client presentations. Equipped with screens, whiteboards, and comfortable seating to keep your sessions running smoothly.",
                        Capacity = capacity,
                        Coworking = coworking
                    };

                    await context.Rooms.AddAsync(meeting);

                    var meetingRoomAmenities = new List<RoomAmenity>
                {
                    new RoomAmenity { AmenityId = amenities.First(a => a.AmenityType == AmenityType.Conditioner).Id, Room = meeting },
                    new RoomAmenity { AmenityId = amenities.First(a => a.AmenityType == AmenityType.Wifi).Id, Room = meeting },
                    new RoomAmenity { AmenityId = amenities.First(a => a.AmenityType == AmenityType.Headphone).Id, Room = meeting },
                    new RoomAmenity { AmenityId = amenities.First(a => a.AmenityType == AmenityType.Microphone).Id, Room = meeting },
                };

                    await context.RoomAmenity.AddRangeAsync(meetingRoomAmenities);

                    if (capacity == 20)
                    {
                        var meetingImages = new List<RoomPhoto>
                    {
                        new RoomPhoto { Room = meeting, ImageName = "meeting1.png", ImagePath = "meeting1.png", Description = "Meeting room 1" },
                        new RoomPhoto { Room = meeting, ImageName = "meeting2.png", ImagePath = "meeting2.png", Description = "Meeting room 2" },
                        new RoomPhoto { Room = meeting, ImageName = "meeting3.png", ImagePath = "meeting3.png", Description = "Meeting room 3" },
                    };
                        await context.RoomPhoto.AddRangeAsync(meetingImages);
                    }
                }
            }
            #endregion
        }

        await context.SaveChangesAsync();
    }

}
