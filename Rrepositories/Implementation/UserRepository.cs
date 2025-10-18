using DataBase;
using Models.Entities;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementation
{
    public class UserRepository(AppDbContext dbContext) : BaseRepository<User>(dbContext), IUserRepository
    {
    }
}
