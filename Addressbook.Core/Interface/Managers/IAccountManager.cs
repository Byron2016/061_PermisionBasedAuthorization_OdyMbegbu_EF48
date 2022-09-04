﻿using Addressbook.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbook.Core.Interface.Managers
{
    public interface IAccountManager
    {
        Operation<UserModel> CreateUser(UserModel user);
    }
}
