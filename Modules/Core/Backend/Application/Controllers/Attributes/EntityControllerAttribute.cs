namespace Olympus.Core.Backend;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class EntityControllerAttribute(EntityControllerActions enabledActions = EntityControllerActions.All) : Attribute {

	public EntityControllerActions EnabledActions { get; } = enabledActions;

}
