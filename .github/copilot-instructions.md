# Copilot Instructions

## Project Guidelines
- Prefer private properties over fields in C# code.
- Before completing any task, perform a clean (non-incremental) rebuild, and fix all warnings and errors that can be fixed.

## JoyMap-Specific Notes
- The log file is at: [app base directory]\joymap.log — typically `E:\Code\CSProjects\JoyMap\bin\Release\net10.0-windows\joymap.log`. Read it with: `Get-Content "E:\Code\CSProjects\JoyMap\bin\Release\net10.0-windows\joymap.log"`
- NEVER place copilot-instructions.md in the user home directory. Always use the repo's `.github\copilot-instructions.md`.