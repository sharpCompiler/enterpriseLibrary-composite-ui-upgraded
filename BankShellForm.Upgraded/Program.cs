using Microsoft.Practices.CompositeUI.WinForms;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;

namespace BankShellForm.Upgraded
{
    //internal static class Program
    //{
    //    /// <summary>
    //    ///  The main entry point for the application.
    //    /// </summary>
    //    [STAThread]
    //    static void Main()
    //    {
    //        // To customize application configuration such as set high DPI settings or default font,
    //        // see https://aka.ms/applicationconfiguration.
    //        ApplicationConfiguration.Initialize();
    //        Application.Run(new Form1());
    //    }
    //}


    public class BankShellApplication : FormShellApplication<WorkItem, Form1>
    {
        [STAThread]
        public static void Main()
        {
            new BankShellApplication().Run();
        }

        // This method is called just after your shell has been created (the root work item
        // also exists). Here, you might want to:
        //   - Attach UIElementManagers
        //   - Register the form with a name.
        //   - Register additional workspaces (e.g. a named WindowWorkspace)
        protected override void AfterShellCreated()
        {
            base.AfterShellCreated();

            //ToolStripMenuItem fileItem = (ToolStripMenuItem)Shell.MainMenuStrip.Items["File"];

            RootWorkItem.WorkItems.AddNew
            //RootWorkItem.UIExtensionSites.RegisterSite(UIExtensionConstants.MAINSTATUS, Shell.mainStatusStrip);
            //RootWorkItem.UIExtensionSites.RegisterSite(UIExtensionConstants.FILE, fileItem);
            //RootWorkItem.UIExtensionSites.RegisterSite(UIExtensionConstants.FILEDROPDOWN, fileItem.DropDownItems);

            //// Load the menu structure from App.config
            //UIElementBuilder.LoadFromConfig(RootWorkItem);


        }

    }

    public interface IWorkItemController
    {
        void Run();
    }
}


