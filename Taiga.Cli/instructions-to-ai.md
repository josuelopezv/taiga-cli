# Instructions for AI LLMs: Using Taiga CLI as MCP Server Replacement

This document provides instructions for AI Large Language Models (LLMs) to use the Taiga CLI tool as a replacement for an MCP (Model Context Protocol) server when interacting with Taiga project management systems.

## Overview

The Taiga CLI (`taiga-cli`) is a command-line interface that provides comprehensive access to Taiga project management functionality. Instead of using an MCP server, you can execute CLI commands directly and parse their output to interact with Taiga.

## Authentication

Before performing any operations, the user must be authenticated. You should check if authentication is required and guide the user accordingly.

**Login Command:**
```bash
taiga auth login -u <username> -p <password>
```

**For self-hosted instances:**
```bash
taiga auth login -u <username> -p <password> -a https://taiga.mycompany.com
```

**Logout Command:**
```bash
taiga auth logout
```

**Check Current User:**
```bash
taiga user me
```

## Available Commands

### Projects
- `taiga project list` - List all projects
- `taiga project get <project-id>` - Get project details
- `taiga project stats <project-id>` - Get project statistics
- `taiga project memberships <project-id>` - List project memberships
- `taiga project roles <project-id>` - List project roles

### User Stories
- `taiga userstory list [-p <project-id>]` - List user stories (optionally filtered by project)
- `taiga userstory get <story-id> [-p <project-id>]` - Get user story details
- `taiga userstory history <story-id>` - Get user story history
- `taiga userstory comments <story-id>` - Get user story comments
- `taiga userstory attachments <story-id>` - List user story attachments
- `taiga userstory create -p <project-id> -t <subject> [-d <description>] [-s <status-id>] [-a <user-id>] [-m <milestone-id>]` - Create a new user story
- `taiga userstory edit <story-id> [-p <project-id>] [-t <subject>] [-d <description>] [-s <status-id>] [-a <user-id>] [-m <milestone-id>]` - Edit a user story

### Tasks
- `taiga task list [-p <project-id>] [-u <user-story-id>]` - List tasks (optionally filtered by project or user story)
- `taiga task get <task-id> [-p <project-id>]` - Get task details

### Issues
- `taiga issue list [-p <project-id>]` - List issues (optionally filtered by project)
- `taiga issue get <issue-id> [-p <project-id>]` - Get issue details

### Epics
- `taiga epic list [-p <project-id>]` - List epics (optionally filtered by project)
- `taiga epic get <epic-id> [-p <project-id>]` - Get epic details
- `taiga epic userstories <epic-id>` - Get epic user stories

### Milestones
- `taiga milestone list [-p <project-id>]` - List milestones (optionally filtered by project)
- `taiga milestone get <milestone-id>` - Get milestone details
- `taiga milestone stats <milestone-id>` - Get milestone statistics
- `taiga milestone userstories <milestone-id>` - Get milestone user stories

### Wiki
- `taiga wiki list -p <project-id>` - List wiki pages for a project
- `taiga wiki get <wiki-id>` - Get wiki page details
- `taiga wiki history <wiki-id>` - Get wiki page history

### Search
- `taiga search <query> [-p <project-id>]` - Full-text search (optionally within a project)

### Notifications
- `taiga notification list` - List all notifications
- `taiga notification get <notification-id>` - Get notification details
- `taiga notification unread` - Get unread notifications count

### Timeline
- `taiga timeline project <project-id>` - Get project timeline
- `taiga timeline user <user-id>` - Get user timeline

### Users
- `taiga user me` - Get current user information
- `taiga user get <user-id>` - Get user by ID
- `taiga user list [-p <project-id>]` - List users (optionally filtered by project)

### Status & Configuration
- `taiga status severities [-p <project-id>]` - List severities
- `taiga status priorities [-p <project-id>]` - List priorities
- `taiga status issue-statuses [-p <project-id>]` - List issue statuses
- `taiga status task-statuses [-p <project-id>]` - List task statuses
- `taiga status userstory-statuses [-p <project-id>]` - List user story statuses

### Webhooks
- `taiga webhook list -p <project-id>` - List webhooks for a project
- `taiga webhook get <webhook-id>` - Get webhook details

## Usage Pattern for AI LLMs

### 1. Execute Commands
When you need to interact with Taiga, execute the appropriate CLI command using the system's command execution capability. The CLI outputs structured text that you can parse.

### 2. Parse Output
The CLI outputs human-readable text. Parse the output to extract:
- IDs (project IDs, story IDs, task IDs, etc.)
- Status information
- Lists of items
- Details about specific resources

### 3. Handle Errors
If a command fails:
- Check if authentication is required (error message will indicate this)
- Verify that required parameters are provided
- Check if the resource exists (invalid IDs will cause errors)

### 4. Chain Operations
You can chain multiple commands to:
- First list projects to find a project ID
- Then use that project ID to list user stories
- Then get details of a specific user story
- And so on...

## Example Workflows

### Workflow 1: Get Project Information
```
1. Execute: taiga project list
2. Parse output to find project ID
3. Execute: taiga project get <project-id>
4. Parse and present project details to user
```

### Workflow 2: Create a User Story
```
1. Execute: taiga project list (to find project ID)
2. Execute: taiga status userstory-statuses -p <project-id> (to find available statuses)
3. Execute: taiga user list -p <project-id> (to find user IDs for assignment)
4. Execute: taiga userstory create -p <project-id> -t "Story Title" -d "Description" -s <status-id>
5. Parse output to confirm creation
```

### Workflow 3: Search and Retrieve
```
1. Execute: taiga search "search query" -p <project-id>
2. Parse search results to identify relevant items
3. Execute specific get commands for items of interest
4. Present findings to user
```

## Best Practices

1. **Always Check Authentication First**: If you get an authentication error, prompt the user to login first.

2. **Use Project Filtering**: When possible, use the `-p <project-id>` option to filter results and improve performance.

3. **Parse IDs Carefully**: Extract IDs from command output carefully, as they're needed for subsequent operations.

4. **Handle Empty Results**: Commands may return "No items found" - handle this gracefully.

5. **Respect Rate Limits**: While the CLI doesn't enforce rate limits, be mindful of making too many rapid requests.

6. **Use Help Commands**: If unsure about command syntax, you can execute `taiga <command> --help` to get detailed information.

7. **Error Messages**: The CLI provides descriptive error messages - parse and present them clearly to the user.

## Configuration

The CLI stores authentication tokens and configuration in `~/.taiga-cli/config.json`. You don't need to manage this file directly - the CLI handles it automatically.

## Integration Notes

- **Output Format**: All commands output plain text to stdout. Parse this text to extract structured information.
- **Exit Codes**: Commands exit with code 0 on success, non-zero on failure.
- **No Interactive Prompts**: The CLI is designed for non-interactive use, making it ideal for AI integration.
- **JSON Output**: While the CLI outputs human-readable text, you can parse it to extract structured data for your needs.

## Limitations

- The CLI does not provide JSON output mode (outputs human-readable text only)
- Some operations may require multiple commands to complete (e.g., getting project ID before listing user stories)
- Authentication must be done explicitly before other operations

## Getting Help

For any command, you can get help by running:
```bash
taiga <command> --help
```

Or for general help:
```bash
taiga --help
```

This will provide detailed information about available options and usage patterns.

