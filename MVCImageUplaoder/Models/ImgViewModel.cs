namespace MVCImageUplaoder.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using Helper_Code.Objects;

    
 

    public class ImgViewModel
    {
        #region Properties

        
        /// Gets or sets Image file.
        
        [Required]
        [Display(Name = "Upload File")]
        public HttpPostedFileBase FileAttach { get; set; }

    
        /// Gets or sets Image file list.
       
        public List<ImgObj> ImgLst { get; set; }

        #endregion
    }
}
