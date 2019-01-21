using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Beadle.Core.Models;

namespace Beadle.Core.Services
{
    public interface IBeadleService
    {
        Task<IEnumerable<Student>> GetStudent();
    }
}
