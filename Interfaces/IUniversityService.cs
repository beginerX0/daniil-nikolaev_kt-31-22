using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using University.Filters;
using University.Models;

namespace University.Interfaces
{
    public interface IUniversityService
    {
        public Task<string[][]> GetNagruzkaAsync(NagruzkaFilter filter, CancellationToken cancellationToken);
        public Task<int> AddDiscip(AddDiscipFilter filter, CancellationToken cancellationToken);
        public Task<int> DeleteDiscip(DeleteDiscipFilter filter, CancellationToken cancellationToken);
        public Task<int> UpdateDiscip(UpdateDiscipFilter filter, CancellationToken cancellationToken);
    }
    public class UniversityService: IUniversityService
    {
        private readonly UniversityContext _dbContext;
        public UniversityService(UniversityContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<string[][]> GetNagruzkaAsync(NagruzkaFilter filter, CancellationToken cancellationToken)
        {
            var info = _dbContext.Set<Prepodavateli>()
                .Join(_dbContext.Set<Kafedri>(), i => i.Kafedra, j => j.Id, (i, j) => new { id = i.Id, ln = i.Lastname, n = i.Name, sn = i.Surname, kaf = j.Name, nag = i.Nagruzka })
                .Join(_dbContext.Set<PrepDisciplini>(), i => i.id, j => j.PrepId, (i, j) => (new string[] { i.ln, i.n, i.sn, i.kaf, j.Disc.Name, i.nag.ToString() }))
                ;
            var info1 = new List<string[]>();
            foreach (var item in info.Select(i=>i))
            {
                if ((item[0] == filter.Lastname || filter.Lastname == "string") &&
                    (item[1] == filter.Name || filter.Name == "string") &&
                    (item[2] == filter.Surname || filter.Surname == "string") &&
                    (item[3] == filter.Kafedra || filter.Kafedra == "string") &&
                    (item[4] == filter.Discip || filter.Discip == "string"))info1.Add(item);
            }
            return Task.Delay(0).ContinueWith(task => info1.ToArray());
        }

        public Task<int> AddDiscip(AddDiscipFilter filter, CancellationToken cancellationToken)
        {
            using (UniversityContext db = new UniversityContext())
            {
                Disciplini disciplini = new Disciplini
                {
                    Name = filter.Name,
                    Deleted = false
                };
                db.Add(disciplini);
                db.SaveChanges();
            }
            return Task.Delay(0).ContinueWith(i => 1);
        }

        public Task<int> DeleteDiscip(DeleteDiscipFilter filter, CancellationToken cancellationToken)
        {
            using (UniversityContext db = new UniversityContext())
            {
                var disciplina = db.Disciplinis.FirstOrDefault(i=>i.Id==filter.Id);
                disciplina!.Deleted = true;
                db.SaveChanges();
            }
            return Task.Delay(0).ContinueWith(i => 1);
        }

        public Task<int> UpdateDiscip(UpdateDiscipFilter filter, CancellationToken cancellationToken)
        {
            using (UniversityContext db = new UniversityContext())
            {
                var disciplina = db.Disciplinis.FirstOrDefault(i => i.Id == filter.Id);
                disciplina!.Name = filter.Name;
                db.SaveChanges();
            }
            return Task.Delay(0).ContinueWith(i => 1);
        }
    }
}
