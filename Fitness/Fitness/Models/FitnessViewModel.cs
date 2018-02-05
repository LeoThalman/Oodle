using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fitness.Models
{
    public class FitnessViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public Student Student { get; set; }

        public IEnumerable<RunData> RunInfo { get; set; }

    }
}