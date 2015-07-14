using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qz.GPS.ViewModel
{
    [Serializable]
    public class Module
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Icon { get; set; }
        public int SubMenu { get; set; }
        public string Code { get; set; }
    }
}
