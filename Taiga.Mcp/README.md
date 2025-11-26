

# Taiga MCP

A Model Context Protocol (MCP) server for Taiga project management, enabling seamless AI integration with Taiga.io and self-hosted Taiga instances.

## Table of Contents

- [Features](#features)
- [Installation](#installation)
  - [Prerequisites](#prerequisites)
  - [Install from NuGet](#install-from-nuget)
  - [Build from Source](#build-from-source)
- [Configuration](#configuration)
  - [Cursor Integration](#cursor-integration)
  - [Other MCP Clients](#other-mcp-clients)
- [Available Tools](#available-tools)
  - [Projects](#projects)
  - [User Stories](#user-stories)
  - [Tasks](#tasks)
  - [Issues](#issues)
  - [Epics](#epics)
  - [Search](#search)
- [Environment Variables](#environment-variables)
- [Usage Examples](#usage-examples)
- [Development](#development)
  - [Architecture](#architecture)
  - [Adding New Tools](#adding-new-tools)
  - [Disabled Tools](#disabled-tools)
- [Contributing](#contributing)
- [License](#license)

## Features

- 🤖 **AI Integration**: Full MCP server implementation for seamless AI assistant integration
- 📁 **Projects**: List and manage Taiga projects
- 📝 **User Stories**: Create, read, update, and list user stories with full lifecycle management
- ✅ **Tasks**: Track and manage tasks within user stories
- 🐛 **Issues**: Handle issues with severity, priority, and type tracking
- 🚀 **Epics**: Manage high-level features and their related user stories
- 🔍 **Search**: Full-text search across projects and within specific projects
- 🔐 **Authentication**: Secure authentication with Taiga.io or self-hosted instances
- 📊 **Rich Data**: Structured responses with comprehensive project management information

## Installation

### Prerequisites

- .NET 10.0 SDK or later
- Taiga instance (taiga.io or self-hosted)

### Install from NuGet

```bash
dotnet tool install --global taiga-mcp
```

### Build from Source

```bash
git clone https://github.com/josuelopezv/taiga-cli.git
cd taiga-cli/Taiga.Mcp
dotnet build --configuration Release
dotnet pack --configuration Release
dotnet tool install --global --add-source ./bin/Release taiga-mcp
```

## Configuration

### Cursor Integration

Add the following configuration to your `cursor/settings.json`:

```json
{
  "mcpServers": {
    "taiga-mcp": {
      "type": "stdio",
      "command": "taiga-mcp",
      "env": {
        "TAIGA_API_URL": "https://taiga-server-url.com",
        "TAIGA_USERNAME": "your-username",
        "TAIGA_PASSWORD": "your-password"
      }
    }
  }
}
```

### Other MCP Clients

For other MCP-compatible clients, configure the server with:

```json
{
  "mcpServers": {
    "taiga-mcp": {
      "command": "taiga-mcp",
      "env": {
        "TAIGA_API_URL": "https://your-taiga-instance.com",
        "TAIGA_USERNAME": "your-username",
        "TAIGA_PASSWORD": "your-password"
      }
    }
  }
}
```

## Available Tools

### Projects

- **ListProjects**: List all accessible projects

### User Stories

- **ListUserStories**: List user stories with optional filtering by project or epic
- **GetUserStory**: Get detailed information about a specific user story
- **CreateUserStory**: Create a new user story with subject, description, status, tags, assignment, and milestone
- **EditUserStory**: Update existing user stories with new information

### Tasks

- **ListTasks**: List tasks with optional filtering by project or user story
- **GetTask**: Get detailed information about a specific task
- **CreateTask**: Create new tasks within user stories
- **EditTask**: Update task status, assignment, and other properties

### Issues

- **ListIssues**: List issues with optional filtering by project
- **GetIssue**: Get detailed information about a specific issue
- **CreateIssue**: Create new issues with severity, priority, and type
- **EditIssue**: Update issue properties and status

### Epics

- **ListEpics**: List epics with optional filtering by project
- **GetEpic**: Get detailed information about a specific epic
- **CreateEpic**: Create new epics to group related user stories
- **EditEpic**: Update epic information and relationships

### Search

- **Search**: Perform full-text search across projects or within specific projects

## Environment Variables

| Variable | Description | Required | Default |
|----------|-------------|----------|---------|
| `TAIGA_API_URL` | URL of your Taiga instance | Yes | None |
| `TAIGA_USERNAME` | Your Taiga username | Yes | None |
| `TAIGA_PASSWORD` | Your Taiga password | Yes | None |

## Usage Examples

Once configured, AI assistants can interact with Taiga through natural language:

- "Show me all projects in Taiga"
- "List user stories for project X"
- "Create a new user story titled 'Implement dark mode' in project Y"
- "Find all issues with 'bug' in the title"
- "Update task status to 'In Progress'"
- "Search for 'authentication' across all projects"

The MCP server handles authentication, API calls, and data formatting automatically.

## Development

### Architecture

The server is built using:
- **Model Context Protocol**: Standard interface for AI tool integration
- **.NET 10.0**: Modern .NET with dependency injection
- **Microsoft.Extensions.Hosting**: Robust hosting framework
- **Taiga.Api**: Shared API client library

### Adding New Tools

1. Create a new tool class inheriting from `BaseTool`
2. Decorate methods with `[McpServerTool]` attributes
3. Implement the tool logic using the injected `ITaigaApi`
4. Add proper logging and error handling

### Disabled Tools

The following tools are implemented but currently disabled:
- **Milestones**: Sprint and milestone management
- **Status**: Status management utilities
- **Timeline**: Activity timeline views
- **Users**: User management and lookup
- **Webhooks**: Webhook configuration
- **Wiki**: Project documentation management

These can be enabled by uncommenting the relevant code in their respective files.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.