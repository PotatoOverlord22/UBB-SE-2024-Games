using System.Windows;
using SuperbetBeclean.Services;
using SuperbetBeclean.Windows;

public class ChatService : IChatService
{
    private static Dictionary<(MenuWindow, string), ChatWindow> menuWindowChatWindowMap;

    public ChatService()
    {
        menuWindowChatWindowMap = new Dictionary<(MenuWindow, string), ChatWindow>();
    }

    public void CloseChat(MenuWindow mainWindow)
    {
        foreach (var entry in menuWindowChatWindowMap)
        {
            var key = entry.Key;
            if (key.Item1 == mainWindow)
            {
                entry.Value.Close();
                break;
            }
        }
    }
    public void NewChat(MenuWindow mainWindow, string tableType)
    {
        foreach (Window window in Application.Current.Windows)
        {
            if (window.GetType() == typeof(MenuWindow) && window == mainWindow)
            {
                MenuWindow menuWindow = (MenuWindow)window;
                var key = (menuWindow, tableType);

                // Check if a ChatWindow is already open for this MenuWindow and tableType
                if (!menuWindowChatWindowMap.ContainsKey(key))
                {
                    ChatWindow chatWindow = new ChatWindow(this);
                    chatWindow.Owner = menuWindow;
                    chatWindow.Closed += (s, args) => menuWindowChatWindowMap.Remove(key); // Remove from dictionary when closed
                    menuWindowChatWindowMap.Add(key, chatWindow); // Add to dictionary
                    chatWindow.messagingBox.Text = mainWindow.UserName();
                    chatWindow.Show();
                }
                else
                {
                    // Bring existing ChatWindow to the front
                    ChatWindow existingChatWindow = menuWindowChatWindowMap[key];
                    existingChatWindow.Activate();
                }
            }
        }
    }

    public void NewMessage(string message, ChatWindow thisWindow)
    {
        string userName = string.Empty;
        string tableType = string.Empty;

        // Find the userName and tableType corresponding to this window
        foreach (var entry in menuWindowChatWindowMap)
        {
            if (entry.Value == thisWindow)
            {
                userName = entry.Key.Item1.UserName();
                tableType = entry.Key.Item2;
                break;
            }
        }

        // Update all messagingBoxes with the new message
        foreach (var entry in menuWindowChatWindowMap)
        {
            var key = entry.Key;
            var chatWindow = entry.Value;

            // Check if the tableType matches the desired tableType
            if (key.Item2 == tableType)
            {
                // Update the messagingBox in chatWindow with the new message
                chatWindow.messagingBox.Text += " " + userName + " (" + tableType + "): " + message;
            }
        }
    }
}
