using JoyMap.Undo.Action;

namespace JoyMap.Undo
{
    public class UndoHistory
    {
        private readonly Stack<IUndoableAction> undoStack = new();
        private readonly Stack<IUndoableAction> redoStack = new();
        public bool CanUndo => undoStack.Count > 0;
        public bool CanRedo => redoStack.Count > 0;

        public string? NextUndoName => CanUndo ? undoStack.Peek().Name : null;
        public string? NextRedoName => CanRedo ? redoStack.Peek().Name : null;
        public IUndoableAction? NextUndoAction => CanUndo ? undoStack.Peek() : null;

        public void ExecuteAction(IUndoableAction action)
        {
            action.Execute();
            undoStack.Push(action);
            redoStack.Clear();
        }
        public void Undo()
        {
            if (CanUndo)
            {
                var action = undoStack.Pop();
                action.Undo();
                redoStack.Push(action);
            }
        }
        public void Redo()
        {
            if (CanRedo)
            {
                var action = redoStack.Pop();
                action.Execute();
                undoStack.Push(action);
            }
        }
    }
}
