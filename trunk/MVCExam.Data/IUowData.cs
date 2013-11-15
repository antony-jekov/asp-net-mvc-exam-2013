using MVCExam.Models;
using System;

namespace MVCExam.Data
{
    public interface IUowData : IDisposable
    {
        IRepository<Ticket> Tickets { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Category> Categories { get; }

        IRepository<PriorityType> PriorityTypes { get; }

        int SaveChanges();
    }
}