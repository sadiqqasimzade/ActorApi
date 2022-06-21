using System.ComponentModel.DataAnnotations;

namespace ActorApi.DTOs
{
    public class CreateActorDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
    }
}
