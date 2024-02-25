namespace Challenge_WebApi.ViewModel
{
    public class ViewModelUser
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime LastUpdated { get; set; }
        public virtual List<string>? AreasLaborales { get; set; }
    }
}
