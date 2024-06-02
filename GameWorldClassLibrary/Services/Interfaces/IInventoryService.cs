namespace GameWorldClassLibrary.Services
{
    public interface IInventoryService
    {
        Task<string> GetCorrespondingValueForLabel(string labelName);
    }
}
