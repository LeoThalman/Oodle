using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oodle.Models.ViewModels
{
    public interface ITeacherVM
    {
        Class cl { get; set; }
        List<Class> classList { get; set; }
        List<User> users { get; set; }
        List<Assignment> assignment { get; set; }
        List<ClassNotification> notifs { get; set; }
        List<Tasks> Tasks { get; set; }
        List<Notes> Notes { get; set; }

    }
}
