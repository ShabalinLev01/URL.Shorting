using System.ComponentModel.DataAnnotations;

namespace URL.Shorting.Models
{
    public class Urls
    {
        [Key]
        public int Id { get; set; }
 
        public string? Username { get; set; }
        
        public string Url { get; set; }
 
        public string ShortUrl { get; set; }
        
        public int NumOfClick { get; set; }
    }
}