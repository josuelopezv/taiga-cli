# Taiga CLI

A powerful command-line interface for Taiga project management, built with .NET 10 and Cocona.

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
- 🔔 **Notifications**: Check unread notifications and manage them
- 📊 **Timeline**: View project and user activity timelines

## Installation

### Prerequisites

- .NET 10.0 SDK or later

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

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.

