﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekHub.Host.Models
{
    public class GameAttribute
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string CodeName { get; set; } = "";
        public bool IsGun { get; set; }

        public GameAttribute Clone()
        {
            return new GameAttribute
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                CodeName = this.CodeName,
                IsGun = this.IsGun
            };
        }

        public override string ToString()
        {
            return $"{Name} + ({CodeName})";
        }
    }
}
