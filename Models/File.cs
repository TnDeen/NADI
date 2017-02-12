using System.ComponentModel.DataAnnotations;

namespace MVC5.Models
{
    public class File 
    {
        public int FileId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileType FileType { get; set; }
        public string userId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}