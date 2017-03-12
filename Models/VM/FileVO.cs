using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5.Models.VM
{
    public class FileVO
    {
        public ApplicationUser CurrentUser { get; set; }
        public File curFile { get; set; }
        public Transaction transantion { get; set; }

        public List<FileVO> lulusList { get; set; }
        public List<FileVO> batalList { get; set; }
    }
}