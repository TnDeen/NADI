namespace MVC5.Models
{
    using System.ComponentModel.DataAnnotations;
    public class FilePath
    {
        public int FilePathId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public string userID { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}