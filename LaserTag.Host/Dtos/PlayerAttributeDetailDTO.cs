using TekHub.Host.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekHub.Host.Dtos
{
    public class GameAttributeDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string CodeName { get; set; } = "";
        public bool IsGun { get; set; }
        public int Value { get; set; }

        public GameAttributeDetailDTO()
        {

        }

        public GameAttributeDetailDTO(int id, string name, string description, string codeName, bool isGun, int value)
        {
            Id = id;
            Name = name;
            Description = description;
            CodeName = codeName;
            IsGun = isGun;
            Value = value;
        }

        public GameAttributeDetailDTO(GameAttribute gameAttribute)
        {
            Id = gameAttribute.Id;
            Name = gameAttribute.Name;
            Description = gameAttribute.Description;
            CodeName = gameAttribute.CodeName;
            IsGun = gameAttribute.IsGun;
            Value = 0;
        }

    }
}
