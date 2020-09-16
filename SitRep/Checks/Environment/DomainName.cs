using SitRep.Interfaces;
using SitRep.NativeMethods;
using System.Text;
using static SitRep.Enums.Enums;

namespace SitRep.Checks.Environment
{
    class DomainName : CheckBase, ICheck
    {
        public bool IsOpsecSafe => true;

        public int DisplayOrder => 1;
        public CheckType CheckType => CheckType.Environment;

        public void Check()
        {
            try
            {
                var success = false;
                var builder = new StringBuilder(260);
                uint size = 260;
                success = kernel32.GetComputerNameEx(kernel32.COMPUTER_NAME_FORMAT.ComputerNameDnsDomain, builder, ref size);
                if (success && builder.Length > 0)
                {
                    Message = builder.ToString();
                }
                else
                {
                    Message = "NOT DOMAIN JOINED [*]";
                }

            }
            catch
            {
                Message = "Check failed [*]";
            }
        }

        public override string ToString()
        {
            return string.Format("Domain Name: {0}", Message);
        }
    }
}
