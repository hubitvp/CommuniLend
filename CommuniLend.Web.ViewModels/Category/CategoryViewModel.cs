using CommuniLend.Data.Models.Constraints;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommuniLend.Web.ViewModels.Category
{
    public class CategoryViewModel
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

    }
}
