using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace SimpleTemplate_Shop.Models
{
    public class CallBack
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ContactForm { get; set; }
        public string Message { get; set; }

        public DateTime callTime = DateTime.Now;

        public DateTime CallTime
        {
            get { return callTime; }
            set { callTime = value; }

        }

        [BindNever]
        public bool Marked { get; set; }
    }
}
