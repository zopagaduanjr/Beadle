using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Beadle.Core.Models;

namespace Beadle.Core.Services
{
    public class BeadleService : IBeadleService
    {
        public Task<IEnumerable<Student>> GetStudent()
        {
            return Task.Run(() => GenerateStudent());
        }

        private IEnumerable<Student> GenerateStudent()
        {
            var students = new ObservableCollection<Student>();
            //students.Add(new Student("Gail", "Short"));
            //students.Add(new Student("Cody", "Rosario"));
            //students.Add(new Student("Paige", "Brown"));
            //students.Add(new Student("Tina", "Travers"));
            //students.Add(new Student("Aamna", "Gaines"));
            return students;
        }
    }
}
