﻿using ClickMart.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClickMart.DataAccess.Repository.IRepository
{
    public interface IUserRepository: IRepository<User>
    {
        User GetUserWithAddresses(Expression<Func<User,bool>> filter);
    }
}
