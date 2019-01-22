using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BeadleSchool.Models;

namespace BeadleSchool.Services
{
    public interface IBeadleService
    {
        Task<IEnumerable<Student>> GetStudent();
    }
}
