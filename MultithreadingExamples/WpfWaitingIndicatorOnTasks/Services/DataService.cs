namespace WpfWaitingIndicatorOnTasks.Services
{
    public class ExampleService : IService
    {
        public void LongRunningMethod(int sleepTime)
        {
            System.Threading.Thread.Sleep(sleepTime);
        }
    }
}