using API.DTO;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text.Json;

namespace API.Data;

public class Seed
{

    public static async Task SeedUsers(DataContext context)
    {
        if (await context.Users.AnyAsync()) return;

        var memberData = await File.ReadAllTextAsync("Data/UserSeedData.json");
        var members = JsonSerializer.Deserialize<List<SeedUserDto>>(memberData);

        if (members == null)
        {
            Console.WriteLine("No members found in the seed data.");
            return;
        }

        foreach (var member in members)
        {
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {

                Id = member.Id,
                DisplayName = member.DisplayName,
                Email = member.Email,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Pa$$w0rd")),
                PasswordSalt = hmac.Key,
                Member = new Member
                {
                    Id = member.Id,
                    DOB = member.DOB,
                    ImageUrl = member.ImageUrl,
                    DisplayName = member.DisplayName,
                    CreatedAt = member.CreatedAt,
                    LastActive = member.LastActive,
                    Description = member.Description,
                    City = member.City,
                    Country = member.Country,
                    Gender = member.Gender
                }
            };
            user.Member.Photos.Add(new Photo
            {
                Url = member.ImageUrl!,
                MemberId = member.Id
            });
            context.Users.Add(user);
        }
        await context.SaveChangesAsync();
    }
}
