namespace JoyMap.Undo.Action
{
    /// <summary>
    /// Defines a contract for actions that support execution and reversal, enabling undo and redo functionality.
    /// </summary>
    /// <remarks>Implement this interface to provide operations that can be executed and subsequently undone.
    /// Typical use cases include command patterns, undo/redo stacks, and transactional editing scenarios. The specific
    /// behavior of undo and execute depends on the implementation. Implementers should ensure that repeated calls to
    /// Execute and Undo maintain consistent and predictable state transitions.</remarks>
    public interface IUndoableAction
    {
        /// <summary>
        /// Gets the name associated with the current instance.
        /// </summary>
        string Name { get; }


        /// <summary>
        /// Executes the operation defined by the implementing class.
        /// </summary>
        void Execute();
        /// <summary>
        /// Reverses the most recent operation, restoring the previous state.
        /// </summary>
        /// <remarks>Use this method to undo the last change made. The specific behavior depends on the
        /// implementation and may not be available in all contexts. Calling this method multiple times may continue to
        /// revert earlier operations, if supported.</remarks>
        void Undo();
    }
}