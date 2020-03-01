using MvcModules;

namespace ImageViewer
{
    public class MvcModule : MvcModuleBase
    {
        public override string DefaultController { get { return "DocumentViewer"; } }
        public override string DefaultAction { get { return "Index"; } }
    }
}
