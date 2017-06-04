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
        public int NumberOfCores { get; set; }
        public string MemorySize { get; set; }
        public bool FPU { get; set; }
        public bool Framebuffer { get; set; }

        public WorkSpaceSettings ()
        {
            NumberOfCores = 2;
            MemorySize = "512M";
            FPU = true;
            Framebuffer = true;
        }
    }
}
