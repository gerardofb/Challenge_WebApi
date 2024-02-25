namespace Challenge_WebApi.ViewModel
{
    public class ViewModelPermissionsUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public Guid PermissionGuid { get; set; }
        public string PermissionName { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}
