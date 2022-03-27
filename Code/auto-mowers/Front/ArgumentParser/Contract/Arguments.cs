using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace auto_mowers.Front.ArgumentParser.Contract
{
    public class Arguments
    {
        [CommandLine.Option(Required = true)]
        public string FilePath { get; set; } = String.Empty;
    }
}
