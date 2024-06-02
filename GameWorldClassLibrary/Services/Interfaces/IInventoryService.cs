namespace GameWorldClassLibrary.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<string> GetCorrespondingValueForLabel(string labelName);
    }
}
