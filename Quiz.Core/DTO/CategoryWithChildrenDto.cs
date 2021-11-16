﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.DTO
{
    public class CategoryWithChildrenDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryWithChildrenDto> SubCategories { get; set; } = new();
        public int? ParentCategoryId { get; set; }

    }
}
