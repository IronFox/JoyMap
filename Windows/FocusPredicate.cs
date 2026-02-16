using JoyMap.Profile;

namespace JoyMap.Windows;


public readonly record struct FocusPredicate(WindowReference? TopMost, WindowReference? Focused)
{
    public bool Any => TopMost is not null || Focused is not null;

    public static FocusPredicate Get()
    {
        return new(WindowReference.OfTopMost(), WindowReference.OfFocused());
    }

    internal bool ProcessNameIsMatch(ProcessRegex processNameRegex)
    {
        if (Focused is not null && processNameRegex.IsMatch(Focused))
            return true;
        if (TopMost is not null && processNameRegex.IsMatch(TopMost))
            return true;
        return false;
    }
}