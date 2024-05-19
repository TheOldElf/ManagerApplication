using ManagerApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerApplication.Domain
{
    //обеспечивает доступ к коллекциям данных и управляет операциями CRUD
    public class ManagerAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        //конструктор класса содержащий настройки контекста базы данных
        public ManagerAppContext(DbContextOptions<ManagerAppContext> options) : base(options) { }
    }
}
