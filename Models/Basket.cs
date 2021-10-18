using System;
using System.Collections.Generic;

namespace OrleansTest.Models
{
    [Serializable]
    public class Basket
    {
        public string Host { get; set; } 
        public Guid CustomerId { get; set; }
        public IList<long> Items { get; } = new List<long>();
    }
}