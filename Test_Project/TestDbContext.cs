using GBC_Travel_Group_32.Data;
using GBC_Travel_Group_32.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Project {
    internal class TestDbContext : ApplicationDBContext {



        public TestDbContext(DbContextOptions<ApplicationDBContext> options) : base(options) {



        }


    


    }
}
