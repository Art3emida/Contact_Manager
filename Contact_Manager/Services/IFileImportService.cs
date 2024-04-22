namespace Contact_Manager.Services
{
    public interface IFileImportService
    {
        public List<T> ExtractData<T>(IFormFile file);
    }
}
