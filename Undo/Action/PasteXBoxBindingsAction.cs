using JoyMap.Profile;

namespace JoyMap.Undo.Action
{
    internal class PasteXBoxBindingsAction : /*CommonAction,*/ IUndoableAction
    {
        public PasteXBoxBindingsAction(MainForm mainForm, WorkProfile activeProfile,
            IReadOnlyList<XBoxAxisBinding> copied)
        //: base(mainForm, activeProfile)
        {
            Copied = copied;
        }

        private List<XBoxAxisBindingInstance> Old { get; } = [];
        public string Name => "Paste XBox Axes";

        public IReadOnlyList<XBoxAxisBinding> Copied { get; }

        public void Execute()
        {
            //    Old.Clear();

            //    foreach (var c in Copied)
            //    {
            //        var item = Form.BindingListView.Items.ToEnumerable().FirstOrDefault(x => AxisOf(x.Tag) == c.OutAxis);
            //        if (item?.Tag is XBoxAxisBindingInstance instance)
            //            Old.Add(instance);
            //        var ev = XBoxAxisBindingInstance.Load(Form.InputMonitor, c);
            //        item.SubItems[0].Text = ev.Event.Name;
            //        item.SubItems[1].Text = string.Join(", ", ev.TriggerInstances.Select(x => x.Trigger.InputId.AxisName));
            //        item.SubItems[2].Text = string.Join(", ", ev.Actions.Select(x => x.Action));
            //        item.Tag = ev;
            //        TargetProfile.Events[idx] = ev;
            //    }
            //    Registry.Persist(TargetProfile);
        }

        //private XBoxAxis AxisOf(object? tag)
        //{
        //    if (tag is XBoxAxis axis)
        //        return axis;
        //    if (tag is XBoxAxisBindingInstance axisBindingInstance)
        //        return axisBindingInstance.Binding.OutAxis;
        //    throw new InvalidOperationException($"Row has neither axis nor binding as tag");
        //}

        public void Undo()
        {
            //    for (int i = 0; i < Old.Count; i++)
            //    {
            //        var idx = SelectedRowIndexes[i];
            //        var item = Form.EventListView.Items[idx];
            //        var ev = Old[i];
            //        item.SubItems[0].Text = ev.Event.Name;
            //        item.SubItems[1].Text = string.Join(", ", ev.TriggerInstances.Select(x => x.Trigger.InputId.AxisName));
            //        item.SubItems[2].Text = string.Join(", ", ev.Actions.Select(x => x.Action));
            //        item.Tag = ev;
            //        TargetProfile.Events[idx] = ev;
            //    }
            //    Registry.Persist(TargetProfile);
        }
    }
}
