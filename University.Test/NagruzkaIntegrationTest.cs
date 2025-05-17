using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using University.Interfaces;
using University.Models;

namespace University.Test
{
    public class NagruzkaIntegrationTest
    {
        public readonly DbContextOptions<UniversityContext> _dbContextOptions;

        public NagruzkaIntegrationTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<UniversityContext>()
                .UseInMemoryDatabase(databaseName: "prepodavateli_db")
                .Options;
        }

        [Fact]
        public async Task GetStudentsByGroupAsync_KT3120_TwoObjects()
        {
            // Arrange
            var ctx = new UniversityContext(_dbContextOptions);
            var universityService = new UniversityService(ctx);
            var prepodavatelis = new List<Prepodavateli>
            {
                new Prepodavateli
                {
                    Id = 1,
                    Lastname="Иванов",
                    Name = "Иван",
                    Surname = "Иванович",
                    Kafedra = 1,
                    Nagruzka = 120,
                },
                new Prepodavateli
                {
                    Id = 2,
                    Lastname="Иванов",
                    Name = "Иван",
                    Surname = "Иванович",
                    Kafedra = 1,
                    Nagruzka = 100,
                }
            };

            var kafedri = new List<Kafedri>
            {
                new Kafedri
                {
                    Id = 1,
                    Name = "ИВТ",
                    Zav = 1
                }
            };

            var discipl = new List<Disciplini>
            {
                new Disciplini
                {
                    Id = 1,
                    Name = "Вычислительная математика",
                    Deleted = false
                },
                new Disciplini
                {
                    Id = 2,
                    Name = "Инженерная графика",
                    Deleted = false
                }
            };

            var prepdiscipl = new List<PrepDisciplini>
            {
                new PrepDisciplini
                {
                    Id = 1,
                    PrepId = 1,
                    DiscId = 1
                },
                new PrepDisciplini
                {
                    Id = 2,
                    PrepId = 2,
                    DiscId = 2
                }
            };
            await ctx.Set<Prepodavateli>().AddRangeAsync(prepodavatelis);
            await ctx.Set<Kafedri>().AddRangeAsync(kafedri);
            await ctx.Set<Disciplini>().AddRangeAsync(discipl);
            await ctx.Set<PrepDisciplini>().AddRangeAsync(prepdiscipl);
            await ctx.SaveChangesAsync();

            // Act
            var filter = new Filters.NagruzkaFilter
            {
                Lastname="Иванов",
                Name = "Иван",
                Surname = "Иванович",
                Kafedra = "ИВТ",
                Discip = "Вычислительная математика"
            };
            var studentsResult = await universityService.GetNagruzkaAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(1, studentsResult.Length);
        }
    }
}
