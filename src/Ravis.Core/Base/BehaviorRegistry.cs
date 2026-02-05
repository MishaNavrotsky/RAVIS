using Ravis.Core.Abstractions;

using BehaviorType = System.Type;
using ComponentType = System.Type;
using RendererType = System.Type;


namespace Ravis.Core.Base
{
    public static class BehaviorRegistry
    {
        private static readonly Dictionary<(BehaviorType, RendererType, ComponentType), Action<object, object>> _registry = [];
        public static void Register<TBehavior, TRenderer, TComponent>(Action<TComponent, TRenderer> action)
            where TComponent : ComponentBase
            where TRenderer : IRenderer
            where TBehavior : IBehavior
        {
            var key = (typeof(TBehavior), typeof(TRenderer), typeof(TComponent));
            _registry[key] = (componentObj, rendererObj) =>
            {
                action((TComponent)componentObj, (TRenderer)rendererObj);
            };
        }

        public static void Execute<TBehavior, TRenderer>(ComponentBase component, TRenderer renderer)
            where TRenderer : IRenderer
            where TBehavior : IBehavior
        {
            if (component == null) return;

            var behaviorType = typeof(TBehavior);
            var rendererType = renderer.GetType();
            var componentType = component.GetType();

            component.HasData<TRenderer>();

            while (componentType != null)
            {
                var key = (behaviorType, rendererType, componentType);

                if (_registry.TryGetValue(key, out var action))
                {
                    action(component, renderer);
                    return;
                }

                componentType = componentType.BaseType;
            }
        }
    }
}
