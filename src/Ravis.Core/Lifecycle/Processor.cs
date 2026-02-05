using Ravis.Core.Abstractions;
using Ravis.Core.Base;
using Ravis.Core.Behaviors;

namespace Ravis.Core.Lifecycle
{
    public class Processor : IProcessor
    {
        public void Process(ComponentBase component, IRenderer renderer)
        {
            component.Build();
            BehaviorRegistry.Execute<Build, IRenderer>(component, renderer);
            foreach (var child in component.Children)
            {
                Process(child, renderer);
            }

            component.Layout();
            component.Render();

            BehaviorRegistry.Execute<Layout, IRenderer>(component, renderer);
            BehaviorRegistry.Execute<Render, IRenderer>(component, renderer);
        }
    }
}
