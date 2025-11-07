namespace JoyMap.ControllerTracking
{
    public class ControllerStatus(Guid id)
    {
        private InputState? LastState { get; set; }

        private HashSet<Guid> AcquiredBy { get; } = [];

        public bool IsAcquired => AcquiredBy.Count > 0;

        private List<InputAxisChange> NewChanges { get; } = [];
        public Guid Id { get; } = id;


        protected virtual void SignalSignificantChanges(IReadOnlyCollection<InputAxisChange> changes)
        {
        }

        internal void Update(InputState state)
        {
            if (LastState != null)
            {
                InputState.DetectSignificantChanges(LastState.Value, state, NewChanges);
                if (NewChanges.Count > 0)
                    SignalSignificantChanges(NewChanges);
            }
            LastState = state;
        }


        /// <summary>
        /// Fetches the latest status value for the specified input.
        /// </summary>
        /// <param name="which"></param>
        /// <returns></returns>
        internal float? Get(InputAxis which)
        {
            if (LastState is null || AcquiredBy.Count != 1)
                return null;
            return LastState.Value.Get(which);
        }



        internal virtual void SignalBegin(Guid instanceGuid)
        {
            AcquiredBy.Add(instanceGuid);
        }

        internal virtual void SignalEnd(Guid instanceGuid)
        {
            AcquiredBy.Remove(instanceGuid);
        }
    }
}
