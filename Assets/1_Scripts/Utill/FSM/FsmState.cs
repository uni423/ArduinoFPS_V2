
public abstract class StateBase
{
    public StateMachine.State state;
    protected Unit parent;
    protected virtual IStateHandle handle { private set; get; }

    public StateBase(Unit parent)
    {
        this.parent = parent;        
    }
    abstract public void Enter();

    abstract public void Exit();

    abstract public void HandleInput();

    abstract public void Update(float delta);

    virtual public void FixedUpdate(float fixedDelta) { }

    virtual public void LateUpate(float latedDelta) { }

    public void SetHandle(IStateHandle handle)
    {
        if (this.handle != null)
            this.handle.OnExit();

        this.handle = handle;
        this.handle.SetUnit(parent);
    }

    public bool IsExistHandle()
    {
        return (handle != null);
    }
}