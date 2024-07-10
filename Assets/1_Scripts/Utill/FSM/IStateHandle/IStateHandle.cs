

/// <summary>
/// 
/// </summary>
public interface IStateHandle {
	/// <summary>
	/// 
	/// </summary>
	/// <param name="agent">Agent.</param>
	void SetUnit(Unit unit);

	/// <summary>
	/// 
	/// </summary>
	void OnEnter();

	/// <summary>
	/// 
	/// </summary>
	void OnUpdate(float delta);

	/// <summary>
	/// 
	/// </summary>
	void OnLateUpdate(float lateDelta);

	/// <summary>
	/// 
	/// </summary>
	void OnFixedUpdate(float fixedDelta);

	/// <summary>
	/// 
	/// </summary>
	void OnExit();

	/// <summary>
	/// 
	/// </summary>
	/// <returns><c>true</c> if this instance is finish; otherwise, <c>false</c>.</returns>
	bool IsFinish();
}

/// <summary>
/// 
/// </summary>
public class StateHandle : IStateHandle {

	/// <summary>
	/// 
	/// </summary>
	public StateHandle() {
	}

	/// <summary>
	/// agent.
	/// </summary>
	protected Unit parent;

	/// <summary>
	/// 
	/// </summary>
	/// <param name="agent">Agent.</param>
	public void SetUnit(Unit agent) {
		this.parent = agent;
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual void OnEnter() {
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual void OnUpdate(float delta) {
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual void OnLateUpdate(float lateDelta) {
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual void OnFixedUpdate(float fixedDelta)
	{
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual void OnExit() {
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns><c>true</c> if this instance is finish; otherwise, <c>false</c>.</returns>
	public virtual bool IsFinish() {
		return false;
	}
}