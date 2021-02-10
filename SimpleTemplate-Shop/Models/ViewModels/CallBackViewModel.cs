using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTemplate_Shop.Models.ViewModels
{
    public class CallBackViewModel
    {
        public int Id { get; set; }

        public CallBack CallBack { get; set; }
        public IEnumerable<CallBack> CallBacks { get; set; }
    }
}
