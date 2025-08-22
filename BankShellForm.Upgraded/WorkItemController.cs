using Microsoft.Practices.CompositeUI;

namespace BankShellForm.Upgraded;

public abstract class WorkItemController : IWorkItemController
{
    [ServiceDependency]
    public WorkItem WorkItem { get; set; }

    // Virtual Run method, override in derived controllers
    public virtual void Run() { }
}