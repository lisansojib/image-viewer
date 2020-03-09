using MvcModules;

namespace ImageViewer
{
    public class ImageViewerModule : MvcModuleBase
    {
        public override string DefaultController { get { return "DocumentViewer"; } }
        public override string DefaultAction { get { return "Index"; } }

        protected override void Init()
        {
        }
    }
}
