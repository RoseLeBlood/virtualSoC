using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vmstudio.Daten
{
   
    [Serializable]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("Workspace")]
    public class WorkSpaceSettings
    {
        public string ConfigName { get; set; }

        public int NumberOfCores { get; set; }
        public string MemorySize { get; set; }
        public bool FPU { get; set; }
        public bool Framebuffer { get; set; }
        public WorkSpaceOuput BinaryOutput { get; set; }
       

        public WorkSpaceSettings()
        {

        }
        public WorkSpaceSettings (string name, int numberOfCores,
            bool fpu, bool framebuffer, WorkSpaceOuput output)
        {
            NumberOfCores = numberOfCores;
            MemorySize = "512M";
            FPU = fpu;
            Framebuffer = framebuffer;
            BinaryOutput = output;
            ConfigName = name;
        }
    }
}
