using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkyWebRazor_Temp.Models
{
    public class Category
    {
        [Key]
        // if the property's name is Id or the classname+Id，key annotation will not required.
        public int Id { get; set; }

        [Required]
        // means the value of this column could not be null.
        [MaxLength(55)]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        [Range(1, 100, ErrorMessage = "Display order should be between 1-100")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }


    }
}
