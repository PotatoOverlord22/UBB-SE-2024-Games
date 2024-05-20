namespace GameWorld.Services
{
    public interface IInventoryService
    {
        Task<string> GetCorrespondingValueForLabel(string labelName);
    }
}
