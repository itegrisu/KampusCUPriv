using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.OtherDtos
{
    public class LogErrorDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string GidUserFK { get; set; }
        public string TimeStamp { get; set; }
        public int LineNumber { get; set; }
        public string Action { get; set; }
        public string FileName { get; set; }    
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
    }
}
