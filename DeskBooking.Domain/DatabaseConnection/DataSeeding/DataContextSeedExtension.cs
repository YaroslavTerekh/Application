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

public static class DataContextSeedExtension
{
    public static async Task SeedBasicDataAsync(DataContext context) 
    {
        if(!await context.Rooms.AnyAsync())
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
                await context.SaveChangesAsync();
            }

            #region Open space 
            var openspace = new Room
            {
                RoomType = RoomType.Openspace,
                RoomName = "Open space",
                Description = "A vibrant shared area perfect for freelancers or small teams who enjoy a collaborative atmosphere. Choose any available desk and get to work with flexibility and ease."
            };

            await context.Rooms.AddAsync(openspace);
            await context.SaveChangesAsync();

            for (int i = 1; i < 24; i++)
            {
                var desk = new OpenspaceDesk
                {
                    DeskNumber = i,
                    RoomId = openspace.Id
                };

                await context.OpenspaceDesks.AddAsync(desk);
                await context.SaveChangesAsync();
            }

            var openspaceAmenities = new List<RoomAmenity>();

            openspaceAmenities.Add(new RoomAmenity
            {
                AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Conditioner).FirstOrDefault()!.Id,
                RoomId = openspace.Id
            });

            openspaceAmenities.Add(new RoomAmenity
            {
                AmenityId = amenities.Where(a => a.AmenityType == AmenityType.GameConsole).FirstOrDefault()!.Id,
                RoomId = openspace.Id
            });

            openspaceAmenities.Add(new RoomAmenity
            {
                AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Wifi).FirstOrDefault()!.Id,
                RoomId = openspace.Id
            });

            openspaceAmenities.Add(new RoomAmenity
            {
                AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Coffee).FirstOrDefault()!.Id,
                RoomId = openspace.Id
            });

            await context.RoomAmenity.AddRangeAsync(openspaceAmenities);
            await context.SaveChangesAsync();
            #endregion

            #region Private rooms
            for (int i = 0; i < 6; i++)
            {
                var room = new Room
                {
                    RoomType = RoomType.PrivateRoom,
                    RoomName = "Private rooms",
                    Description = "Ideal for focused work, video calls, or small team huddles. These fully enclosed rooms offer privacy and come in a variety of sizes to fit your needs.",
                    Capacity = 1,
                };

                await context.Rooms.AddAsync(room);
                await context.SaveChangesAsync();

                var privateRoomAmenities = new List<RoomAmenity>();

                privateRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Conditioner).FirstOrDefault()!.Id,
                    RoomId = room.Id
                });

                privateRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Wifi).FirstOrDefault()!.Id,
                    RoomId = room.Id
                });

                privateRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Headphone).FirstOrDefault()!.Id,
                    RoomId = room.Id
                });

                await context.RoomAmenity.AddRangeAsync(privateRoomAmenities);
                await context.SaveChangesAsync();

            }

            for (int i = 0; i < 3; i++)
            {
                var room = new Room
                {
                    RoomType = RoomType.PrivateRoom,
                    RoomName = "Private rooms",
                    Description = "Ideal for focused work, video calls, or small team huddles. These fully enclosed rooms offer privacy and come in a variety of sizes to fit your needs.",
                    Capacity = 2,
                };

                await context.Rooms.AddAsync(room);
                await context.SaveChangesAsync();

                var privateRoomAmenities = new List<RoomAmenity>();

                privateRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Conditioner).FirstOrDefault()!.Id,
                    RoomId = room.Id
                });

                privateRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Wifi).FirstOrDefault()!.Id,
                    RoomId = room.Id
                });

                privateRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Headphone).FirstOrDefault()!.Id,
                    RoomId = room.Id
                });

                await context.RoomAmenity.AddRangeAsync(privateRoomAmenities);
                await context.SaveChangesAsync();

            }

            for (int i = 0; i < 2; i++)
            {
                var room = new Room
                {
                    RoomType = RoomType.PrivateRoom,
                    RoomName = "Private rooms",
                    Description = "Ideal for focused work, video calls, or small team huddles. These fully enclosed rooms offer privacy and come in a variety of sizes to fit your needs.",
                    Capacity = 5,
                };

                await context.Rooms.AddAsync(room);
                await context.SaveChangesAsync();

                var privateRoomAmenities = new List<RoomAmenity>();

                privateRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Conditioner).FirstOrDefault()!.Id,
                    RoomId = room.Id
                });

                privateRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Wifi).FirstOrDefault()!.Id,
                    RoomId = room.Id
                });

                privateRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Headphone).FirstOrDefault()!.Id,
                    RoomId = room.Id
                });

                await context.RoomAmenity.AddRangeAsync(privateRoomAmenities);
                await context.SaveChangesAsync();

            }

            for (int i = 0; i == 0; i++)
            {
                var room = new Room
                {
                    RoomType = RoomType.PrivateRoom,
                    RoomName = "Private rooms",
                    Description = "Ideal for focused work, video calls, or small team huddles. These fully enclosed rooms offer privacy and come in a variety of sizes to fit your needs.",
                    Capacity = 10,
                };

                await context.Rooms.AddAsync(room);
                await context.SaveChangesAsync();

                var privateRoomAmenities = new List<RoomAmenity>();

                privateRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Conditioner).FirstOrDefault()!.Id,
                    RoomId = room.Id
                });

                privateRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Wifi).FirstOrDefault()!.Id,
                    RoomId = room.Id
                });

                privateRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Headphone).FirstOrDefault()!.Id,
                    RoomId = room.Id
                });

                await context.RoomAmenity.AddRangeAsync(privateRoomAmenities);
                await context.SaveChangesAsync();

            }
            #endregion

            #region Meeting rooms
            for (int i = 0; i < 3; i++)
            {
                var meeting = new Room
                {
                    RoomType = RoomType.MeetingRoom,
                    RoomName = "Meeting room",
                    Description = "Designed for productive meetings, workshops, or client presentations. Equipped with screens, whiteboards, and comfortable seating to keep your sessions running smoothly.",
                    Capacity = 10
                };

                await context.Rooms.AddAsync(meeting);
                await context.SaveChangesAsync();

                var meetingRoomAmenities = new List<RoomAmenity>();

                meetingRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Conditioner).FirstOrDefault()!.Id,
                    RoomId = meeting.Id
                });

                meetingRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Wifi).FirstOrDefault()!.Id,
                    RoomId = meeting.Id
                });

                meetingRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Headphone).FirstOrDefault()!.Id,
                    RoomId = meeting.Id
                });

                meetingRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Microphone).FirstOrDefault()!.Id,
                    RoomId = meeting.Id
                });

                await context.RoomAmenity.AddRangeAsync(meetingRoomAmenities);
                await context.SaveChangesAsync();
            }

            for (int i = 0; i == 0; i++)
            {
                var meeting = new Room
                {
                    RoomType = RoomType.MeetingRoom,
                    RoomName = "Meeting room",
                    Description = "Designed for productive meetings, workshops, or client presentations. Equipped with screens, whiteboards, and comfortable seating to keep your sessions running smoothly.",
                    Capacity = 20
                };

                await context.Rooms.AddAsync(meeting);
                await context.SaveChangesAsync();

                var meetingRoomAmenities = new List<RoomAmenity>();

                meetingRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Conditioner).FirstOrDefault()!.Id,
                    RoomId = meeting.Id
                });

                meetingRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Wifi).FirstOrDefault()!.Id,
                    RoomId = meeting.Id
                });

                meetingRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Headphone).FirstOrDefault()!.Id,
                    RoomId = meeting.Id
                });

                meetingRoomAmenities.Add(new RoomAmenity
                {
                    AmenityId = amenities.Where(a => a.AmenityType == AmenityType.Microphone).FirstOrDefault()!.Id,
                    RoomId = meeting.Id
                });

                await context.RoomAmenity.AddRangeAsync(meetingRoomAmenities);
                await context.SaveChangesAsync();
            }
            #endregion
        }

        await context.SaveChangesAsync();
    }
}
