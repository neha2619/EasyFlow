using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    internal class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasData
            (
                new Admin
                {
                    Id = new Guid("8A6EA087-CBC8-4699-92CE-13056AE550FE"),
                    Name = "Sam Raiden",
                    Email = "Samraiden@gmail.com",
                    Mobile = "8976543209",
                    Pass="hshskhsk"

                }




                );
        }


    }
    }
