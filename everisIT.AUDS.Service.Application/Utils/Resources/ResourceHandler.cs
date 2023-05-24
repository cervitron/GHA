using System.Reflection;
using System.Resources;

namespace everisIT.AUDS.Service.Application.Utils.Resources
{
    public class ResourceHandler
    {
        public static string GetResource(string resourceName, string resourceFileName)
        {
            Assembly asm = Assembly.GetCallingAssembly();
            string rsFileName = asm.GetName().Name + ".Utils.Resources." + resourceFileName;
            ResourceManager rm = new ResourceManager(rsFileName, asm);
            string msg2 = rm.GetString(resourceName);
            return msg2;
        }
    }
}
