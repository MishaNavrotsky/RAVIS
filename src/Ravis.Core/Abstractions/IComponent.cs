using Ravis.Core.Behaviors;

namespace Ravis.Core.Abstractions
{
    internal interface IComponent
    {
        string? Id { get; }
        IEnumerable<IComponent> Children { get; }

        public void Build();
        public void Render();
        public void Layout();
    }
}
