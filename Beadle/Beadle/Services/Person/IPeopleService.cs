using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Beadle.Models;

namespace Beadle.Services
{
    public interface IPeopleService
    {
        Task<IEnumerable<Person>> GetPeople();
    }
}
