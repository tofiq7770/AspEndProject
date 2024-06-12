namespace AspEndProject.Models
{
    public class BaseNameableEntity : BaseEntity
    {
        //    [Required]
        //    [MaxLength(25, ErrorMessage = "Max Length is 25")]
        public string Name { get; set; }
    }
}
