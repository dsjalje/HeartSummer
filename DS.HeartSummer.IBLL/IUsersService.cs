﻿using DS.HeartSummer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.HeartSummer.IBLL
{
    public partial interface IUsersService : IBaseService<Users>
    {
        Users AddEntity(Users entity,out string msg); 
    }
}
