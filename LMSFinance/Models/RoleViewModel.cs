using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSFinance.Models
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {

        }

        public RoleViewModel(ApplicationRole role )
        {
            RoleName = role.Name;
            Id = role.Id;
        }

        public string Id { get; set; }
        public string RoleName { get; set; }
    }
}