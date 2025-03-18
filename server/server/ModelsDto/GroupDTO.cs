using Server.Core.Models;

namespace Server.API.ModelsDto
{
    public class GroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }       
        public string AdminPassword { get; set; }
    }
}
