using Microsoft.Practices.CompositeUI.Commands;

namespace BankShellForm.Upgraded;

public class OrderController : WorkItemController
{
    [CommandHandler("SaveCommand")]
    public void OnSaveCommand(object sender, EventArgs e)
    {
        MessageBox.Show("Save command executed!", "CommandHandler");
    }

    [CommandHandler("ExitCommand")]
    public void OnExitCommand(object sender, EventArgs e)
    {
        MessageBox.Show("Application will exit.", "CommandHandler");
        Application.Exit();
    }
}