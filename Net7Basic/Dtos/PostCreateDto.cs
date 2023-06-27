using Net7Basic.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net7Basic.Dtos
{
    public class PostCreateDto
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
