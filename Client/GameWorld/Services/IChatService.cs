using GameWorld.Views;

namespace GameWorld.Services
{
    public interface IChatService
    {
        void CloseChat(MenuWindow mainWindow);
        void NewChat(MenuWindow mainWindow, string tableType);
        void NewMessage(string message, ChatWindow thisWindow);
    }
}
