using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace ImageViewer.Extensions
{
    public class EmbeddedVirtualPathProvider : VirtualPathProvider
    {
        private readonly Assembly assembly = typeof(EmbeddedVirtualPathProvider).Assembly;
        private readonly string[] resourceNames;

        public EmbeddedVirtualPathProvider()
        {
            this.resourceNames = assembly.GetManifestResourceNames();
        }

        private bool IsEmbeddedResourcePath(string virtualPath)
        {
            var checkPath = VirtualPathUtility.ToAppRelative(virtualPath);
            var resourceName = this.GetType().Namespace + "." + checkPath.Replace("~/", "").Replace("/", ".");
            return this.resourceNames.Contains(resourceName);
        }

        public override bool FileExists(string virtualPath)
        {
            return IsEmbeddedResourcePath(virtualPath) || base.FileExists(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsEmbeddedResourcePath(virtualPath))
            {
                return new EmbeddedVirtualFile(virtualPath);
            }
            return base.GetFile(virtualPath);
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (IsEmbeddedResourcePath(virtualPath))
            {
                return null;
            }
            return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }
    }

    public class EmbeddedVirtualFile : VirtualFile
    {
        private readonly string virtualPath;
        private readonly Assembly assembly;

        public EmbeddedVirtualFile(string virtualPath)
            : base(virtualPath)
        {
            this.assembly = this.GetType().Assembly;
            this.virtualPath = VirtualPathUtility.ToAppRelative(virtualPath);
        }

        public override System.IO.Stream Open()
        {
            var resourceName = this.GetType().Namespace + "." + virtualPath.Replace("~/", "").Replace("/", ".");
            return assembly.GetManifestResourceStream(resourceName);

        }
    }
}
