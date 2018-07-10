﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Metanit_15_04_EntityFrameworkSort.Models
{
    public enum SortState
    {
        NameAsc,    // по имени по возрастанию
        NameDesc,   // по имени по убыванию
        AgeAsc, // по возрасту по возрастанию
        AgeDesc,    // по возрасту по убыванию
        CompanyAsc, // по компании по возрастанию
        CompanyDesc // по компании по убыванию
    }
}