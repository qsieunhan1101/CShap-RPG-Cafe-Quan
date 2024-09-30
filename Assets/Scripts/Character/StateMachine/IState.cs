
public interface IState
{
    void OnEnter(Bot bot);
    void OnExercute(Bot bot);
    void OnExit(Bot bot);
}
