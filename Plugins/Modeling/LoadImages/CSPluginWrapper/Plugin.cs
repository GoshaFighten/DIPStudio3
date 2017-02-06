using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSPlugin
{
    public class Plugin
    {
        public static OutputObject Run(InputObject args) {
            var result = new OutputObject();
            result.Result = (args.W + 5) * args.Input;
            return result;
        }
    }
}
