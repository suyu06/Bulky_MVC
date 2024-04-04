using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        [Key] 
        // if the property's name is Id or the classname+Id，key annotation will not required.
        public int Id { get; set; }

        [Required]
        // means the value of this column could not be null.
        public string CategoryName { get; set; }
        public int DisplayOrder { get; set; }
   

    }
}
