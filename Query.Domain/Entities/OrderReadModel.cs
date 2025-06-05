using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Domain.Entities
{
    // Entity در دیتابیس Query
    public class OrderReadModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
