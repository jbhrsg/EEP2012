using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Drawing.Design;
using System.Configuration;
using System.Workflow.ComponentModel.Design;

namespace FLDesignerCore
{
    public class FLDesignSurface : DesignSurface
    {
        public FLDesignSurface()
            : base()
        {
        }

        public FLDesignSurface(Type rootComponentType)
            : base(rootComponentType)
        {
        }

        public FLDesignSurface(IServiceProvider provider, Type rootComponentType)
            : base(provider, rootComponentType)
        {
        }

        public FLDesignSurface(IServiceProvider provider)
            : base(provider)
        {
        }

        public void AddService(Type serviceType, object serviceInstance)
        {
            this.ServiceContainer.RemoveService(serviceType);
    
            this.ServiceContainer.AddService(serviceType, serviceInstance);
        }
    }
}
