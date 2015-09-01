using System.Diagnostics;

namespace Bit.BitLib
{
	public class ApplicationType
	{
		public enum Type
		{
			Console,
			WinForm,
			WPF
		}

		public static Type GetApplicationType()
		{
			StackTrace stackTrace = new StackTrace();

			string assemblyName = string.Empty;
			string typeName = string.Empty;
			string method = string.Empty;
			foreach(StackFrame sf in stackTrace.GetFrames()) {
				assemblyName = sf.GetMethod().DeclaringType.Assembly.GetName().Name;
				typeName = sf.GetMethod().DeclaringType.FullName;
				method = sf.GetMethod().Name;

				if (assemblyName.Equals("PresentationFramework")
					&& typeName.Equals("System.Windows.Application")
					&& method.Equals("Run"))
					return Type.WPF;
				else if (assemblyName.Equals("System.Windows.Forms")
					&& typeName.Equals("System.Windows.Forms.Application")
					&& method.Equals("Run"))
					return Type.WinForm;
			}

			return Type.Console;
		}
	}
}
