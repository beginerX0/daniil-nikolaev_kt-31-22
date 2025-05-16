using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using University.Interfaces;
using University.Models;
using Xunit;

namespace University.Tests
{
    public class PrepodavateliIntegrationTests
    {
        public readonly DbContextOptions<UniversityContext> _dbContextOptions;

        public PrepodavateliIntegrationTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<UniversityContext>()
                .UseInMemoryDatabase(databaseName: "prepodavateli_db")
                .Options;
        }
        public async Task GetNagruzkaByPrepodavatelKafedraPredmet_TwoObjects()
        {
            // Arrange
            var ctx = new UniversityContext(_dbContextOptions);
            var studentService = new UniversityService(ctx);
            var prep = new List<Prepodavateli>
            {
                new Prepodavateli
                {
                    Id = 1,
                    Lastname = "Иванов",
                    Name = "Иван",
                    Surname = "Иванович",
                    Kafedra = 1,
                    Nagruzka = 100
                },
                new Prepodavateli
                {
                    Id = 2,
                    Lastname = "Иванов",
                    Name = "Игорь",
                    Surname = "Петрович",
                    Kafedra = 1,
                    Nagruzka = 100
                }
            };
            await ctx.Set<Prepodavateli>().AddRangeAsync(prep);

            var kafd = new List<Kafedri>
            {
                new Kafedri
                {
                    Id = 1,
                    Name = "ИВТ"
                }
            };
            await ctx.Set<Kafedri>().AddRangeAsync(kafd);

            var prepd = new List<PrepDisciplini>
            {
                new PrepDisciplini
                {
                    PrepId = 1,
                    DiscId = 1
                },
                new PrepDisciplini
                {
                    PrepId = 2,
                    DiscId = 2
                }
            };
            await ctx.Set<PrepDisciplini>().AddRangeAsync(prepd);

            var disc = new List<Disciplini>
            {
                new Disciplini
                {
                    Id = 1,
                    Name = "Вычислительная математика"
                },
                new Disciplini
                {
                    Id = 2,
                    Name = "Структуры данных и алгоритмы"
                }
            };
            await ctx.Set<Disciplini>().AddRangeAsync(disc);

            await ctx.SaveChangesAsync();

            // Act
            var filter = new Filters.NagruzkaFilter
            {
                Lastname = "Иванов",
                Name = "Иван",
                Surname = "Иванович",
                Kafedra = "ИВТ",
                Discip = "Вычислительная математика"
            };
            var nagruzkaResult = await studentService.GetNagruzkaAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(1, nagruzkaResult.Length);

        }
    }
}
