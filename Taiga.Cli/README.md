# Taiga CLI

A powerful command-line interface for Taiga project management, built with .NET 10 and Cocona.

## Table of Contents

- [Features](#features)
- [Installation](#installation)
  - [Prerequisites](#prerequisites)
  - [Global install - Preferred method](#global-install---preferred-method)
  - [Run from DotNet DNX](#run-from-dotnet-dnx)
- [Usage](#usage)
  - [Authentication](#authentication)
  - [Projects](#projects)
  - [User Stories](#user-stories)
  - [Tasks & Issues](#tasks--issues)
  - [Help](#help)
- [Configuration](#configuration)
- [AI Integration: MCP Server Replacement](#ai-integration-mcp-server-replacement)
  - [For AI Developers](#for-ai-developers)
  - [Instructions for AI LLMs](#instructions-for-ai-llms)
  - [Benefits](#benefits)
  - [Example AI Workflow](#example-ai-workflow)
- [Contributing](#contributing)
- [License](#license)

## Features

- 🔐 **Authentication**: Login with Taiga.io or self-hosted instances
- 📁 **Projects**: List, view details, stats, memberships, and roles
- 📝 **User Stories**: Manage user stories, view history, comments, and attachments
- ✅ **Tasks**: Track tasks, update status, and view history
- 🐛 **Issues**: Manage issues with severity, priority, and type tracking
- 🚀 **Epics**: Handle high-level features and related user stories
- 📅 **Milestones**: Track sprint progress and burndown charts
- 📚 **Wiki**: Access project documentation and history
- 🔍 **Search**: Full-text search across projects or within specific projects
- 📊 **Timeline**: View project and user activity timelines

## Installation

### Prerequisites

- .NET 10.0 SDK or later

### Global install - Preferred method

Use the command:
```bash
dotnet tool install --global taiga-cli
```

### Run from DotNet DNX

Use the command:
```bash
dnx taiga-cli
```

## Usage

### Authentication

Login to Taiga.io (default):
```bash
taiga auth login -u <username> -p <password>
```

Login to a self-hosted instance:
```bash
taiga auth login -u <username> -p <password> -a https://taiga.mycompany.com
```

Logout:
```bash
taiga auth logout
```

### Projects

List all projects:
```bash
taiga project list
```

Get project details:
```bash
taiga project get <project-id>
```

### User Stories

List user stories for a specific project:
```bash
taiga userstory list -p <project-id>
```

Get user story details:
```bash
taiga userstory get <story-id>
```

### Tasks & Issues

List tasks:
```bash
taiga task list -p <project-id>
```

List issues:
```bash
taiga issue list -p <project-id>
```

### Help

View all available commands:
```bash
taiga --help
```

View help for a specific command:
```bash
taiga userstory --help
```

## Configuration

The CLI stores configuration (including the authentication token) in `~/.taiga-cli/config.json`.

## AI Integration: MCP Server Replacement

The Taiga CLI can be used as a replacement for an MCP (Model Context Protocol) server when integrating with AI assistants and LLMs. Instead of setting up a dedicated MCP server, AI systems can execute CLI commands directly and parse their output.

### For AI Developers

If you're building an AI assistant that needs to interact with Taiga, you can use this CLI tool by:

1. **Executing Commands**: Have your AI system execute `taiga` commands programmatically
2. **Parsing Output**: Parse the human-readable output from commands to extract structured data
3. **Chaining Operations**: Chain multiple commands together to perform complex workflows

### Instructions for AI LLMs

We provide detailed instructions for AI LLMs in [`instructions-to-ai.md`](instructions-to-ai.md). This file contains:

- Complete command reference
- Usage patterns and workflows
- Best practices for AI integration
- Example workflows for common tasks
- Error handling guidelines

You can include this file in your AI system's context or use it as a reference when configuring your AI assistant to work with Taiga.

### Benefits

- **No MCP Server Required**: Skip the complexity of setting up and maintaining an MCP server
- **Direct Integration**: Execute commands directly from your AI system
- **Full Feature Access**: Access all CLI features without additional API layers
- **Simple Authentication**: Use standard CLI authentication flow

### Example AI Workflow

```bash
# AI system executes commands like:
taiga project list                    # Find projects
taiga userstory list -p 123          # List user stories
taiga userstory get 456              # Get details
taiga userstory create -p 123 -t "New Story"  # Create new story
```

The AI system then parses the output and presents it to the user in a conversational format.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

