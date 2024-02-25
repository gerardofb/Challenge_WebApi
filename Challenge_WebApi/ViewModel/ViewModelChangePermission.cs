using System.ComponentModel.DataAnnotations;
namespace Challenge_WebApi.ViewModel
{
    public class ViewModelChangePermission
    {
        public int Id { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "El permiso a reemplazar debe contener al menos 4 caracteres")]
        public string OldPermission { get; set; }
        [Required]
        [MinLength(4,ErrorMessage="El permiso debe contener al menos 4 caracteres")]
        public string NewPermission { get; set; }   
    }
}
