using JoyMap.Undo.Action;

namespace JoyMap.Undo
{
    public class UndoHistory
    {
        private Stack<IUndoableAction> UndoStack { get; } = new();
        private Stack<IUndoableAction> RedoStack { get; } = new();
        public bool CanUndo => UndoStack.Count > 0;
        public bool CanRedo => RedoStack.Count > 0;

        public string? NextUndoName => CanUndo ? UndoStack.Peek().Name : null;
        public string? NextRedoName => CanRedo ? RedoStack.Peek().Name : null;
        public IUndoableAction? NextUndoAction => CanUndo ? UndoStack.Peek() : null;

        public void ExecuteAction(IUndoableAction action)
        {
            action.Execute();
            UndoStack.Push(action);
            RedoStack.Clear();
        }
        public void Undo()
        {
            if (CanUndo)
            {
                var action = UndoStack.Pop();
                action.Undo();
                RedoStack.Push(action);
            }
        }
        public void Redo()
        {
            if (CanRedo)
            {
                var action = RedoStack.Pop();
                action.Execute();
                UndoStack.Push(action);
            }
        }
    }
}
