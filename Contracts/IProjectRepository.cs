using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Contracts
{
    public interface IProjectRepository
    {
        void Add(Project project);
        void Save();
    }
}
