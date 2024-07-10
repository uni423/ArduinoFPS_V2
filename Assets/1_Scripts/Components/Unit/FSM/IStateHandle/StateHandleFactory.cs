

public static class StateHandleFactory
{
	static public T Create<T>() where T : new() {

		T handle = new T ();
		//handle.SetAgent (agent);

		return handle;
	}
}