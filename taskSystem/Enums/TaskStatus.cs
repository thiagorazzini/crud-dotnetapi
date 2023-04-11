using System.ComponentModel;

namespace taskSystem.Enums
{
    public enum TaskStatus
    {
        [Description("A Fazer")]
        Todo = 0,
        [Description("Em andamento")]
        InProgress = 1,
        [Description("Feito")]
        Done = 2
    }
}
