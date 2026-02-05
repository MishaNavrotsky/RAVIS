using Ravis.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ravis.Core.Abstractions
{
    public interface IProcessor
    {
        void Process(ComponentBase component, IRenderer renderer);
    }
}
