# JoyMap

## Introduction

JoyMap is a powerful Joystick-to-Keyboard/Mouse/Xbox mapping program that extends beyond tools like JoyToKey with advanced features for complex input routing and profile management.

**Key Features:**

1. **Multi-trigger Events**: Each event can use any number of joystick inputs, including those already used by other events
2. **Independent Axis Ranges**: Every trigger on every event has independent min/max thresholds with fraction precision (e.g., 12.34567%)
3. **Complex Trigger Logic**: Simple and complex boolean expressions (AND, OR, NOT, parentheses) with trigger labels (T0, T1, ...)
4. **Multiple Actions per Event**: Each action can specify delays, auto-fire frequency, and repetition limits
5. **Smart Device Mapping**: Controllers mapped by Product GUID → Instance GUID with automatic fallback to any instance of the same product
6. **Profile Auto-switching**: Automatically activates profiles based on focused window process/title (regex)
7. **Undo/Redo Support**: Full undo/redo history for profile editing with auto-save in Release builds
8. **Controller Families**: Group different controller models to treat them as interchangeable
9. **Virtual Xbox 360 Controller**: Built-in Xbox controller emulation via ViGEmBus driver

## Installation

1. Download and install the **ViGEmBus driver** from https://github.com/nefarius/ViGEmBus/releases (required for Xbox controller emulation)
2. Launch JoyMap.exe
3. Connect your joystick/gamepad via USB or Bluetooth

## Quick Start

1. **Create a Profile**: 
   - Launch your game
   - In JoyMap: **Profiles** → **New from Window...**
   - Click the game window to capture its process/window name
   
2. **Add Events**:
   - Right-click the event list → **New ...**
   - Add triggers (joystick inputs) and actions (keyboard/mouse/Xbox buttons)
   
3. **Automatic Activation**: Focus your game window and JoyMap will automatically activate the matching profile

## Full Manual

### Profiles

A **profile** is the core configuration that defines all input mappings for a specific game or application.

**Creating Profiles:**
- **From Window**: **Profiles** → **New from Window...** → Click target window (auto-fills process/window regex)
- **Empty Profile**: **Profiles** → **New Empty** (manually configure process/window regex)

**Profile Settings:**
- **Name**: Display name in the profile dropdown
- **Process Name Regex**: Regular expression matching the executable name (e.g., `game\.exe` or `.*` for all)
- **Window Name Regex**: Regular expression matching the window title
- **Note**: At least one regex (process or window) must match for auto-activation. Empty regex matches everything.

**Profile Selection:**
- Use the **Profile:** dropdown to manually switch profiles
- Profiles auto-activate when you focus a matching window
- The **Delete** button permanently removes the current profile (cannot be undone)

**Auto-Activation Logic:**
- JoyMap continuously monitors the focused window
- When a window matches a profile's process/window regex, that profile becomes active
- Only one profile is active at a time (most recent match wins)

### Save and Load

**Auto-Save (Release Builds):**
- All changes immediately save to disk in Release configuration
- No manual save required

**Manual Save (Debug Builds):**
- **File** → **Save (Debug Only)** to persist changes
- Allows testing without modifying saved configuration

**Storage Location:**
- Profiles: `[My Documents]\JoyMap\Profiles.json`
- Controller Families: `[My Documents]\JoyMap\ControllerFamilies.json`

**Startup Behavior:**
- JoyMap automatically loads all profiles and controller families on launch
- Profiles without events are automatically removed

### Controller Families

Controller families allow treating different physical controllers as functionally identical.

**Default Behavior:**
- Triggers first read from the exact device instance they were recorded on
- If disconnected, fallback to **any** instance of the same product (Product GUID)

**Family Setup:**
1. **File** → **Edit Controller Families...**
2. Right-click → **Add Family** (or edit existing)
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

### Xbox Controller Emulation

JoyMap creates a **virtual Xbox 360 controller** that games see as a real gamepad.

**Prerequisites:**
- Install **ViGEmBus driver**: https://github.com/nefarius/ViGEmBus/releases
- Driver must be running (installed as Windows service)

#### Xbox Axis Binding Tab

The **Xbox Axis Binding** tab maps physical joystick inputs to virtual Xbox 360 controller axes.

**Binding List Columns:**
- **Xbox Axis**: Target virtual axis (see list below)
- **Input**: Physical joystick axis/button(s) bound to this axis
- **Output**: Real-time output value (only updates when JoyMap is focused)

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
- **Double-click** an axis or **Right-click** → **Edit Selected...**
- **Context Menu**: Copy, Paste, Unbind, Suspend
- **Keyboard Shortcuts**: Ctrl+C (copy), Ctrl+V (paste), Delete (unbind), Ctrl+A (select all)

#### Creating/Editing an Axis Binding

**Binding Dialog Layout:**

1. **Axis Label** (top): Shows which Xbox axis you're configuring
2. **Input List**: All physical inputs bound to this axis

**Input List Columns:**
- **Device**: Physical joystick axis or button name (e.g., "TWCS Throttle / Slider")
- **Transform**: Transformation type (currently only "Linear")
- **Current**: Real-time input value after transformation

**Adding Inputs:**
1. **Right-click** → **Add...** or use toolbar button
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
- Input -1.0 → Output -1.0
- Input -0.05 (within deadzone) → Output 0
- Input +0.5 → Output +0.5
- Input +1.0 → Output +1.0

**Multiple Inputs (Combining):**
- **Multiple axes can feed one Xbox axis**
- Output uses the **largest absolute value** (preserving sign)
- Example: Bind throttle (0→1) and slider (-1→1) to Trigger Left; stronger input wins

**Common Configurations:**

| Goal | DeadZone | Scale | Notes |
|------|----------|-------|-------|
| Reduce stick drift | 10-15% | 100% | Ignore small unintentional movements |
| Decrease sensitivity | 5% | 50% | Smaller scale = less sensitive (input*scale) |
| Increase sensitivity | 5% | 200% | Larger scale = more sensitive |
| Combine multiple axes | varies | 100% | Add both to same Xbox axis |

**Future Enhancements:**
- Additional transformation modes ([-1,1] ↔ [0,1] conversion)
- Custom curve editor for non-linear response
- DeadZone and Scale will remain available for all transformation types

#### Xbox Button Mapping (via Events)

In the **Events** tab, actions can now target **Xbox controller buttons** in addition to keyboard/mouse keys.

**Available Xbox Buttons:**
- A, B, X, Y
- Back, Start, Guide
- ShoulderLeft (LB), ShoulderRight (RB)
- ThumbLeft (L3), ThumbRight (R3)

**Usage:**
1. Create/edit an event
2. Add an action → **Pick...** button
3. Select from keyboard keys, mouse buttons, **or Xbox buttons**
4. When the event triggers, the virtual Xbox button will press/release

**Example Use Case:**
- Trigger: Joystick Button 5 (50-100%)
- Action: Press Xbox Button A
- Result: Physical button 5 acts as virtual Xbox A button

#### Virtual Controller Lifecycle

- **Connects**: When a profile with ≥1 axis binding or Xbox button action becomes active
- **Disconnects**: When the profile stops (window unfocused or profile switched)
- **Games see**: Native Xbox 360 controller (no special drivers needed beyond ViGEmBus)

### Events

An **event** combines **triggers** (input conditions) and **actions** (outputs). When all trigger conditions are met, all actions execute.

**Execution Context:**
- Actions execute while the target game window is focused
- **OR** any non-JoyMap window if **Profiles** → **Run Only when Game is Focused** is disabled

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
- **Move Selected Up/Down** (Ctrl+↑/↓): Reorder events (visual only, doesn't affect behavior)
- **Select All** (Ctrl+A): Select all events
- **(Un)Suspend Selected**: Toggle event suspension (suspended events don't execute)

**Note**: Event order is cosmetic; all events evaluate independently.

#### Event Dialog

**Sections:**
1. **Name** (top): Event identifier
2. **Triggers** (middle): Input conditions
3. **Actions** (bottom): Outputs when triggered

**Workflow:**
- Changes only apply when you click **Update/Create**
- No undo within this dialog (use profile-level undo after closing)

#### Triggers Section

**Trigger List Columns:**
- **Label**: Auto-assigned identifier (T0, T1, T2, ...)
- **Device**: Joystick name
- **Axis/Button**: Input name
- **Status**: Empty if inactive, **"A"** if currently active

**Adding/Editing:**
- **Right-click** → **Pick/Add...**: Record new trigger
- **Right-click** → **Edit**: Modify existing trigger
- **Right-click** → **Delete**: Remove trigger

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
- **Identifiers**: T0, T1, T2, ... (trigger labels)
- **Keywords**: `TRUE`, `FALSE` (always on/off)

**Examples:**
- `T0 AND T1`: Both triggers active
- `T0 OR T1 OR T2`: Any trigger active
- `(T0 && T1) || T2`: (T0 and T1) or T2
- `!(T0 || T1)`: Neither T0 nor T1 active
- `T0 AND NOT T1`: T0 active but not T1

#### Trigger Dialog

The Trigger Dialog has **two tabs**: **Range** (traditional threshold-based) and **Dither** (PWM duty-cycle).

**Common Fields:**
| Field | Description |
|-------|-------------|
| **Device** | Joystick/controller name |
| **Input** | Axis or button name |

##### Range Tab (Traditional Trigger)

**Configuration:**
| Field | Description |
|-------|-------------|
| **Min %** | Minimum activation threshold (0-100%) |
| **Max %** | Maximum activation threshold (0-100%) |
| **Auto Release after (ms)** | Force inactive after X milliseconds (optional) |
| **Delay Release by (ms)** | Keep active for X milliseconds after input leaves range (optional) |

**Activation Logic:**
- Trigger is active when: `Min% ≤ input% ≤ Max%`
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
| Axis centered | 45 | 55 | ±5% around center |
| Full axis range | 0 | 100 | Always active (not useful alone) |

##### Dither Tab (PWM Duty-Cycle Trigger)

The **Dither** trigger mode implements **Pulse Width Modulation (PWM)** using analog input values to control activation duty cycle. This converts analog axes into time-varying digital triggers.

**⚠ Important: Match Game FPS**
- Dither frequency should align with your game's framerate
- Games typically poll inputs once per frame
- High dither frequency + low FPS = unpredictable behavior
- **Recommended**: Set frequency ≤ game FPS (e.g., 30Hz for 60 FPS games)
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
2. **Ramp Start ≤ Input < Ramp Max**: **Oscillating** activation
   - Duty Cycle = `(Input - RampStart) / (RampMax - RampStart)`
   - Higher input = longer "on" time per cycle
3. **Input ≥ Ramp Max**: Always **active** (100% duty cycle)

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
Input: 20% ┌─┐     ┌─┐     ┌─┐     (minimal on-time, ~0% duty)
           └─┘─────└─┘─────└─┘─────
Input: 50% ┌────┐  ┌────┐  ┌────┐  (50% duty cycle)
           └────┘──└────┘──└────┘──
Input: 80% ┌────────┐┌────────┐┌── (maximum on-time, 100% duty)
           └────────┘└────────┘└──
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

#### Actions Section

**Action List Columns:**
- **Name**: User-defined or auto-generated action name
- **Key/Button**: Target output (keyboard key, mouse button, or Xbox button)
- **Behavior**: "Hold", "Auto-fire X Hz", "Delayed", etc.

**Adding/Editing:**
- **Right-click** → **Add...**: Create new action
- **Right-click** → **Edit**: Modify existing action
- **Right-click** → **Delete**: Remove action

#### Action Dialog

**Configuration:**
| Field | Description | Default |
|-------|-------------|---------|
| **Name** | Action identifier (auto-filled from key if blank) | - |
| **Key/Button** | Target output (keyboard/mouse/Xbox) | None |
| **Initial Delay (ms)** | Wait X ms before first activation | 0 |
| **Auto Trigger Frequency** | Enable auto-fire (checkbox) | Off |
| **Frequency (Hz)** | Presses per second (consider game FPS) | 10 |
| **Delay Start (ms)** | Hold first press for X ms before auto-fire | 0 |
| **Limit Auto-Triggers** | Maximum number of presses (checkbox) | Off |
| **Limit Count** | Max presses | 3 |

**⚠ Action Frequency & Game FPS:**
- Like dither triggers, action auto-fire should consider game FPS
- High auto-fire frequency in low-FPS games may cause missed inputs
- **Recommended**: Auto-fire frequency ≤ game FPS

**Behavior Modes:**

**1. Hold Mode (Auto Trigger OFF):**
- Press output when event activates
- Release output when event deactivates
- Use case: Simple key hold (e.g., W for forward movement)

**2. Auto-Fire Mode (Auto Trigger ON):**
- Rapidly press/release output at specified frequency
- Continues until event deactivates or limit reached
- Use case: Rapid-fire weapon, spam key press

**3. Delayed Auto-Fire:**
- Hold first press for "Delay Start" duration
- Then switch to auto-fire
- Use case: Charge attack (hold), then rapid combo (auto-fire)

**4. Limited Auto-Fire:**
- Auto-fire exactly N times
- Last press is **held** until event deactivates
- Use case: Double jump with subsequent glide (press 2x, then hold)

**Key/Button Selection:**
- **Pick...** button opens picker dialog
- Shows **all** keyboard keys (A-Z, F1-F12, Shift, Ctrl, ...)
- Shows **mouse buttons** (Left, Right, Middle, X1, X2)
- Shows **Xbox buttons** (A, B, X, Y, Back, Start, Guide, LB, RB, L3, R3)

**Examples:**

| Configuration | Behavior |
|---------------|----------|
| Hold Space, no auto-fire | Holds Space while event active |
| Auto-fire 10 Hz, no delay | Presses Space 10x/second while active |
| Auto-fire 5 Hz, delay 500ms | Holds Space 500ms, then presses 5x/second |
| Auto-fire 10 Hz, limit 3 | Presses Space 3x rapidly, holds 3rd press until deactivate |

### Undo/Redo

**Availability**: All profile edits (except within dialogs)

**Shortcuts:**
- **Undo**: Ctrl+Z or **Edit** → **Undo**
- **Redo**: Ctrl+Y or Ctrl+Shift+Z or **Edit** → **Redo**

**Scope:**
- Per-profile history (switching profiles doesn't clear)
- Tracks: Event add/edit/delete/move, profile name/regex changes, axis binding changes
- **Not tracked**: Changes within open dialogs (only recorded on OK)

**Limitations:**
- Profile deletion cannot be undone (prompts for confirmation)
- History cleared on application restart

### Advanced Features

#### Profile Suspension

**Profiles** → **Run Only when Game is Focused** (checked by default):
- **Checked**: Actions only execute when the game window matching the profile is focused
- **Unchecked**: Actions execute when **any** non-JoyMap window is focused (allows global hotkeys)

**Individual Event/Binding Suspension:**
- Right-click event/binding → **(Un)Suspend Selected**
- Suspended items shown in "Active" column as **"Suspended"**
- Use case: Temporarily disable specific events without deleting them

#### Clipboard Operations

**Events:**
- Copy/Paste events between profiles
- JSON format (can edit externally)
- Paste Over: Replace selected events (must have same count)
- Paste Insert: Insert at selection

**Xbox Bindings:**
- Copy/Paste axis bindings
- Paste over multiple axes at once

**Tip**: Use Copy All → Paste into text editor to backup or share configurations

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
- **Ctrl+↑**: Move Selected Up
- **Ctrl+↓**: Move Selected Down
- **Double-click**: Edit event

**Xbox Tab:**
- **Double-click**: Edit axis binding

## Troubleshooting

**"XBox controller created" does not appear in status bar:**
- Install ViGEmBus driver: https://github.com/nefarius/ViGEmBus/releases
- Restart Windows after driver installation
- Check Windows Device Manager → "System Devices" → "Virtual Gamepad Emulation Bus"

**Controller not detected:**
- Ensure controller is connected before launching JoyMap
- Check Windows **Game Controllers** (joy.cpl) to verify detection
- For HOTAS/throttles: May appear as "Supplemental" or "Flight" device type
- Try USB connection (not Bluetooth) to rule out connection issues

**Profile doesn't auto-activate:**
- Check process/window regex is correct (use **Pick...** to recapture)
- Test regex: Empty regex matches all; `.*` also matches all
- Ensure **at least one** regex (process OR window) is non-empty and matches
- Check **Profiles** → **Run Only when Game is Focused** if testing outside game

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
- **Erratic behavior**: Frequency too high for game FPS; reduce to ≤ game FPS
- **Choppy effect**: Frequency too low; increase (but stay under game FPS)
- **Use case**: Check your game's FPS setting and set dither frequency to half of that value

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
- Limited triggers: Great for combo sequences (e.g., 3x rapid tap, then hold)
- **FPS-aware**: Lower action frequency in low-FPS games to prevent missed inputs

**Xbox Binding:**
- Start with one axis at a time to verify behavior
- Use 10-15% deadzone for analog sticks
- Combine multiple physical axes (e.g., pedals + twist) to one virtual axis

**Controller Families:**
- Group controllers you frequently swap (e.g., home/travel setups)
- Don't over-group; keep distinct controller types separate

**Backup:**
- Copy `[My Documents]\JoyMap\` folder periodically
- Use Copy All → Paste to text file for configuration snapshots

**FPS-Specific Tuning:**
- **30 FPS games**: Use 10-15 Hz dither/auto-fire
- **60 FPS games**: Use 20-30 Hz dither/auto-fire
- **120+ FPS games**: Use 30-60 Hz (diminishing returns above 60)
- **Variable FPS**: Use conservative values (lowest expected FPS / 2)

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

## Known Limitations

- Cannot read XInput-only controllers (e.g., Xbox Wireless Controller) as input (only output via emulation)
- No mouse movement emulation (only button clicks)
- Profile auto-activation requires focused window; no global profile override
- Event order in UI is cosmetic; cannot set execution priority
- Virtual Xbox controller Guide button may not work in all games
- No per-action undo within event/trigger/action dialogs
- **Dither/auto-fire frequency limited by game FPS**: High frequencies (>60Hz) may cause unpredictable behavior in low-FPS games
- **No frame-sync**: Dither timing is wall-clock based, not frame-synchronized

## Credits

**Assets:**
- Icon: [Joystick free icon](https://www.flaticon.com/free-icon/joystick_12585353?term=joystick&page=1&position=15&origin=tag&related_id=12585353) from Flaticon

**Libraries:**
- [ViGEmClient](https://github.com/nefarius/ViGEmClient) - Virtual gamepad interface
- [SharpDX.DirectInput](https://github.com/sharpdx/SharpDX) - DirectInput wrapper

**License:**
See LICENSE file for details.

---

**Version:** Check **Help** → **About...** for current version number

