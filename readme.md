# JoyMap

## Table of Contents

- [Introduction](#introduction)
- [Installation](#installation)
- [Quick Start](#quick-start)
- [Full Manual](#full-manual)
  - [Profiles](#profiles)
    - [Settings Tab](#settings-tab)
  - [Save and Load](#save-and-load)
  - [Controller Families](#controller-families)
  - [Global Statuses](#global-statuses)
    - [Global Status Modes](#global-status-modes)
    - [Creating/Editing a Global Status](#creatingediting-a-global-status)
    - [Referencing Global Statuses](#referencing-global-statuses)
  - [Mode Groups](#mode-groups)
    - [Creating/Editing a Mode Group](#creatingediting-a-mode-group)
    - [Mode Entry Dialog](#mode-entry-dialog)
    - [How Mode Switching Works](#how-mode-switching-works)
    - [Referencing Modes in Expressions](#referencing-modes-in-expressions)
    - [Key Triggers](#key-triggers)
  - [Xbox Controller Emulation](#xbox-controller-emulation)
    - [Xbox Axis Binding Tab](#xbox-axis-binding-tab)
    - [Creating/Editing an Axis Binding](#creatingediting-an-axis-binding)
    - [Enable Switch](#enable-switch)
    - [Xbox Button Mapping (via Events)](#xbox-button-mapping-via-events)
    - [Virtual Controller Lifecycle](#virtual-controller-lifecycle)
  - [Events](#events)
    - [Event List](#event-list)
    - [Event Dialog](#event-dialog)
    - [Triggers Section](#triggers-section)
    - [Combiner Help Dialog (? Button)](#combiner-help-dialog--button)
    - [Trigger Dialog](#trigger-dialog)
      - [Range Tab (Traditional Trigger)](#range-tab-traditional-trigger)
      - [Dither Tab (PWM Duty-Cycle Trigger)](#dither-tab-pwm-duty-cycle-trigger)
    - [Pick Axis Dialog](#pick-axis-dialog)
    - [Actions Section](#actions-section)
    - [Action Dialog](#action-dialog)
    - [On-Change Action](#on-change-action)
  - [Undo/Redo](#undoredo)
  - [Advanced Features](#advanced-features)
    - [Profile Suspension](#profile-suspension)
    - [Clipboard Operations](#clipboard-operations)
    - [Keyboard Shortcuts Summary](#keyboard-shortcuts-summary)
- [Troubleshooting](#troubleshooting)
- [Tips & Best Practices](#tips--best-practices)
- [Technical Details](#technical-details)
- [Known Limitations](#known-limitations)
- [Credits](#credits)

<a id="introduction"></a>
## Introduction

JoyMap is a powerful Joystick-to-Keyboard/Mouse/Xbox mapping program that extends beyond tools like JoyToKey with advanced features for complex input routing and profile management.

**Key Features:**

1. **Multi-trigger Events**: Each event can use any number of joystick inputs, including those already used by other events
2. **Independent Axis Ranges**: Every trigger on every event has independent min/max thresholds with fraction precision (e.g., 12.34567%)
3. **Complex Trigger Logic**: Boolean expressions (AND, OR, NOT, parentheses) with trigger labels (T0, T1, ...) plus live combiner help dialog
4. **Action Timing**: Actions support delays, auto-fire frequency, explicit hold/gap timing cycles, and repetition limits
5. **Smart Device Mapping**: Controllers mapped by Product GUID â†’ Instance GUID with automatic fallback to any instance of the same product
6. **Profile Auto-switching**: Automatically activates profiles based on focused window process/title (regex)
7. **Undo/Redo Support**: Full undo/redo history for profile editing with auto-save in Release builds
8. **Controller Families**: Group different controller models to treat them as interchangeable
9. **Virtual Xbox 360 Controller**: Built-in Xbox controller emulation via ViGEmBus driver
10. **Global Statuses**: Named boolean values (always-on/off, combiner-driven, or toggle) referenceable in any combiner expression
11. **Mode Groups**: Mutually exclusive named modes driven by triggers; active mode referenced in combiners (M0, M1, ...)
12. **Global Key Triggers**: Any keyboard key can be a trigger input, monitored system-wide regardless of focused window
13. **Xbox Axis Enable Switch**: Axis bindings can be gated by any global status or mode entry

<a id="installation"></a>
## Installation

1. Download and install the **ViGEmBus driver** from https://github.com/nefarius/ViGEmBus/releases (required for Xbox controller emulation)
2. Launch JoyMap.exe
3. Connect your joystick/gamepad via USB or Bluetooth

<a id="quick-start"></a>
## Quick Start

1. **Create a Profile**: 
   - Launch your game
   - In JoyMap: **Profiles** â†’ **New from Window...**
   - Click the game window to capture its process/window name
   
2. **Add Events**:
   - Right-click the event list â†’ **New ...**
   - Add triggers (joystick inputs) and actions (keyboard/mouse/Xbox buttons)
   
3. **Automatic Activation**: Focus your game window and JoyMap will automatically activate the matching profile

<a id="full-manual"></a>
## Full Manual

<a id="profiles"></a>
### Profiles

A **profile** is the core configuration that defines all input mappings for a specific game or application.

**Creating Profiles:**
- **From Window**: **Profiles** â†’ **New from Window...** â†’ Click target window (auto-fills process/window regex)
- **Empty Profile**: **Profiles** â†’ **New Empty** (manually configure process/window regex)

**Profile Settings:**
- **Name**: Display name in the profile dropdown
- **Process Name Regex**: Regular expression matching the executable name (e.g., `game\.exe` or `.*` for all)
- **Window Name Regex**: Regular expression matching the window title
- **Note**: At least one regex (process or window) must match for auto-activation. Empty regex matches everything.

<a id="settings-tab"></a>
#### Settings Tab

The **Settings** tab contains additional per-profile options.

**Hide controllers from games when active:**
- When checked, JoyMap uses the **HidHide** driver to hide all physical controllers referenced in this profile's triggers and axis bindings from other applications while the profile is active
- Games will no longer see the physical joystick/gamepad at all while the profile is active
- When the profile deactivates (window unfocused or profile switched), the physical controllers are automatically made visible again
- The checkbox is disabled if HidHide is not detected; install the driver from https://github.com/nefarius/HidHide/releases to enable it

**Why hiding is useful:**

The most obvious reason is to prevent duplicate inputs when JoyMap is emitting a virtual Xbox 360 controller: without hiding, the game would see both the physical joystick and the virtual Xbox controller and process both simultaneously.

A less obvious reason is that some games treat *any* connected controller as an Xbox controller regardless of what it actually is. This is rare, but when it occurs the game will attempt to read the physical joystick using the Xbox axis/button layout, producing scrambled or unintended input. Hiding the physical controller removes it from the game's view entirely, eliminating the scrambled input â€” leaving only whatever JoyMap actually provides (keyboard/mouse actions, a virtual Xbox controller if configured, or both).

**Profile Selection:**
- Use the **Profile:** dropdown to manually switch profiles
- Profiles auto-activate when you focus a matching window
- The **Delete** button permanently removes the current profile (cannot be undone)

**Auto-Activation Logic:**
- JoyMap continuously monitors the focused window
- When a window matches a profile's process/window regex, that profile becomes active
- Only one profile is active at a time (most recent match wins)

<a id="save-and-load"></a>
### Save and Load

**Auto-Save (Release Builds):**
- All changes immediately save to disk in Release configuration
- No manual save required

**Manual Save (Debug Builds):**
- **File** â†’ **Save (Debug Only)** to persist changes
- Allows testing without modifying saved configuration

**Storage Location:**
- Profiles: `[My Documents]\JoyMap\Profiles.json`
- Controller Families: `[My Documents]\JoyMap\ControllerFamilies.json`

**Startup Behavior:**
- JoyMap automatically loads all profiles and controller families on launch
- Profiles without events are automatically removed

<a id="controller-families"></a>
### Controller Families

Controller families allow treating different physical controllers as functionally identical.

**Default Behavior:**
- Triggers first read from the exact device instance they were recorded on
- If disconnected, fallback to **any** instance of the same product (Product GUID)

**Family Setup:**
1. **File** â†’ **Edit Controller Families...**
2. Right-click â†’ **Add Family** (or edit existing)
3. **Check** all controllers you want in this family
4. **OK** to save

**Family Rules:**
- Controllers must be connected to appear in the dialog
- Each controller can only be in **one** family
- Adding to a new family removes it from the previous one
- Changes persist immediately on dialog close

**Use Case Example:**
- Group "Logitech F310" and "Xbox 360 Controller" in a "Gamepad" family
- Triggers recorded on F310 will now also accept input from Xbox 360 Controller

<a id="global-statuses"></a>
### Global Statuses

**Global Statuses** are named boolean values that persist for the lifetime of a profile and can be referenced by ID in any combiner expression. They centralise stateful logic â€” such as "Walk Mode is active" or "Combat Mode is on" â€” so that many events and bindings can all react to the same state without duplicating conditions.

**Global Status List** (main window, **Global Statuses** tab):

| Column | Description |
|--------|-------------|
| **Name** | User-defined status name |
| **ID** | Auto-assigned identifier (G0, G1, ...) used in combiner expressions |
| **Mode** | How the status value is determined |
| **Active** | Live value: **True** / **False**, or **Suspended** |

<a id="global-status-modes"></a>
#### Global Status Modes

| Mode | Description |
|------|-------------|
| **Always True** | Permanently active; useful as a constant or placeholder |
| **Always False** | Permanently inactive |
| **True if Combiner** | Active whenever the trigger combiner evaluates to true |
| **False if Combiner** | Active whenever the trigger combiner evaluates to false (inverted) |
| **Toggle (Start Inactive)** | Starts inactive; flips state on each rising edge of the combiner (false â†’ true transition) |
| **Toggle (Start Active)** | Starts active; flips state on each rising edge of the combiner |

Toggle modes reset to their initial state each time the profile is loaded.

<a id="creatingediting-a-global-status"></a>
#### Creating/Editing a Global Status

**Dialog Sections:**

1. **Name**: Display name for this status
2. **Mode**: How the value is determined (see table above)
3. **Combiner** *(Combiner and Toggle modes only)*: Expression that drives the status â€” may reference local triggers (T0, T1, ...), other global statuses (G0, G1, ...), and mode entries (M0, M1, ...)
4. **Triggers** *(Combiner and Toggle modes only)*: Local trigger inputs available as T0, T1, ... in the combiner

**Adding Triggers:**
- **Right-click** â†’ **Pick/Add...**: Record a joystick/gamepad input
- **Right-click** â†’ **Add Key Trigger...**: Use a keyboard key as input (see [Key Triggers](#key-triggers))

**Context Menu (Global Status List):**
- **New...**: Create a new global status
- **Edit Selected...** (or double-click): Modify the selected status
- **Copy/Paste**: Clipboard operations
- **Delete**: Remove the selected status

<a id="referencing-global-statuses"></a>
#### Referencing Global Statuses

Reference a status in any combiner expression by its ID:

```
G0            Active when global status G0 is true
G0 && T0      Active when G0 is true AND trigger T0 fires
!G0           Active when G0 is false
G0 || G1      Active when either G0 or G1 is true
```

Global statuses are evaluated in declaration order; a status may reference statuses and mode entries declared before it, as well as all mode entries.

<a id="mode-groups"></a>
### Mode Groups

A **Mode Group** is a mutually exclusive set of named modes. Exactly one mode per group is active at any time. Activating a mode entry immediately deactivates all others in the same group. Mode entries are identified as M0, M1, ... and can be referenced in any combiner expression.

**Mode Group List** (main window, **Mode Groups** tab):

| Column | Description |
|--------|-------------|
| **Name** | User-defined group name |
| **ID** | Auto-assigned group identifier (MG0, MG1, ...) |
| **Active Mode** | Name of the currently active mode entry |

<a id="creatingediting-a-mode-group"></a>
#### Creating/Editing a Mode Group

**Dialog Sections:**

1. **Name**: Display name for this group
2. **Mode Entries**: List of all modes in this group
3. **Default Mode**: Which entry is active when the profile first loads

**Mode Entry List Columns:**

| Column | Description |
|--------|-------------|
| **ID** | Auto-assigned identifier (M0, M1, ...) used in combiner expressions |
| **Name** | User-defined mode name |
| **Trigger** | Summary of the trigger(s) that activate this mode |

**Context Menu (Mode Entry List):**
- **New Entry...**: Create a new mode entry
- **Edit...** (or double-click): Modify the selected entry
- **Delete**: Remove the entry
- **Set as Default**: Make this entry the startup default

<a id="mode-entry-dialog"></a>
#### Mode Entry Dialog

Each mode entry requires:
- **Name**: Display name
- **Triggers**: Physical or keyboard inputs that can activate this mode
- **Combiner**: Expression combining the triggers (same syntax as event combiners; supports T0, T1, ..., G0, G1, ...)

**Activation Rule:** A mode entry becomes active on each rising edge of its combiner (inactive â†’ active transition). It stays active until another entry in the same group fires. The active mode is shown in the Mode Group list's **Active Mode** column.

<a id="how-mode-switching-works"></a>
#### How Mode Switching Works

1. On profile load the **Default Mode** becomes active
2. Each polling cycle all entry combiners are evaluated
3. The first entry whose combiner transitions false â†’ true becomes the new active mode
4. The previously active mode deactivates immediately

<a id="referencing-modes-in-expressions"></a>
#### Referencing Modes in Expressions

Reference a mode entry by its numeric ID in any combiner expression:

```
M0              Active when mode entry M0 is the current active mode
M0 && T0        Active when mode 0 is active AND trigger T0 fires
M0 || M1        Active when either mode 0 or mode 1 is active
!M0             Active when mode 0 is NOT the active mode
G0 && M1        Active when global status G0 is true and mode M1 is active
```

Mode entries (M0, M1, ...) are available in all combiner expressions â€” event triggers, global status triggers, and other mode entry triggers. Mode group IDs (MG0, MG1, ...) are not used in expressions; always reference the individual entries.

<a id="key-triggers"></a>
#### Key Triggers

Any trigger list in JoyMap â€” event triggers, global status triggers, and mode entry triggers â€” can include a **keyboard key** as an input source in addition to joystick/gamepad inputs.

**Adding a Key Trigger:**
1. Right-click the trigger list â†’ **Add Key Trigger...**
2. Press the desired key in the key picker dialog
3. The trigger appears with **Device = Key** and the key name as the Input

**How It Works:**
- JoyMap installs a global low-level keyboard hook (WH_KEYBOARD_LL) that tracks physical key state system-wide, regardless of which window is focused
- Key is active (100%) while held, inactive (0%) when released; threshold is 50% so key held = trigger active
- Injected/synthetic key events are filtered out â€” only physical key presses are detected

**Use Cases:**
- Press a keyboard key to toggle a global status (e.g., F5 switches Walk â†” Run mode)
- Combine a keyboard key with a joystick axis: `T0 && T1` where T0 is a stick tilt and T1 is a keyboard modifier
- Use a keyboard key as the sole trigger for an event when no joystick button is convenient

<a id="xbox-controller-emulation"></a>
### Xbox Controller Emulation

JoyMap creates a **virtual Xbox 360 controller** that games see as a real gamepad.

**Prerequisites:**
- Install **ViGEmBus driver**: https://github.com/nefarius/ViGEmBus/releases
- Driver must be running (installed as Windows service)

<a id="xbox-axis-binding-tab"></a>
#### Xbox Axis Binding Tab

The **Xbox Axis Binding** tab maps physical joystick inputs to virtual Xbox 360 controller axes.

**Binding List Columns:**
- **Xbox Axis**: Target virtual axis (see list below)
- **Input**: Physical joystick axis/button(s) bound to this axis
- **Output**: Real-time output value; shows **0** when gated off by an enable switch, or **Suspended** if manually suspended
- **Enable Status**: Global status or mode entry gating this binding (empty = always active)

**Available Xbox Axes:**
| Xbox Axis | Description | Range |
|-----------|-------------|-------|
| Move Horizontal | Left stick X | -1 (left) to +1 (right) |
| Move Vertical | Left stick Y | -1 (down) to +1 (up) |
| Look Horizontal | Right stick X | -1 (left) to +1 (right) |
| Look Vertical | Right stick Y | -1 (down) to +1 (up) |
| Trigger Left | Left trigger (LT) | 0 (released) to 1 (fully pressed) |
| Trigger Right | Right trigger (RT) | 0 (released) to 1 (fully pressed) |

**Editing Bindings:**
- **Double-click** an axis or **Right-click** â†’ **Edit Selected...**
- **Context Menu**: Copy, Paste, Unbind, Suspend
- **Keyboard Shortcuts**: Ctrl+C (copy), Ctrl+V (paste), Delete (unbind), Ctrl+A (select all)

<a id="creatingediting-an-axis-binding"></a>
#### Creating/Editing an Axis Binding

**Binding Dialog Layout:**

1. **Axis Label** (top): Shows which Xbox axis you're configuring
2. **Input List**: All physical inputs bound to this axis

**Input List Columns:**
- **Device**: Physical joystick axis or button name (e.g., "TWCS Throttle / Slider")
- **Transform**: Transformation type (currently only "Linear")
- **Current**: Real-time input value after transformation

**Adding Inputs:**
1. **Right-click** â†’ **Add...** or use toolbar button
2. **Pick Device Input Dialog** opens
3. **Move** an axis or **press** a button to select it
4. Configure **DeadZone**, **Scale**, and **Translation**
5. **OK** to add

**Input Configuration:**

| Parameter | Description | Typical Range |
|-----------|-------------|---------------|
| **DeadZone** | Percentage near center/zero to ignore (reduces drift) | 5-15% |
| **Scale** | Output multiplier (larger = more sensitive) | 50% - 200% |
| **Translation** | Transformation algorithm (currently only "Linear") | Linear |

**Linear Transformation Math:**
- Input values in the deadzone range [-DeadZone, +DeadZone] map to 0
- Values outside the deadzone are scaled: output = input * Scale
- Final output is clamped to [-1, +1]

Example: With DeadZone=10% and Scale=100%:
- Input -1.0 â†’ Output -1.0
- Input -0.05 (within deadzone) â†’ Output 0
- Input +0.5 â†’ Output +0.5
- Input +1.0 â†’ Output +1.0

**Multiple Inputs (Combining):**
- **Multiple axes can feed one Xbox axis**
- Output uses the **largest absolute value** (preserving sign)
- Example: Bind throttle (0â†’1) and slider (-1â†’1) to Trigger Left; stronger input wins

<a id="enable-switch"></a>
#### Enable Switch

The **Enable Status** dropdown gates the entire binding through a global status or mode entry.

| Setting | Behaviour |
|---------|-----------|
| **(none)** | Binding is always active (default) |
| **G0 â€¦ Gn** | Binding outputs its value only while the selected global status is true; outputs 0 otherwise |
| **M0 â€¦ Mn** | Binding outputs its value only while the selected mode entry is the active mode; outputs 0 otherwise |

**Use Cases:**
- Bind a joystick axis to the virtual left stick, but only when "Flight Mode" (M0) is active; in all other modes the axis is ignored
- Gate a throttle binding on a global status toggle so it can be disabled with a key press
- Use the same physical axis for different virtual axes depending on the active mode by creating one binding per mode, each gated on a different mode entry

The **Output** column in the main window shows **0** when the enable switch is currently blocking the binding, making it easy to verify gating at a glance.

**Common Configurations:**

| Goal | DeadZone | Scale | Notes |
|------|----------|-------|-------|
| Reduce stick drift | 10-15% | 100% | Ignore small unintentional movements |
| Decrease sensitivity | 5% | 50% | Smaller scale = less sensitive (input*scale) |
| Increase sensitivity | 5% | 200% | Larger scale = more sensitive |
| Combine multiple axes | varies | 100% | Add both to same Xbox axis |

**Future Enhancements:**
- Additional transformation modes ([-1,1] â†” [0,1] conversion)
- Custom curve editor for non-linear response
- DeadZone and Scale will remain available for all transformation types

<a id="xbox-button-mapping-via-events"></a>
#### Xbox Button Mapping (via Events)

In the **Events** tab, actions can now target **Xbox controller buttons** in addition to keyboard/mouse keys.

**Available Xbox Buttons:**
- A, B, X, Y
- Back, Start, Guide
- ShoulderLeft (LB), ShoulderRight (RB)
- ThumbLeft (L3), ThumbRight (R3)

**Usage:**
1. Create/edit an event
2. Add an action â†’ **Pick...** button
3. Select from keyboard keys, mouse buttons, **or Xbox buttons**
4. When the event triggers, the virtual Xbox button will press/release

**Example Use Case:**
- Trigger: Joystick Button 5 (50-100%)
- Action: Press Xbox Button A
- Result: Physical button 5 acts as virtual Xbox A button

<a id="virtual-controller-lifecycle"></a>
#### Virtual Controller Lifecycle

- **Connects**: When a profile with â‰¥1 axis binding or Xbox button action becomes active
- **Disconnects**: When the profile stops (window unfocused or profile switched)
- **Games see**: Native Xbox 360 controller (no special drivers needed beyond ViGEmBus)

<a id="events"></a>
### Events

An **event** combines **triggers** (input conditions) and **actions** (outputs). When all trigger conditions are met, all actions execute.

**Execution Context:**
- Actions execute while the target game window is focused
- **OR** any non-JoyMap window if **Profiles** â†’ **Run Only when Game is Focused** is disabled

<a id="event-list"></a>
#### Event List

**Location**: Main window, **Events** tab

**List Columns:**
| Column | Description |
|--------|-------------|
| **Name** | User-defined event name |
| **Trigger(s)** | Summary of input conditions (e.g., "Button0, AxisY") |
| **Action(s)** | Summary of outputs (e.g., "Press Space, Hold W") |
| **Active** | Empty if inactive, **"A"** if currently triggered, **"Suspended"** if suspended |

**Context Menu Actions:**
- **New...**: Create new event
- **Edit Selected...** (or double-click): Modify event
- **Copy Selected** (Ctrl+C): Copy to clipboard
- **Copy All**: Copy all events
- **Paste Over**: Replace selected events with clipboard
- **Paste Insert** (Ctrl+V): Insert clipboard events at selection
- **Delete** (Del): Remove selected events
- **Move Selected Up/Down** (Ctrl+â†‘/â†“): Reorder events (visual only, doesn't affect behavior)
- **Select All** (Ctrl+A): Select all events
- **(Un)Suspend Selected**: Toggle event suspension (suspended events don't execute)

**Note**: Event order is cosmetic; all events evaluate independently.

<a id="event-dialog"></a>
#### Event Dialog

**Sections:**
1. **Name** (top): Event identifier
2. **Triggers** (middle): Input conditions
3. **Actions** (bottom): Outputs when triggered

**Workflow:**
- Changes only apply when you click **Update/Create**
- No undo within this dialog (use profile-level undo after closing)

<a id="triggers-section"></a>
#### Triggers Section

**Trigger List Columns:**
- **Label**: Auto-assigned identifier (T0, T1, T2, ...)
- **Device**: Joystick name
- **Axis/Button**: Input name
- **Status**: Empty if inactive, **"A"** if currently active

**Adding/Editing:**
- **Right-click** â†’ **Pick/Add...**: Record new trigger
- **Right-click** â†’ **Edit**: Modify existing trigger
- **Right-click** â†’ **Delete**: Remove trigger

**Trigger Combiner:**

Defines how multiple triggers combine:

| Mode | Behavior | Expression |
|------|----------|------------|
| **And** | All triggers must be active | `T0 && T1 && T2` |
| **Or** | At least one trigger active | `T0 \|\| T1 \|\| T2` |
| **Custom** | Boolean expression with &&, \|\|, !, () | `(T0 && T1) \|\| !T2` |

**Custom Expression Syntax:**
- **Operators**: `AND`/`&&`, `OR`/`||`, `NOT`/`!`
- **Grouping**: `(` `)` parentheses
- **Local identifiers**: T0, T1, T2, ... (trigger labels for this event/status/mode entry)
- **Global identifiers**: G0, G1, ... (global status IDs), M0, M1, ... (mode entry IDs)
- **Keywords**: `TRUE`, `FALSE` (always on/off)

**Examples:**
- `T0 AND T1`: Both triggers active
- `T0 OR T1 OR T2`: Any trigger active
- `(T0 && T1) || T2`: (T0 and T1) or T2
- `!(T0 || T1)`: Neither T0 nor T1 active
- `T0 AND NOT T1`: T0 active but not T1
- `M0 && T0`: Only fires in mode M0 and when T0 is active
- `G0 || T0`: Fires when global status G0 is true, or when T0 triggers

<a id="combiner-help-dialog--button"></a>
#### Combiner Help Dialog (? Button)

Every combiner field has a **?** button that opens the **Combiner Help Dialog** â€” an interactive reference and editor.

**Dialog Contents:**

1. **Local Inputs table** â€” The local triggers for this event/status/mode entry (T0, T1, ...) with Device, Axis/Key, and a live active indicator (â—)
2. **Global Inputs table** â€” All global statuses (G0, G1, ...) and mode entries (M0, M1, ...) with their names and live active indicators (â—)
3. **Expression editor** â€” Edit and validate the combiner expression with real-time error feedback
4. **Syntax reference** â€” Full operator and shorthand reference shown in the dialog

**Interacting with the dialog:**
- **Double-click** any row in either table to insert that identifier at the cursor position in the expression
- **Right-click** the local or global table for a quick-insert context menu
- **OK** applies the validated expression back to the combiner field; the button is only enabled when the expression is valid

The live active indicators update on a timer so you can watch inputs change while the dialog is open, making it easy to verify which identifiers to use.

<a id="trigger-dialog"></a>
#### Trigger Dialog

The Trigger Dialog has **two tabs**: **Range** (traditional threshold-based) and **Dither** (PWM duty-cycle).

**Common Fields:**
| Field | Description |
|-------|-------------|
| **Device** | Joystick/controller name |
| **Input** | Axis or button name |

<a id="range-tab-traditional-trigger"></a>
##### Range Tab (Traditional Trigger)

**Configuration:**
| Field | Description |
|-------|-------------|
| **Min %** | Minimum activation threshold (0-100%) |
| **Max %** | Maximum activation threshold (0-100%) |
| **Auto Release after (ms)** | Force inactive after X milliseconds (optional) |
| **Delay Release by (ms)** | Keep active for X milliseconds after input leaves range (optional) |

**Activation Logic:**
- Trigger is active when: `Min% â‰¤ input% â‰¤ Max%`
- Buttons: 0% (released), 100% (pressed)
- Axes: Normalized to 0-100% (or -100% to +100% for centered axes)

**Advanced Options:**

**Auto Release (Timeout):**
- Forces trigger to deactivate after X ms, even if input still in range
- Resets when input leaves and re-enters the range
- Use case: Single-shot activation (e.g., trigger once, then require re-trigger)

**Delay Release (Debounce):**
- Keeps trigger active for X ms after input leaves the range
- Use case: Smooth out jittery inputs or hold actions slightly longer
- **Combined with Auto Release**: Extends the auto-release timer

**Example Configurations:**

| Goal | Min % | Max % | Notes |
|------|-------|-------|-------|
| Button pressed | 50 | 100 | Standard button trigger |
| Button released | 0 | 50 | Inverted button trigger |
| Axis forward | 60 | 100 | Upper 40% of axis range |
| Axis centered | 45 | 55 | Â±5% around center |
| Full axis range | 0 | 100 | Always active (not useful alone) |

<a id="dither-tab-pwm-duty-cycle-trigger"></a>
##### Dither Tab (PWM Duty-Cycle Trigger)

The **Dither** trigger mode implements **Pulse Width Modulation (PWM)** using analog input values to control activation duty cycle. This converts analog axes into time-varying digital triggers.

**âš  Important: Match Game FPS**
- Dither frequency should align with your game's framerate
- Games typically poll inputs once per frame
- High dither frequency + low FPS = unpredictable behavior
- **Recommended**: Set frequency â‰¤ game FPS (e.g., 30Hz for 60 FPS games)
- **Actual FPS**. Generated frames do not work that way. Best disable frame generation while using this program

**Configuration:**

| Field | Description | Typical Range |
|-------|-------------|---------------|
| **Ramp Start %** | Input value where dithering begins (0-100%) | 10-50% |
| **Ramp Max %** | Input value where trigger is always active (0-100%) | 50-100% |
| **Frequency (Hz)** | Dither cycle rate (must consider game FPS) | 5-30 Hz |

**How Dither Works:**

The trigger oscillates on/off in a **repeating cycle**, with the duty cycle (percentage of time "on") determined by the input value:

1. **Input < Ramp Start**: Always **inactive** (0% duty cycle)
2. **Ramp Start â‰¤ Input < Ramp Max**: **Oscillating** activation
   - Duty Cycle = `(Input - RampStart) / (RampMax - RampStart)`
   - Higher input = longer "on" time per cycle
3. **Input â‰¥ Ramp Max**: Always **active** (100% duty cycle)

**Mathematical Formula:**

```
if input < RampStart:
    trigger = always false
else
    if input >= RampMax:
        trigger = always true
    else:
        normalizedValue = (input - RampStart) / (RampMax - RampStart)
        cyclePosition = (elapsedTime * Frequency) % 1.0
        trigger = (cyclePosition < normalizedValue)
```

**Visual Example (RampStart=20%, RampMax=80%, Frequency=10Hz):**

```
Input: 20% â”Œâ”€â”     â”Œâ”€â”     â”Œâ”€â”     (minimal on-time, ~0% duty)
           â””â”€â”˜â”€â”€â”€â”€â”€â””â”€â”˜â”€â”€â”€â”€â”€â””â”€â”˜â”€â”€â”€â”€â”€
Input: 50% â”Œâ”€â”€â”€â”€â”  â”Œâ”€â”€â”€â”€â”  â”Œâ”€â”€â”€â”€â”  (50% duty cycle)
           â””â”€â”€â”€â”€â”˜â”€â”€â””â”€â”€â”€â”€â”˜â”€â”€â””â”€â”€â”€â”€â”˜â”€â”€
Input: 80% â”Œâ”€â”€â”€â”€â”€â”€â”€â”€â”â”Œâ”€â”€â”€â”€â”€â”€â”€â”€â”â”Œâ”€â”€ (maximum on-time, 100% duty)
           â””â”€â”€â”€â”€â”€â”€â”€â”€â”˜â””â”€â”€â”€â”€â”€â”€â”€â”€â”˜â””â”€â”€
     0ms   50ms  100ms 150ms 200ms  (at 10Hz = 100ms cycle)
```

**Frequency Selection Guidelines:**

| Game FPS | Recommended Dither Frequency | Rationale |
|----------|----------------------------|-----------|
| 30 FPS | 10-15 Hz | 2-3 dither cycles per frame |
| 60 FPS | 15-30 Hz | 2-4 dither cycles per frame |
| 120 FPS | 30-60 Hz | 2-4 dither cycles per frame |
| 144+ FPS | 30-60 Hz | Diminishing returns above 60Hz |

**Why FPS Matters:**
- Games sample inputs at their render framerate
- If dither frequency >> game FPS: Game may miss state transitions
- If dither frequency << game FPS: Effect appears choppy/steppy
- **Optimal**: 2-4 dither cycles per game frame ensures smooth sampling


<a id="pick-axis-dialog"></a>
#### Pick Axis Dialog

**Behavior:**
- **Real-time recording**: Move axes or press buttons to detect them
- **Axis polarity**: Axes may appear twice (positive/negative directions)
- **Button persistence**: Buttons only appear after first press
- **Dialog reset**: Closing and reopening clears the recorded inputs

**Usage:**
1. Open dialog via **Pick...** button
2. Move/press the desired input
3. Select from the list
4. **OK** to confirm

<a id="actions-section"></a>
#### Actions Section

**Action List Columns:**
- **Name**: User-defined or auto-generated action name
- **Key/Button**: Target output (keyboard key, mouse button, or Xbox button)
- **Behavior**: "Hold", "Auto-fire X Hz", "t=[hold]/[gap]ms" (Timing mode), "Delayed", etc.

**Adding/Editing:**
- **Right-click** â†’ **Add...**: Create new action
- **Right-click** â†’ **Edit**: Modify existing action
- **Right-click** â†’ **Delete**: Remove action

<a id="action-dialog"></a>
#### Action Dialog

**Configuration:**
| Field | Description | Default |
|-------|-------------|---------|
| **Name** | Action identifier (auto-filled from key if blank) | - |
| **Key/Button** | Target output (keyboard/mouse/Xbox) | None |
| **Initial Delay (ms)** | Wait X ms before first activation | 0 |
| **Auto Trigger Frequency** | Enable frequency-based auto-fire (checkbox; mutually exclusive with Timing) | Off |
| **Frequency (Hz)** | Presses per second when using Frequency mode (consider game FPS) | 10 |
| **Auto Trigger Timing** | Enable timing-controlled hold/gap cycle (checkbox; mutually exclusive with Frequency) | Off |
| **Hold (ms)** | Duration key is held pressed per cycle when using Timing mode | 200 |
| **Gap (ms)** | Duration of gap between presses when using Timing mode | 200 |
| **Delay Start (ms)** | Hold first press for X ms before auto-fire or timing cycle begins | 0 |
| **Limit Auto-Triggers** | Maximum number of presses (checkbox; applies to both modes) | Off |
| **Limit Count** | Max presses (minimum useful value is **2**; see note below) | 3 |

**âš  Action Frequency & Game FPS:**
- Like dither triggers, action auto-fire should consider game FPS
- High auto-fire frequency in low-FPS games may cause missed inputs
- **Recommended**: Auto-fire frequency â‰¤ game FPS
- **Timing mode**: Hold/Gap cycle timing is wall-clock based and not FPS-dependent; use it when precise hold/gap durations matter more than a target press rate

**Behavior Modes:**

**1. Hold Mode (Auto Trigger OFF):**
- Press output when event activates
- Release output when event deactivates
- Use case: Simple key hold (e.g., W for forward movement)

**2. Auto-Fire Mode (Auto Trigger Frequency ON):**
- Rapidly press/release output at specified frequency
- Continues until event deactivates or limit reached
- Use case: Rapid-fire weapon, spam key press

**3. Timing Mode (Auto Trigger Timing ON):**
- Holds output for **Hold (ms)**, then releases for **Gap (ms)**, cycling continuously
- Total cycle time = Hold + Gap ms
- Cycle begins with the key held (initial press at activation); use Delay Start for a plain-hold phase before cycling begins
- Continues until event deactivates or limit reached
- Use case: Controlled rhythmic keypresses where exact hold/gap durations matter (e.g., hold 300ms, gap 100ms)

> **Note**: Auto Trigger Frequency and Auto Trigger Timing are **mutually exclusive** â€” checking one automatically unchecks the other. Delay Start and Limit apply to both modes.

**4. Delayed Auto-Fire/Timing:**
- Hold first press for "Delay Start" duration
- Then switch to auto-fire or timing cycle
- Use case: Charge attack (hold), then rapid combo

**5. Limited Auto-Fire/Timing:**
- Auto-fire or timing cycle exactly N times
- Last press is **held** until event deactivates
- Use case: Double jump with subsequent glide (press 2x, then hold)

> âš  **Limit = 1 caveat**: With a limit of 1 the key fires once after the first half-cycle interval and is then held for the remainder of the event, which is almost indistinguishable from Hold Mode. Set **Limit â‰¥ 2** for meaningful repetition.

**Key/Button Selection:**
- **Pick...** button opens picker dialog
- Shows **all** keyboard keys (A-Z, F1-F12, Shift, Ctrl, ...)
- Shows **mouse buttons** (Left, Right, Middle, X1, X2)
- Shows **Xbox buttons** (A, B, X, Y, Back, Start, Guide, LB, RB, L3, R3)

**Examples:**

| Configuration | Behavior |
|---------------|----------|
| Hold Space, no auto-fire | Holds Space while event active |
| Auto-fire 10 Hz, no delay | Presses Space 10Ã—/second while active |
| Auto-fire 5 Hz, delay 500ms | Holds Space 500ms, then presses 5Ã—/second |
| Auto-fire 10 Hz, limit 3 | Presses Space 3Ã— rapidly, holds 3rd press until deactivate |
| Timing: Hold=300ms, Gap=100ms | Key held 300ms, gap 100ms, repeating while active |
| Timing: Hold=200ms, Gap=200ms, delay 500ms | Holds Space 500ms, then cycles 200ms on / 200ms off |
| Timing: Hold=500ms, Gap=50ms, limit 3 | 3 slow presses (500ms held, 50ms gap each), last press held |

<a id="on-change-action"></a>
#### On-Change Action

The Action Dialog has a second mode â€” **On-Change** â€” available via its second tab. Instead of holding a key for the duration of the event, On-Change fires a brief key press on each edge transition of the event combiner.

**Configuration:**
| Field | Description | Default |
|-------|-------------|---------|
| **Name** | Action identifier | - |
| **Initial Delay (ms)** | Wait X ms before the first (rising) press fires | 0 |
| **Rising Key/Button** | Key pressed when the event activates (false â†’ true transition) | None |
| **Different Falling Key** | When checked, use a separate key for deactivation | Off |
| **Falling Key/Button** | Key pressed when the event deactivates (true â†’ false transition); only if **Different Falling Key** is checked | (same as rising) |
| **Press Duration (ms)** | How long each triggered key press is held before release | 100 |

**How it works:**
- On the **rising edge** (combiner transitions inactive â†’ active), the rising key is pressed for `Press Duration` ms and then released
- On the **falling edge** (combiner transitions active â†’ inactive), the falling key (or the same rising key if no separate falling key is set) is pressed for `Press Duration` ms and then released
- No key is held between edges; each transition produces exactly one brief press
- The `Initial Delay` postpones the first rising press; the falling press is never delayed

**Common use case:** A game that uses a single key to **toggle** between walk and run. Map that toggle key to an On-Change action: one press fires when the stick crosses the trigger threshold (rising edge), and one press fires when it falls back below it (falling edge).

> âš  **Reliability and resynchronization:** On-change triggers are of **low reliability** by nature. JoyMap fires key presses on transitions of its own combiner state, but has no visibility into the game's internal state â€” the game receives blind key presses with no acknowledgement. If any press is missed (during loading, a cutscene, a brief window-focus loss, or any in-game input suppression), JoyMap's edge timing and the game's actual toggle state will **diverge**. Every subsequent press will then toggle the wrong direction until the states are manually re-aligned.
>
> Resynchronization requires a deliberate player action â€” a full character stop, a manual key press, or any other action that forces the game back to a known state. Expect this to become necessary when using on-change triggers regularly.
>
> **Recommendation:** Check your game's settings for a **"hold to run"** (or "hold to walk", or any toggle â†’ hold equivalent) option. Most â€” though not all â€” games provide this. Switching to a hold-based control scheme eliminates all synchronization issues entirely: the physical input is held while the stick is pushed, and the game mirrors that state directly. If your game supports it, prefer **Hold Mode** over On-Change triggers.

<a id="undoredo"></a>
### Undo/Redo

**Availability**: All profile edits (except within dialogs)

**Shortcuts:**
- **Undo**: Ctrl+Z or **Edit** â†’ **Undo**
- **Redo**: Ctrl+Y or Ctrl+Shift+Z or **Edit** â†’ **Redo**

**Scope:**
- Per-profile history (switching profiles doesn't clear)
- Tracks: Event add/edit/delete/move, profile name/regex changes, axis binding changes
- **Not tracked**: Changes within open dialogs (only recorded on OK)

**Limitations:**
- Profile deletion cannot be undone (prompts for confirmation)
- History cleared on application restart

<a id="advanced-features"></a>
### Advanced Features

<a id="profile-suspension"></a>
#### Profile Suspension

**Profiles** â†’ **Run Only when Game is Focused** (checked by default):
- **Checked**: Actions only execute when the game window matching the profile is focused
- **Unchecked**: Actions execute when **any** non-JoyMap window is focused (allows global hotkeys)

**Individual Event/Binding Suspension:**
- Right-click event/binding â†’ **(Un)Suspend Selected**
- Suspended items shown in "Active" column as **"Suspended"**
- Use case: Temporarily disable specific events without deleting them

<a id="clipboard-operations"></a>
#### Clipboard Operations

**Events:**
- Copy/Paste events between profiles
- JSON format (can edit externally)
- Paste Over: Replace selected events (must have same count)
- Paste Insert: Insert at selection

**Xbox Bindings:**
- Copy/Paste axis bindings
- Paste over multiple axes at once

**Tip**: Use Copy All â†’ Paste into text editor to backup or share configurations

<a id="keyboard-shortcuts-summary"></a>
#### Keyboard Shortcuts Summary

**Global:**
- **Ctrl+Z**: Undo
- **Ctrl+Y** / **Ctrl+Shift+Z**: Redo
- **Ctrl+A**: Select All
- **Ctrl+C**: Copy Selected
- **Ctrl+V**: Paste
- **Delete**: Delete Selected

**Events Tab:**
- **Ctrl+N**: New event
- **Ctrl+â†‘**: Move Selected Up
- **Ctrl+â†“**: Move Selected Down
- **Double-click**: Edit event

**Xbox Tab:**
- **Double-click**: Edit axis binding

<a id="troubleshooting"></a>
## Troubleshooting

**"XBox controller created" does not appear in status bar:**
- Install ViGEmBus driver: https://github.com/nefarius/ViGEmBus/releases
- Restart Windows after driver installation
- Check Windows Device Manager â†’ "System Devices" â†’ "Virtual Gamepad Emulation Bus"

**Controller not detected:**
- Ensure controller is connected before launching JoyMap
- Check Windows **Game Controllers** (joy.cpl) to verify detection
- For HOTAS/throttles: May appear as "Supplemental" or "Flight" device type
- Try USB connection (not Bluetooth) to rule out connection issues

**Profile doesn't auto-activate:**
- Check process/window regex is correct (use **Pick...** to recapture)
- Test regex: Empty regex matches all; `.*` also matches all
- Ensure **at least one** regex (process OR window) is non-empty and matches
- Check **Profiles** â†’ **Run Only when Game is Focused** if testing outside game

**Actions not executing:**
- Verify event "Active" column shows **"A"** when triggered
- Check trigger Min/Max percentages match your input range
- Test with simple trigger (button 50-100%) and action (Press A) first
- Ensure profile is active (shown in dropdown) and game window is focused

**Xbox axis not responding in game:**
- Check "Output" column shows non-zero values when moving physical axis
- Verify DeadZone isn't too high (try 0% to test)
- Check Scale value (100% is neutral)
- Ensure ViGEmBus driver is running (restart if necessary)
- Test with different game (some games don't support virtual controllers)

**Dither trigger not working as expected:**
- **Flickering "A" in Status column**: Dither is working; check game responsiveness
- **Always on/off**: Input outside Ramp range; adjust RampStart/RampMax
- **Erratic behavior**: Frequency too high for game FPS; reduce to â‰¤ game FPS
- **Choppy effect**: Frequency too low; increase (but stay under game FPS)
- **Use case**: Check your game's FPS setting and set dither frequency to half of that value

<a id="tips--best-practices"></a>
## Tips & Best Practices

**Profile Organization:**
- Name profiles clearly (e.g., "Star Citizen - HOTAS", "Elite Dangerous - Dual Stick")
- Use specific process/window regex to avoid conflicts (e.g., `EliteDangerous64\.exe` not `.*`)

**Event Design:**
- Name events descriptively (e.g., "Boost - Hold Shift", "Fire Primary - Auto 10Hz")
- Start simple: Single trigger + single action, then add complexity
- Use trigger combiners for advanced logic (e.g., "Fire only when throttle > 50%")

**Trigger Tuning:**
- Default 50-100% works for most buttons
- Axes: Test ranges by watching trigger "Status" column
- Add deadzone (45-55%) for centered axes to avoid constant triggering
- **Dither**: Always check your game's FPS first; set frequency = FPS / 2 as starting point

**Action Timing:**
- Initial Delay: Use for sequential macros (e.g., 0ms, 200ms, 400ms delays)
- Auto-fire: Match to game FPS (e.g., 30Hz action for 60 FPS game)
- Auto-Trigger Timing: Use when exact hold/gap durations matter more than a press rate (e.g., hold 300ms, gap 100ms); not FPS-dependent
- Limited triggers: Great for combo sequences (e.g., 3Ã— rapid tap, then hold)
- **FPS-aware**: Lower auto-fire frequency in low-FPS games to prevent missed inputs; Timing mode is wall-clock based and unaffected by game FPS

**Xbox Binding:**
- Start with one axis at a time to verify behavior
- Use 10-15% deadzone for analog sticks
- Combine multiple physical axes (e.g., pedals + twist) to one virtual axis

**Controller Families:**
- Group controllers you frequently swap (e.g., home/travel setups)
- Don't over-group; keep distinct controller types separate

**Backup:**
- Copy `[My Documents]\JoyMap\` folder periodically
- Use Copy All â†’ Paste to text file for configuration snapshots

**FPS-Specific Tuning:**
- **30 FPS games**: Use 10-15 Hz dither/auto-fire
- **60 FPS games**: Use 20-30 Hz dither/auto-fire
- **120+ FPS games**: Use 30-60 Hz (diminishing returns above 60)
- **Variable FPS**: Use conservative values (lowest expected FPS / 2)

<a id="technical-details"></a>
## Technical Details

**DirectInput Support:**
- Polls all DirectInput devices (Gamepad, Joystick, Flight, Driving, Supplemental)
- Reads up to 64 buttons per device
- Supports axes: X, Y, Z, RotationX, RotationY, RotationZ, Slider(s), POV(s)

**Virtual Xbox 360 Controller:**
- ViGEmBus driver: https://github.com/nefarius/ViGEmBus
- Exposes: 2 analog sticks, 2 triggers, D-pad, 10 buttons, Guide button
- Latency: <5ms typical (depends on DirectInput poll rate)

**Dither Implementation:**
- High-resolution time-based PWM using DateTime.UtcNow
- Elapsed time = UtcNow - trigger creation time
- Cycle position calculated per evaluation: (elapsed_seconds * Frequency) % 1.0
- Duty cycle = (input - RampStart) / (RampMax - RampStart)
- Thread-safe: Each trigger maintains independent state

**File Format:**
- JSON with UTF-8 encoding
- Enums serialized as strings (e.g., "Button0", "MoveHorizontal")
- Backwards-compatible reader (old Keys-only actions load as KeyOrButton)
- Trigger serialization: Either `Range` or `Dither` object (mutually exclusive)

**Platform:**
- Windows 10/11 (64-bit recommended)
- .NET 10 runtime
- Requires Windows with DirectInput support

<a id="known-limitations"></a>
## Known Limitations

- Cannot read XInput-only controllers (e.g., Xbox Wireless Controller) as input (only output via emulation)
- No mouse movement emulation (only button clicks)
- Profile auto-activation requires focused window; no global profile override
- Event order in UI is cosmetic; cannot set execution priority
- Virtual Xbox controller Guide button may not work in all games
- No per-action undo within event/trigger/action dialogs
- **Dither/auto-fire frequency limited by game FPS**: High frequencies (>60Hz) may cause unpredictable behavior in low-FPS games
- **No frame-sync**: Dither timing is wall-clock based, not frame-synchronized
- **Auto-trigger Limit = 1 behaves like Hold Mode**: A limit of 1 fires the key once after the first half-cycle interval and then holds it, making the auto-cycle option effectively inactive; use Limit â‰¥ 2 for actual repetition

<a id="credits"></a>
## Credits

**Assets:**
- Icon: [Joystick free icon](https://www.flaticon.com/free-icon/joystick_12585353?term=joystick&page=1&position=15&origin=tag&related_id=12585353) from Flaticon

**Libraries:**
- [ViGEmClient](https://github.com/nefarius/ViGEmClient) - Virtual gamepad interface
- [SharpDX.DirectInput](https://github.com/sharpdx/SharpDX) - DirectInput wrapper

**License:**
See LICENSE file for details.

---

**Version:** Check **Help** â†’ **About...** for current version number

