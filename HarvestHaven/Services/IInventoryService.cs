namespace HarvestHaven.Services
{
    public interface IInventoryService
    {
        Task<string> GetCorrespondingValueForLabel(string labelName);
    }
}
