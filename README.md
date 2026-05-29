# FileMerger

A simple and efficient command-line tool to merge source code files by their file extensions.

## Features

- Scans a directory recursively for common source code files
- Supports multiple file types (e.g. `.cs`, `.js`, `.ts`, `.java`, `.cpp`, `.h`, `.py`)
- Case-insensitive handling of file extensions
- Groups files by extension and creates merged output files:
  - `merged.cs`
  - `merged.js`
  - etc.
- Overwrites existing merged files
- Excludes:
  - Files starting with `merged.*`
  - Paths containing `debug` or `release` (case-insensitive)
- Outputs relative file paths for readability
- Each file section is clearly marked:

```text
// ===== File: relative/path/to/file.cs =====