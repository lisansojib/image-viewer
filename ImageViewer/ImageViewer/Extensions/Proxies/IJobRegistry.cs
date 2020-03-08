using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ImageViewer.Extensions.Proxies
{
    public interface IJobRegistry
    {

    }

    public interface ISchedule
    {

	}

	public interface IJob
	{
		void Execute();
	}

	public class ScheduleImpl : ISchedule
	{
		private readonly object instance;

		internal ScheduleImpl(object instance)
		{
			this.instance = instance;
		}

		public void Schedule(object message)
		{
			MethodInfo method = instance.GetType().GetMethod("Info", new[] { message.GetType() });
			method.Invoke(instance, new[] { message });
		}
	}
}
