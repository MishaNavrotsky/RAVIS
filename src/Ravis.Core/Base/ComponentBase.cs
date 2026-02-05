using Ravis.Core.Abstractions;

using RendererType = System.Type;

namespace Ravis.Core.Base
{
    public abstract class ComponentBase : Abstractions.IComponent
    {

        public string? Id { get; set; }
        public List<ComponentBase> Children { get; } = [];
        IEnumerable<IComponent> IComponent.Children => Children;

        internal Dictionary<RendererType, object> RenderData { get; } = [];

        public TComponentRendererData GetData<TRenderer, TComponentRendererData>()
            where TRenderer : IRenderer
            where TComponentRendererData : new()
        {
            var key = typeof(TRenderer);
            if (!RenderData.TryGetValue(key, out var data))
            {
                data = new TComponentRendererData();
                RenderData[key] = data;
            }

            if (data is not TComponentRendererData typed)
            {
                throw new InvalidOperationException(
                    $"[ComponentBase] Render data for {typeof(TRenderer)} is not {typeof(TComponentRendererData)}, it is of type {data?.GetType() ?? typeof(void)}"
                );
            }

            return typed;
        }

        public TComponentRendererData GetData<TRenderer, TComponentRendererData>(TComponentRendererData data)
            where TRenderer : IRenderer
        {
            RenderData[typeof(TRenderer)] = data!;
            return data;
        }

        public bool HasData<TRenderer>() where TRenderer : IRenderer => RenderData.ContainsKey(typeof(TRenderer));

        public virtual void Build() { }
        public virtual void Render() { }
        public virtual void Layout() { }
    }
}
