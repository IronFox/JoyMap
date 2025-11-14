
# JoyMap

## Introduction

JoyMap is a Joystick to Keyboard mapping program, somewhat akin to JoyToKey, however more powerful in most aspects.

Key differences

1) JoyMap maintains lists of events, where each event can use any number of joystick inputs, including those already in use by other events.
1) Independent Joystick ranges on every trigger on every event.
While the range defaults to [50%,100%], you can define your own with fraction precision (12.34567%).
1) Simple and complex trigger expressions (and, or, (T0 && T1) || T2, etc.)
1) Any number of independent Actions that execute if the given combination of triggers on an Event fires.
Each Action can define a delay, repetition frequency, and max repetition count.
1) Joysticks are mapped by product GUID -> instance GUID.
So if the instance GUID changes, it will default to any other instance of the same product, but use the specific instance if it exists.
1) Automatic switching and activation based on window and process name of the focused window
1) Autosave and undo/redo

## Full Manual
### Profiles
The main construct that defines what the program does is a profile.
A profile has a name and is associated with one or more games.
To create a new profile, start the game, then use the main menu (Profiles -> New from Window...) to pick its window and create a new profile.
Alternatively you can create a new empty profile (Profiles -> New Empty). It will have pre-filled profile, process, and window names, which can now be freely edited.
Process and window names are regular expressions.
Apart from editing, the "Pick ..." button allows selecting a new process/window name combo.
Empty regex's will always match, but at least one (process or window name) must be set correctly or the profile will never activate.
The "Profile: ..." drop down in the top section of the window allows selecting any created profile.
Focusing a game window that matches a registered process or window name will automatically switch to that profile.

The "Delete" button can be used to delete a profile.
Alternatively, profiles are automatically removed if they are restored from file without events.
Removal of a profile can currently not be undone.

### Save and Load

If the application is compiled and runs in its release configuration, all changes to profiles are immediately written to file.
Otherwise, you can test modifications without changing the file, and the main menu File -> Save (...) entry will save everything.

When starting, the program will attempt to restore its profile/event configuration from file.

This file is currently stored in
   [My Documents]\JoyMap\Profiles.json

### Events
An event is a combination of triggers and actions. If the trigger combination is considered active, the actions are executed while the target game window is focused
(or any non JoyMap window if Profiles->"Run Only when Game is Focused" is disabled).

#### Event List
Most of the main window is one huge list of events. Right click the list, chose 'New ...' to create a new event.
The buttons and context menu also allow you to change the order, select, edit, copy, paste, and delete events.
Note that the order of events does not matter.
It is only a way to structure the list according to personal preferences.

The list shows four columns: 'Name' is the customizable name of the respective event,
'Triggers' an abbreviation of what causes the event to activate,
and 'Actions' an abbreviation of what it does while active.
The 'Active' column is empty if the event is currently not active according to its trigger configuration, 
and a single 'A' if it currently is and would execute if the target game window was focused

#### Event Dialog
Editing an existing or creating a new event will open a dialog to do so.
The dialog features a name input in the top section, a trigger section, and an action section.
Note that any changes made here will take effect only when pressing the "Update / Create" button. This dialog does not have an undo feature.

**_Triggers_**

The trigger section is primarily a list of possible triggers (joystick buttons or axes), plus a combiner that tells the system how to combine multiple triggers.
The columns show the automatically determined label, the device (Joystick), axis/button name, and status.
As with the event list, the status column shows nothing if the trigger is considered inactive, otherwise a single A.
To add a new trigger open the context menu and select "Pick/Add ...". Likewise, existing ones can be edited or deleted.

**_Combiners_**

The combiner input is both a drop-down and a text input. By default, it supports **And** and **Or**, which respectively activate if **_all_** or **_at least one_** trigger is currently active.
Alternatively, it supports complex expressions where triggers are referenced by their label (T0, T1, T2, ...) and can be combined using ||/or, &&/and, !/not, and ().
E.g. "(T0 and T1 and T2) or not T3", which is identical to writing "(T0 && T1 && T2) || !T3".

**_Actions_**

The bottom half of the event dialog is composed of a list of independent actions to perform when the trigger combination is determined as active.
As with the trigger list, you can add, edit, or remove actions with the context menu.

#### Trigger Dialog
The trigger dialog opens if you create a new or edit an existing trigger.
The top two rows show the device (Joystick) and input (axis/button). To change those, use the "Pick ..." button.
The Min% and Max% inputs will defining the minimum and maximum threshold for the current axis percentage.
For buttons, the axis percentage is 0% if not pressed and 100% if pressed.
To invert activation, set min/max to something like 0%/50%.

The option "Auto Release after (ms):" allows forcing the trigger result to off if it has been active for that many milliseconds. Once the device axis is moved out of the min/max range, then in again, this timer will reset.

The option "Delay Release by (ms):" enables to extend the active time after the device axis was moved out of the min/max range. If both "Auto Release after (ms)" and "Delay Release by (ms)" are set, the delay will effectively extend the auto release timer.
Like other dialogs, this dialog's inputs will take effect only when pressing "Update / Create".

#### Pick Axis Dialog
This dialog records and changes in device buttons and axes and provides them for selection. Axes may be listed twice for positive and negative values. Buttons will be listed once only if they have been pressed while the dialog is open.
Closing and re-opening the dialog will flush all previous inputs.

#### Action Dialog
This dialog allows configuring a single action to run if an event's trigger composition activates.
Currently, only one key/mouse event may be issued, either exactly while the trigger is active, or auto-firing at a fixed frequency.
Initial delay is the time in milliseconds, until this action starts. The key or mouse button can be selected and changed via the "Pick ..." button.
It may also automatically name the action if it was not previously manually changed.
If the "Auto Trigger Frequency" checkbox is checked, the chosen key/button will virtually be pressed and released at that frequency (pressed times per second).

The auto-triggering can be delayed by checking "Delay Start (ms):". Doing so will hold the simulated key press until the delay has passed, then switch to autofire.

The auto-triggering can further be limited by checking "Limit Auto-Triggers". If set, only that many times, the key/button will be pressed. The last time, it will be held until they trigger combination becomes inactive.
 



## Assets used
This application uses a free icon from:
<a href="https://www.flaticon.com/free-icon/joystick_12585353?term=joystick&page=1&position=15&origin=tag&related_id=12585353" title="Joystick free icon">Joystick free icon - Flaticon</a>
