using System;
using System.IO;
using System.Reflection;

namespace ImageViewer
{
	public static class CustomLoader
    {
		private static readonly Assembly fluentSchedulerAssembly = LoadAssembly();

		private static Assembly LoadAssembly()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			string assemblyName = assembly.FullName.Split(',')[0];
			string resourceName = assemblyName + ".Resources.FluentScheduler.dll";

			Stream imageStream = assembly.GetManifestResourceStream(resourceName);
			long bytestreamMaxLength = imageStream.Length;
			byte[] buffer = new byte[bytestreamMaxLength];
			imageStream.Read(buffer, 0, (int)bytestreamMaxLength);

			Assembly loadedAssembly = Assembly.Load(buffer);

			AppDomain.CurrentDomain.AssemblyResolve +=
				delegate (object o, ResolveEventArgs args)
				{
					if (args.Name == "FluentScheduler" ||
						args.Name == "FluentScheduler, Version=5.3.0.0, Culture=neutral, PublicKeyToken=b76503528a14ebd1")
						return fluentSchedulerAssembly;
					else
						return null;
				};

			return loadedAssembly;
		}

		internal static Type GetType(string typeName)
		{
			Type type = null;
			if (fluentSchedulerAssembly != null)
			{
				type = fluentSchedulerAssembly.GetType(typeName);
				if (null == type)
					throw new ArgumentException("Unable to locate type");
			}
			return type;
		}
	}
}
